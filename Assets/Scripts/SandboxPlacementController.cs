using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SandboxPlacementController : MonoBehaviour
{

    //[SerializeField]
    //private GameObject dominoPiecePrefab;

    public float verticalSpeed = 0.001f;

    [SerializeField]
    private GameObject finalPiece, pieceOne, pieceTwo, pieceThree, pieceFour, pieceFive, pieceSix, pieceSeven, pieceEight, pieceNine;

    [SerializeField]
    private GameObject structureOne, structureTwo, structureThree, structureFour, structureFive;

    [SerializeField]
    private Button piecesTab, structuresTab, playButton, editorButton;

    [SerializeField]
    private GameObject ControlObjectTab;

    private GameObject selectedGameObject, objectInFocus;
    private float mouseWheelRotation;
    private bool hasEdit = false;

    void Update()
    {
        //HandleObjectHotKey();

        if (selectedGameObject != null)
        {
            MoveCurrentSelectedObjectToMouse();
            RotateFromMouseWheel();
            MoveAxisOnEditMode();
            ReleaseIfClicked();
        }
        else HandleEditPiece();

        if (objectInFocus) EditPiece();
    }


    // ---------------- HANDLE EDIT OBJECT ----------------
    private void EditPiece()
    {
        Slider scaleSliderX = ControlObjectTab.transform.GetChild(0).gameObject.GetComponent<Slider>();
        Slider scaleSliderY = ControlObjectTab.transform.GetChild(1).gameObject.GetComponent<Slider>();
        Slider scaleSliderZ = ControlObjectTab.transform.GetChild(2).gameObject.GetComponent<Slider>();
        Slider scaleMass = ControlObjectTab.transform.GetChild(3).gameObject.GetComponent<Slider>();

        if (!hasEdit)
        {
            scaleSliderX.value = objectInFocus.transform.localScale.x;
            scaleSliderY.value = objectInFocus.transform.localScale.y;
            scaleSliderZ.value = objectInFocus.transform.localScale.z;
        }
        objectInFocus.transform.localScale = new Vector3(scaleSliderX.value, scaleSliderY.value, scaleSliderZ.value);
        hasEdit = true;

        Rigidbody objectRb = objectInFocus.gameObject.GetComponent<Rigidbody>();
        objectRb.mass = scaleMass.value;
    }

    public void DeleteObject()
    {
        // delete object and instantiate a new one
        Destroy(objectInFocus);
        objectInFocus.GetComponent<Rigidbody>().isKinematic = true;
        selectedGameObject = Instantiate(objectInFocus, objectInFocus.transform.position, objectInFocus.transform.rotation);

        ControlObjectTab.SetActive(false);
        objectInFocus = null;
        hasEdit = false;


    }

    private void HandleEditPiece()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Piece")
                {
                    ControlObjectTab.SetActive(true);
                    objectInFocus = hit.transform.gameObject;
                }
                else if (objectInFocus)
                {
                    ControlObjectTab.SetActive(false);
                    objectInFocus = null;
                    hasEdit = false;
                }
            }
        }
    }
    // ---------------- ---------------- ----------------



    public void PlayGame()
    {
        GameObject[] allPieaces = GameObject.FindGameObjectsWithTag("Piece");

        for (int i = 0; i < allPieaces.Length; i++)
        {
            allPieaces[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        playButton.gameObject.SetActive(false);
        editorButton.gameObject.SetActive(true);
    }

    public void BackToEditorMode()
    {
        // return objetcs to its original position
        GameObject[] allPieaces = GameObject.FindGameObjectsWithTag("Piece");
        for (int i = 0; i < allPieaces.Length; i++)
        {
            allPieaces[i].transform.position = allPieaces[i].gameObject.GetComponent<PieceController>().originalPosition;
            allPieaces[i].transform.rotation = allPieaces[i].gameObject.GetComponent<PieceController>().originalRotation;
            allPieaces[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        playButton.gameObject.SetActive(true);
        editorButton.gameObject.SetActive(false);
    }

    private void MoveAxisOnEditMode()
    {
        if (Input.GetButton("Fire1") && selectedGameObject != null)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                var pos = selectedGameObject.transform.position;
                pos.y += verticalSpeed;
                selectedGameObject.transform.SetPositionAndRotation(pos, selectedGameObject.transform.rotation);
                selectedGameObject.GetComponent<PieceController>().ChangePosition(pos, selectedGameObject.transform.rotation);

            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                var pos = selectedGameObject.transform.position;
                if (pos.y < 0) pos.y = 0;
                else pos.y -= verticalSpeed;
                selectedGameObject.transform.SetPositionAndRotation(pos, selectedGameObject.transform.rotation);
                selectedGameObject.GetComponent<PieceController>().ChangePosition(pos, selectedGameObject.transform.rotation);

            }
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
        if (Input.GetButton("Fire1"))
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
            if (hitInfo.collider.tag == "Floor" && !Input.GetButton("Fire1"))
            {
                selectedGameObject.transform.position = new Vector3(hitInfo.point.x, selectedGameObject.transform.position.y, hitInfo.point.z); // place object in the mouse position
                Quaternion objectRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                selectedGameObject.transform.rotation = objectRotation; // rotates the objects based on the normal of the object
                selectedGameObject.GetComponent<PieceController>().ChangePosition(new Vector3(hitInfo.point.x, selectedGameObject.transform.position.y, hitInfo.point.z), objectRotation);

            }

        }
    }

    // ---------------- HANDLE TABS ----------------
    public void SwitchToPieces()
    {
        ColorBlock piecesColors = piecesTab.GetComponent<Button>().colors;
        piecesColors.normalColor = Color.white;
        piecesTab.GetComponent<Button>().colors = piecesColors;

        ColorBlock structureColors = structuresTab.GetComponent<Button>().colors;
        structureColors.normalColor = Color.grey;
        structuresTab.GetComponent<Button>().colors = structureColors;

        structuresTab.transform.GetChild(1).gameObject.SetActive(false);
        piecesTab.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void SwitchToStructures()
    {
        ColorBlock piecesColors = piecesTab.GetComponent<Button>().colors;
        piecesColors.normalColor = Color.grey;
        piecesTab.GetComponent<Button>().colors = piecesColors;

        ColorBlock structureColors = structuresTab.GetComponent<Button>().colors;
        structureColors.normalColor = Color.white;
        structuresTab.GetComponent<Button>().colors = structureColors;

        piecesTab.transform.GetChild(1).gameObject.SetActive(false);
        structuresTab.transform.GetChild(1).gameObject.SetActive(true);


    }
    // ---------------- ---------------- ----------------


    // ---------------- HANDLE INSTANTIATE PIECES AND STRUCTURES ----------------
    void HandleSelectPrefab(GameObject prefab)
    {
        if (selectedGameObject == null)
        {
            prefab.GetComponent<Rigidbody>().isKinematic = true;
            selectedGameObject = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        }
        else Destroy(selectedGameObject);

    }

    public void GenerateFinalPiece()
    {
        HandleSelectPrefab(finalPiece);
    }

    public void GeneratePieceOne()
    {
        HandleSelectPrefab(pieceOne);
    }

    public void GeneratePieceTwo()
    {
        HandleSelectPrefab(pieceTwo);
    }
    public void GeneratePieceThree()
    {
        HandleSelectPrefab(pieceThree);
    }

    public void GeneratePieceFour()
    {
        HandleSelectPrefab(pieceFour);
    }

    public void GeneratePieceFive()
    {
        HandleSelectPrefab(pieceFive);
    }

    public void GeneratePieceSix()
    {
        HandleSelectPrefab(pieceSix);
    }

    public void GeneratePieceSeven()
    {
        HandleSelectPrefab(pieceSeven);
    }

    public void GeneratePieceEight()
    {
        HandleSelectPrefab(pieceEight);
    }

    public void GeneratePieceNine()
    {
        HandleSelectPrefab(pieceNine);
    }








    public void GenerateStructureOne()
    {
        HandleSelectPrefab(structureOne);
    }

    public void GenerateStructureTwo()
    {
        HandleSelectPrefab(structureTwo);
    }

    public void GenerateStructureThree()
    {
        HandleSelectPrefab(structureThree);
    }
    public void GenerateStructureFour()
    {
        HandleSelectPrefab(structureFour);
    }

    public void GenerateStructureFive()
    {
        HandleSelectPrefab(structureFive);
    }
    // ---------------- ---------------- ---------------- ----------------
}
