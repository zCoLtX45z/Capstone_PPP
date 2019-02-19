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

    // Post set variables
    private Mesh ObjectMesh = null;

    // Running variables
    private bool MeshSet = false;
    private RaycastHit LookHit;
    [HideInInspector]
    public Vector3 ObjectPosition = Vector3.zero;
    [HideInInspector]
    public Vector3 ObjectNormal = Vector3.zero;

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

    public bool UpdatePlacement()
    {
        if (MeshSet)
        {
            if (Physics.Raycast(LookDirection.position, LookDirection.forward, out LookHit, PlaceDistance, PlaceLayers))
            {
                // Find where you are looking
                Vector3 ObjectPosition = LookHit.point;
                Vector3 ObjectNormal = LookHit.normal;

                // Set the child object to that position and set rotation
                ChildTransform.position = ObjectPosition;
                ChildTransform.up = ObjectNormal;

                // Set the mesh to the desired mesh
                ChildMesh.mesh = ObjectMesh;

                // Set the mesh and collider to fit each other
                ChildCollider.size = ChildMesh.mesh.bounds.size;
                MeshTransform.position = Vector3.zero;


                // Turn on object
                ChildTransform.gameObject.SetActive(true);

                // Make sure hte mesh is not in the ground
                MeshTransform.Translate(0, ChildMesh.mesh.bounds.size.y / 2, 0);
                if (PT.TriggerActive)
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
                ChildTransform.gameObject.SetActive(false);
                return false;
            }
        }
        return false;
    }
}
