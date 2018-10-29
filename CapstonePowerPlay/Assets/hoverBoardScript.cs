using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverBoardScript : MonoBehaviour
{
    public Rigidbody m_body;
    public float m_deadZone = 0.1f;

    public float m_forwardAcl = 100.0f;
    public float m_backwardAcl = 25.0f;
    public float m_currThrust = 0.0f;

    public float m_turnStrength = 10f;
    public float m_currTurn = 0.0f;

    public LayerMask m_layerMask;
    public float m_maxHoverForce = 9.0f;
    public float m_minHoverForce = 0.0f;
    public float m_hoverHeight = 2.0f;
    public GameObject[] m_hoverPoints;

    [SerializeField]
    private PIDController[] PIDHoverPoints;

    // PID Controller
    [SerializeField]
    private float Kp = 1.1f;
    [SerializeField]
    private float Ki = 0.0f;
    [SerializeField]
    private float Kd = 0.0f;
    private bool[] ToggleStabilizers;
    private int StabalizersActive = 0;
    private bool AutoStabalize = true;

    // Maximum
    [SerializeField]
    private float MaxSpeed = 10;
    public float Speed;

    [SerializeField]
    private float MaxTurnAdjustPercent = 50f;
    private float CurrentAdjust = 0;
    [SerializeField]
    private float SpeedDeadZone = 5f;

	// Use this for initialization
	void Start ()
    {
        Physics.gravity = new Vector3(0, -100, 0);
        if (!m_body)
            m_body = GetComponent<Rigidbody>();

        //m_layerMask = 1 << LayerMask.NameToLayer("Characters");
        //m_layerMask = ~m_layerMask;
        if (PIDHoverPoints.Length > 0)
        {
            float KAdjust = m_maxHoverForce / m_hoverHeight;
            ToggleStabilizers = new bool[PIDHoverPoints.Length];
            for (int i = 0; i < PIDHoverPoints.Length; i++)
            {
                ToggleStabilizers[i] = true;
                PIDHoverPoints[i].setGains(Kp * KAdjust, Ki * KAdjust, Kd * KAdjust);
                PIDHoverPoints[i].EnableClamp(m_minHoverForce, m_maxHoverForce);
                //PIDHoverPoints[i].transform.localPosition = new Vector3(PIDHoverPoints[i].transform.localPosition.x, 0, PIDHoverPoints[i].transform.localPosition.z);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //main thrust
        m_currThrust = 0.0f;
        float aclAxis = Input.GetAxis("Vertical");
        if(aclAxis > m_deadZone)
        {
            m_currThrust = aclAxis * m_forwardAcl;
        }
        else if(aclAxis < m_deadZone)
        {
            m_currThrust = aclAxis * m_backwardAcl;
        }
        Speed = m_body.velocity.magnitude;

        //turning
        m_currTurn = 0.0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if(Mathf.Abs(turnAxis) > m_deadZone)
        {
            m_currTurn = turnAxis * 4;
        }
        // Adjust turning height adjustment
        CurrentAdjust = Speed < SpeedDeadZone ? 0.0f
            : Speed < MaxSpeed ? m_hoverHeight * MaxTurnAdjustPercent * (Speed - SpeedDeadZone) / ((MaxSpeed - SpeedDeadZone) * 100f)
            : m_hoverHeight * MaxTurnAdjustPercent / (100f);

    }

    void FixedUpdate()
    {

        // Non PID Controllers
        //Hover force
        if (m_hoverPoints.Length > 0)
        {
            RaycastHit hit;
            for (int i = 0; i < m_hoverPoints.Length; i++)
            {
                var hoverPoint = m_hoverPoints[i];
                if (Physics.Raycast(hoverPoint.transform.position, -transform.up, out hit, m_hoverHeight + 0.3f, m_layerMask))
                {
                    Debug.DrawRay(hoverPoint.transform.position, -hoverPoint.transform.up * hit.distance, Color.red);
                    m_body.AddForceAtPosition(Vector3.up * m_maxHoverForce * (1.0f - (hit.distance / m_hoverHeight)), hoverPoint.transform.position);
                    //Debug.Log(hoverPoint.name + ", Force: " + (Vector3.up * m_hoverForce * (1.0f - (hit.distance / m_hoverHeight))).y + ", Distance: " + hit.distance);
                }
                else
                {
                    if (!Input.GetKey(KeyCode.Space))
                    {
                        if (transform.position.y > hoverPoint.transform.position.y)
                        {
                            m_body.AddForceAtPosition(hoverPoint.transform.up * m_maxHoverForce, hoverPoint.transform.position);
                            Debug.DrawRay(hoverPoint.transform.position, -hoverPoint.transform.up * hit.distance, Color.black);
                        }
                        else
                        {
                            m_body.AddForceAtPosition(hoverPoint.transform.up * -m_maxHoverForce, hoverPoint.transform.position);
                            Debug.DrawRay(hoverPoint.transform.position, -hoverPoint.transform.up * hit.distance, Color.blue);
                        }
                    }
                }
            }
        }

        StabalizersActive = 0;
        // PID controllers
        if (PIDHoverPoints.Length > 0)
        {

            RaycastHit hit;
            for (int i = 0; i < PIDHoverPoints.Length; i++)
            {
                PIDController temp = PIDHoverPoints[i];
                if (Physics.Raycast(temp.gameObject.transform.position, -transform.up, out hit, m_hoverHeight + 1.3f, m_layerMask))
                {
                    StabalizersActive++;
                    if (!ToggleStabilizers[i])
                    {
                        ToggleStabilizers[i] = true;
                        temp.WipeErrors();
                    }
                    // This changes the target hieght when turning,NOTE all left and right PID points on the controller MUST be names and changed here
                    float TargetAdjustHeight = (temp.name == "frontLeft" || temp.name == "centerLeft" || temp.name == "backLeft") && m_currTurn < 0f ? -CurrentAdjust
                        : (temp.name == "frontLeft" || temp.name == "centerLeft" || temp.name == "backLeft") && m_currTurn > 0f ? CurrentAdjust
                        :(temp.name == "frontRight" || temp.name == "centerRight" || temp.name == "backRight") && m_currTurn < 0f ? CurrentAdjust
                        : (temp.name == "frontRight" || temp.name == "centerRight" || temp.name == "backRight") && m_currTurn > 0f ? -CurrentAdjust
                        : 0f;
                    temp.step(m_hoverHeight + TargetAdjustHeight, hit.distance);
                    m_body.AddForceAtPosition(Vector3.up * temp.getOutput(), temp.gameObject.transform.position);

                    Debug.DrawRay(temp.gameObject.transform.position, -temp.gameObject.transform.up * hit.distance, Color.red);
                    Debug.Log(temp.gameObject.name + ", Force: " + (temp.gameObject.transform.up * m_maxHoverForce * (1.0f - (hit.distance / m_hoverHeight))).y + ", Distance: " + hit.distance);
                }
                else
                {

                    if (Input.GetMouseButton(1) || StabalizersActive == 0) 
                    {
                        if (ToggleStabilizers[i])
                        {
                            ToggleStabilizers[i] = false;
                            temp.WipeErrors();
                        }
                        temp.step(transform.position.y, temp.gameObject.transform.position.y);
                        m_body.AddForceAtPosition(temp.gameObject.transform.up * temp.getOutput(), temp.gameObject.transform.position);
                    }
                }
            }
        }
        //forward
        if (Mathf.Abs(m_currThrust) > 0)
        {
            m_body.AddForce(transform.forward * m_currThrust * Time.fixedDeltaTime, ForceMode.Acceleration);
            if (m_body.velocity.z > MaxSpeed)
                m_body.velocity = new Vector3(m_body.velocity.x, m_body.velocity.y, MaxSpeed);
            else if (m_body.velocity.z < -MaxSpeed)
                m_body.velocity = new Vector3(m_body.velocity.x, m_body.velocity.y, -MaxSpeed);
        }
        //turn
        if(m_currTurn > 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }
        else if(m_currTurn < 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }
    }

    void OnDrawGizmos()
    {
        
    }

    public void ChangeKp(float kp)
    {
        Kp = kp;
        if (Kp < 0f)
            Kp = 0f;
        float KAdjust = m_maxHoverForce / m_hoverHeight;
        for (int i = 0; i < PIDHoverPoints.Length; i++)
        {
            PIDHoverPoints[i].setGains(Kp * KAdjust, Ki * KAdjust, Kd * KAdjust);
        }
    }

    public void ChangeKi(float ki)
    {
        Ki = ki;
        if (Ki < 0f)
            Ki = 0f;
        float KAdjust = m_maxHoverForce / m_hoverHeight;
        for (int i = 0; i < PIDHoverPoints.Length; i++)
        {
            PIDHoverPoints[i].setGains(Kp * KAdjust, Ki * KAdjust, Kd * KAdjust);
        }
    }

    public void ChangeKd(float kd)
    {
        Kd = kd;
        if (Kd < 0f)
            Kd = 0f;
        float KAdjust = m_maxHoverForce / m_hoverHeight;
        for (int i = 0; i < PIDHoverPoints.Length; i++)
        {
            PIDHoverPoints[i].setGains(Kp * KAdjust, Ki * KAdjust, Kd * KAdjust);
        }
    }
}
