using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBounds : MonoBehaviour
{
    // Created with aid of tutorial by ZER0 Unity Tutorials

    public Transform Player1;
    [SerializeField] float xPos,yPos,zPos;
    [SerializeField] float outBoundsDmg = 5f;


    // Method for when Player1 object leaves the desired bounds
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // You can adjust variables through the Inspector
            StartCoroutine(Drowning());
        }
        if(other.gameObject.tag == "Enemy")
        {

            Destroy(other.gameObject);
            FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>().enemyHasDied(other.gameObject);

        }
    }
    IEnumerator Drowning()
    {

        new WaitForSeconds(1.5f);
        ScoreboardManager.Instance.StopGame();
        yield return null;

    }
}
