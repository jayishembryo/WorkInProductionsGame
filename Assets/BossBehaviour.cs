using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    public int health;
    [SerializeField] private int currentAttack;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNextAttackAtVariableTime()
    {
        Invoke(nameof(PerformAttack), Random.Range(0.7f, 1.95f));
    }

    public void PerformAttack()
    {
        int chosenAttack = Random.Range(1, 3);
        switch (chosenAttack)
        {
            case 1:
                anim.SetTrigger("fire");
                break;
            case 2:
                anim.SetTrigger("lightning");
                break;
            case 3:
                anim.SetTrigger("idk");
                break;
        }
    }
    public void PerformSpecialAttack(int whichOne)
    {
        switch (whichOne)
        {
            case 0:
                anim.SetTrigger("beginFlood");
                break;
            case 1:
                anim.SetTrigger("ice");
                break;
        }
    }
    public void EnterFloodedState()
    {
        anim.SetBool("flooded", true);
        Debug.Log("The arena is now flooded!");
    }

    public void DeathFunctions(int progress)
    {
        switch (progress)
        {
            case 0:
                //Destroy(spawnManager.gameObject);
                GameObject.Find("EndScreen").GetComponent<Animation>().Play();
                break;
            case 1:
                SceneManager.LoadScene(0);
                break;
        }
    }
}

