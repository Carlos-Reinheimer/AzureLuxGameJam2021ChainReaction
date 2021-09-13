using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public AudioClip collidePiece;
    public bool canScaleSize = false;
    public bool canScaleMass = false;
    public bool isACollection = false;
    public bool rotate90Deg = false;

    public void ChangePosition(Vector3 newPosition, Quaternion newRotation)
    {
        originalPosition = newPosition;
        originalRotation = newRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Piece" || collision.gameObject.tag == "Structure")
        {
            gameObject.GetComponent<AudioSource>().volume = 0.3f;
            gameObject.GetComponent<AudioSource>().PlayOneShot(collidePiece);
        }
    }

}
