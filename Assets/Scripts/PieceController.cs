using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public bool canScaleSize = false;
    public bool canScaleMass = false;
    public bool isACollection = false;
    public bool rotate90Deg = false;

    public void ChangePosition(Vector3 newPosition, Quaternion newRotation)
    {
        originalPosition = newPosition;
        originalRotation = newRotation;
    }

}
