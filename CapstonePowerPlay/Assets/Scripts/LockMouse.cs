using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouse : MonoBehaviour {
    CursorLockMode lockState;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetLockState();
        }

	}

    public void SetLockState()
    {
        //if (Cursor.lockState == CursorLockMode.Locked)
        //{
        //    Cursor.lockState = CursorLockMode.None;

        //    Cursor.visible = true;
        //}

        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;
        }

        //Cursor.lockState = lockState;

        //Cursor.visible = (CursorLockMode.Locked != lockState);
    }

}
