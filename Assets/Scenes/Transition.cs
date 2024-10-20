using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public int WhichScene;

    private void Start()
    {
        SceneManager.LoadScene(WhichScene);
    }

    
}
