using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placing : MonoBehaviour {

    // Pre set variables
    [SerializeField]
    private Material PlacingMaterialRed;
    [SerializeField]
    private Material PlacingMaterialBlue;
    [SerializeField]
    private Transform LookDirection;
    [SerializeField]
    private float PlaceDistance = 5;
    [SerializeField]
    private LayerMask PlaceLayers;
    [SerializeField]
    private MeshFilter ChildMesh;
    [SerializeField]
    private MeshRenderer ChildMeshRenderer;
    [SerializeField]
    private Transform ChildTransform;
    [SerializeField]
    private Transform MeshTransform;
    [SerializeField]
    private BoxCollider ChildCollider;
    [SerializeField]
    private PlacingTrigger PT;
    [SerializeField]
    private Transform PlayerCameraTransform;

    // Post set variables
    private Mesh ObjectMesh = null;

    // Running variables
    private bool MeshSet = false;
    private RaycastHit LookHit;
    [HideInInspector]
    public Vector3 ObjectPosition = Vector3.zero;
    [HideInInspector]
    public Vector3 ObjectNormal = Vector3.zero;
    [HideInInspector]
    public Vector3 MeshTransformChange = Vector3.zero;
    [SerializeField]
    private float ObjectTurnOffset = 90;

    public void ChangePlaceDistance(float dist)
    {
        PlaceDistance = dist;
    }

    public void TurnSettingBoolOff()
    {
        MeshSet = false;
    }

    public void SetMesh(Mesh meshObject)
    {
        ObjectMesh = meshObject;
        if (ObjectMesh != null)
            MeshSet = true;
    }

    public Vector3 GetPlacePosition()
    {
        return ChildTransform.position;
    }

    public bool UpdatePlacement(float MeshScale = 1)
    {
        if (MeshSet)
        {
            if (Physics.Raycast(LookDirection.position, LookDirection.forward, out LookHit, PlaceDistance, PlaceLayers))
            {
                //Debug.DrawRay(LookDirection.position, LookDirection.forward * LookHit.distance, Color.blue);
                /// Find where you are looking
                ObjectPosition = LookHit.point;
                ObjectNormal = LookHit.normal;
                //Debug.DrawRay(ObjectPosition, ObjectNormal * 3, Color.black);
                /// Set the child object to that position and set rotation
                ChildTransform.position = ObjectPosition;
                ChildTransform.up = ObjectNormal;
                //ChildTransform.localScale = ChildTransform.localScale * MeshScale;
                MeshTransform.localEulerAngles = new Vector3(MeshTransform.localEulerAngles.x, PlayerCameraTransform.eulerAngles.y + ObjectTurnOffset, MeshTransform.localEulerAngles.z);
                MeshTransformChange = MeshTransform.eulerAngles;

                /// Set the mesh to the desired mesh
                ChildMesh.mesh = ObjectMesh;

                /// Set the mesh and collider to fit each other
                ChildCollider.size = ChildMesh.mesh.bounds.size * MeshScale;
                float XOffSetCollider = 0;
                float YOffSetCollider = 0;
                float ZOffSetCollider = 0;
                if (ChildCollider.size.x <= 0)
                {
                    XOffSetCollider = 0.1f;
                }
                if (ChildCollider.size.y <= 0)
                {
                    YOffSetCollider = 0.1f;
                }
                if (ChildCollider.size.z <= 0)
                {
                    ZOffSetCollider = 0.1f;
                }
                ChildCollider.size += new Vector3(XOffSetCollider, YOffSetCollider, ZOffSetCollider);
                MeshTransform.localPosition = Vector3.zero;


                /// Turn on object
                ChildTransform.gameObject.SetActive(true);

                /// Make sure hte mesh is not in the ground
                MeshTransform.Translate(0, ChildMesh.mesh.bounds.size.y / 2 + YOffSetCollider, 0);
                if (PT.TriggerActive || PT.InGround)
                {
                    ChildMeshRenderer.material = PlacingMaterialRed;
                    return false;
                }
                else
                {
                    ChildMeshRenderer.material = PlacingMaterialBlue;
                    return true;
                }
            }
            else
            {
                ChildMeshRenderer.material = PlacingMaterialRed;
                //Debug.DrawRay(LookDirection.position, LookDirection.forward * LookHit.distance, Color.red);
                return false;
            }
        }
        return false;
    }
}
