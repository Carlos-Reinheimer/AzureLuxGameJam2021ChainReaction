using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{

    [SerializeField]
    private GameObject dominoPiecePrefab;

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.A;

    private GameObject selectedGameObject;
    private float mouseWheelRotation;

    void Update()
    {
        HandleObjectHotKey();

        if (selectedGameObject != null)
        {
            MoveCurrentSelectedObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButton(0))
        {
            selectedGameObject = null;
        }
    }

    private void RotateFromMouseWheel()
    {
        if (Input.GetButton("Jump"))
        {
            mouseWheelRotation = Input.mouseScrollDelta.y;
            selectedGameObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }
    }

    private void MoveCurrentSelectedObjectToMouse()
    {
        // shoot a ray from the mouse position to the world it self
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "Floor")
            {
                Debug.Log(hitInfo.normal, selectedGameObject.transform);
                selectedGameObject.transform.position = new Vector3(hitInfo.point.x, 1.053842f, hitInfo.point.z); // place object in the mouse position
                selectedGameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal); // rotates the objects based on the normal of the object
            }

        }
    }

    private void HandleObjectHotKey()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            if (selectedGameObject == null)
            {
                selectedGameObject = Instantiate(dominoPiecePrefab);
            }
            else
            {
                Destroy(selectedGameObject);
            }
        }
    }
}
