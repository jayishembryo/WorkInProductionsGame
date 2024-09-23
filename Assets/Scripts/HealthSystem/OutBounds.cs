using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBounds : MonoBehaviour
{
    // Created with aid of tutorial by ZER0 Unity Tutorials

    public Transform Player1;
    [SerializeField] float xPos,yPos,zPos;
    [SerializeField] float outBoundsDmg = 5f;
    [SerializeField] private GameObject drowning;

    // Method for when Player1 object leaves the desired bounds
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // You can adjust variables through the Inspector
            StartCoroutine(nameof(Drowning));
            drowning.SetActive(true);
        }
       // if(other.gameObject.tag == "Enemy")
       // {

           // Destroy(other.gameObject);
           // FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>().enemyHasDied(other.gameObject);

       // }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // You can adjust variables through the Inspector
            StopCoroutine(nameof(Drowning));
            drowning.SetActive(false);
        }
    }
    IEnumerator Drowning()
    {
        yield return new WaitForSeconds(2f);
        ScoreboardManager.Instance.StopGame();
    }
}
