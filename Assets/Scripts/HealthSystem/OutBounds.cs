using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBounds : MonoBehaviour
{
    // Created with aid of tutorial by ZER0 Unity Tutorials

    public Transform Player1;
   // [SerializeField] float xPos,yPos,zPos;
   // [SerializeField] float outBoundsDmg = 5f;


    // Method for when Player1 object leaves the desired bounds
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {

        new WaitForSeconds(1.5f);

        FindObjectOfType<ScoreboardManager>().GetComponent<ScoreboardManager>().StopGame();

        yield return null;

    }

}
