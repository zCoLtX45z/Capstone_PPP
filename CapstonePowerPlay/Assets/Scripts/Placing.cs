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

    public void SetVariables(Mesh meshObject)
    {
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
                ChildObject.transform.position = pos;
                ChildObject.transform.up = norm;
                ChildMesh = ObjectMesh;
                
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
