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

    public int m_layerMask;
    public float m_hoverForce = 9.0f;
    public float m_hoverHeight = 2.0f;
    public GameObject[] m_hoverPoints;

    // Maximum
    [SerializeField]
    private float MaxSpeed = 10;

	// Use this for initialization
	void Start ()
    {
        m_body = GetComponent<Rigidbody>();
        m_layerMask = 1 << LayerMask.NameToLayer("Characters");
        m_layerMask = ~m_layerMask;
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

        //turning
        m_currTurn = 0.0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if(Mathf.Abs(turnAxis) > m_deadZone)
        {
            m_currTurn = turnAxis * 4;
        }
	}

    void FixedUpdate()
    {
      
       
        //Hover force
        RaycastHit hit;
        for(int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if(Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, m_hoverHeight, m_layerMask))
            {
                m_body.AddForceAtPosition(Vector3.up * m_hoverForce * (1.0f - (hit.distance / m_hoverHeight)), hoverPoint.transform.position);
            }
            else
            {
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    m_body.AddForceAtPosition(hoverPoint.transform.up * m_hoverForce, hoverPoint.transform.position);
                }
                else
                {
                    m_body.AddForceAtPosition(hoverPoint.transform.up * -m_hoverForce, hoverPoint.transform.position);
                }
            }
        }
        //forward
        if(Mathf.Abs(m_currThrust) > 0)
        {
            m_body.AddForce(transform.forward * m_currThrust, ForceMode.Acceleration);
            if (m_body.velocity.z > MaxSpeed)
                m_body.velocity = new Vector3(m_body.velocity.z, m_body.velocity.y, MaxSpeed);
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
}
