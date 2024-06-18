using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool hasTarget;


    [SerializeField] private Transform agentTarget;
    [SerializeField] bool isRotating = false;
    [SerializeField] private float rotatingSpeed= 20f;



    private UiManager uiManager;
    private void Start()
        //this is the part that mades our enemy target the player once they are seen
    {
        agent = GetComponent<NavMeshAgent>();

        Time.timeScale = 1f;

        uiManager = FindObjectOfType<UiManager>();

        agentTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() 
    {
        // agent.SetDestination(agentTarget.position);
        // this is from the code we had prior ^^

        //this makes the enemy rotate "look" for us and able to "sight" us in the first place

        if (hasTarget)
        {
            agent.SetDestination(agentTarget.position);
        }
        else
        {
            transform.Rotate(transform.up, angle: rotatingSpeed * Time.deltaTime);
        }

        if (isRotating)
        {
            transform.Rotate(transform.up, angle: rotatingSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
        //this makes it so once they bump into us we die and go to the game over screen
    {
        if (other.collider.CompareTag("Player"))
        {
            uiManager.ShowLostPanel();
        }
    }

    
}
