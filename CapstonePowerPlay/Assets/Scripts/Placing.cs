using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placing : MonoBehaviour {

    // Pre set variables
    [SerializeField]
    private Material PlacingMaterial;
    [SerializeField]
    private Transform LookDirection;
    [SerializeField]
    private float PlaceDistance = 5;
    [SerializeField]
    private LayerMask PlaceLayers;
    [SerializeField]
    private Mesh ChildMesh;
    [SerializeField]
    private GameObject ChildObject;

    // Post set variables
    private Mesh ObjectMesh = null;
    private MeshRenderer ObjectRenderer = null;
    private Collider ObjectPlacingTrigger = null;
    private Transform BaseTransform = null;

    // Running variables
    private bool VariablesSet = false;
    private RaycastHit LookHit;

    public void ChangePlaceDistance(float dist)
    {
        PlaceDistance = dist;
    }

    public void TurnSettingBoolOff()
    {
        VariablesSet = false;
    }

    public void SetVariables(MeshRenderer meshRenderer,Mesh meshObject, Collider ObjectTrigger, Transform bottonTransform)
    {
        ObjectRenderer = meshRenderer;
        ObjectPlacingTrigger = ObjectTrigger;
        BaseTransform = bottonTransform;
        ObjectMesh = meshObject;
        VariablesSet = true;
    }

    public bool UpdatePlacement()
    {
        if (VariablesSet)
        {
            if (Physics.Raycast(LookDirection.position, LookDirection.forward, out LookHit, PlaceDistance, PlaceLayers))
            {
                Vector3 pos = LookHit.point;
                Vector3 norm = LookHit.normal;

                ChildObject.SetActive(true);
                return true;
            }
            else
            {
                ChildObject.SetActive(false);
                return false;
            }
        }
        return false;
    }
}
