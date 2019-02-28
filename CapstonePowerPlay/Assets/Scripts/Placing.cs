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
    private Transform LookDirectionOffset;
    [SerializeField]
    private Transform ItemLookDirection;
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
    private RaycastHit LookHitOffset;
    [HideInInspector]
    public Vector3 ObjectPosition = Vector3.zero;
    [HideInInspector]
    public Vector3 ObjectNormal = Vector3.zero;
    [HideInInspector]
    public Vector3 TurnOffset = Vector3.zero;
    [HideInInspector]
    public Vector3 OffsetDirection = Vector3.zero;
    [HideInInspector]
    public Vector3 ItemWorldPosition = Vector3.zero;
    [HideInInspector]
    public Quaternion ItemWorldRotation = Quaternion.identity;
    [SerializeField]
    private float ObjectTurnOffset = 90;
    private Item PlaceholderItem = null;

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

    public void PlaceItem()
    {
        Destroy(PlaceholderItem.gameObject);
        PlaceholderItem = null;
    }

    public bool UpdatePlacement(Item item, float MeshScale = 1)
    {
        if (Physics.Raycast(LookDirection.position, LookDirection.forward, out LookHit, PlaceDistance, PlaceLayers))
        {
            if (Physics.Raycast(LookDirectionOffset.position, LookDirectionOffset.forward, out LookHitOffset, PlaceDistance, PlaceLayers))
            {
                // Reset the trigger
                //PT.ResetPT();

                Debug.DrawRay(LookDirection.position, LookDirection.forward * LookHit.distance, Color.blue);
                Debug.DrawRay(LookDirectionOffset.position, LookDirectionOffset.forward * LookHitOffset.distance, Color.green);
                Debug.DrawLine(LookHit.point, LookHitOffset.point, Color.yellow);
                Debug.DrawRay(ObjectPosition, ObjectNormal, Color.black);

                // Find where you are looking
                ObjectPosition = LookHit.point;
                ObjectNormal = LookHit.normal;
                OffsetDirection = (LookHit.point - LookHitOffset.point).normalized;

                TurnOffset = MeshTransform.eulerAngles;
                ChildTransform.position = ObjectPosition;
                //ChildTransform.up = ObjectNormal;
                //ChildTransform.forward = OffsetDirection;
                ItemLookDirection.localPosition = Vector3.zero;
                ItemLookDirection.position += OffsetDirection;
                Debug.DrawLine(LookHit.point, ItemLookDirection.position, Color.red);
                ChildTransform.LookAt(ItemLookDirection, ObjectNormal);
                ItemWorldPosition = ChildTransform.position;
                ItemWorldRotation = ChildTransform.rotation;

                // Spawn Temp Item
                if (PlaceholderItem == null)
                {
                    PlaceholderItem = Instantiate(item, ChildTransform);
                    PlaceholderItem.Disable();
                }

                /// Set the mesh to the desired mesh
                //ChildMesh.mesh = ObjectMesh;

                /// Set the mesh and collider to fit each other
                ChildCollider.size = PlaceholderItem.BoxSize;
                ChildCollider.center = PlaceholderItem.BoxOffset;
                MeshTransform.localPosition = Vector3.zero;


                /// Turn on object
                ChildTransform.gameObject.SetActive(true);

                /// Make sure the item isn't always in the ground
                MeshTransform.localPosition += new Vector3(0, ChildCollider.size.y / 2 + 0.1f, 0);
                ///
                if (PT.TriggerActive || PT.InGround)
                {
                    if (PlaceholderItem != null)
                        PlaceholderItem.ChangeMaterials(PlacingMaterialRed);
                    return false;
                }
                else
                {
                    if (PlaceholderItem != null)
                        PlaceholderItem.ChangeMaterials(PlacingMaterialBlue);
                    return true;
                }
            }
            return false;
        }
        else
        {
            if (PlaceholderItem != null)
                PlaceholderItem.ChangeMaterials(PlacingMaterialRed);
            Debug.DrawRay(LookDirection.position, LookDirection.forward * LookHit.distance, Color.red);
            return false;
        }
    }
}
