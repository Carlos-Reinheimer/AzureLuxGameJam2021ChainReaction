using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    public void ChangePosition(Vector3 newPosition, Quaternion newRotation)
    {
        originalPosition = newPosition;
        originalRotation = newRotation;
    }

}
