using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaTriggerController : MonoBehaviour
{
    public GameObject Santa;
    public GameObject LevelPasser;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PieceController>().isTriggerPiece)
        {
            LevelPasser.gameObject.GetComponent<PassLevel>().TriggerSanta();
        }
        else Debug.Log("Teste");
    }
}
