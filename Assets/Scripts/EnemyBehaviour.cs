//Joey Consoli
//Created 1/22/2024
//Changing this format later im lazy xd
//This script tells an enemy to look at the player and move towards them at a set speed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject player;
    private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  //Finds the player
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);  //Makes the front of the object look towards the player, good for a 2d sprite in 3d space
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);  //Moves the enemy towards the player from the previous position by a set amount every frame
    }

}
