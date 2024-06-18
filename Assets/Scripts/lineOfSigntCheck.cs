using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineOfSigntCheck : MonoBehaviour
{

    [SerializeField] private LayerMask layerToCover;
    private GameObject target;
    private Coroutine detectPlayerCoroutine;

    [SerializeField] private SimpleEnemyBehaviour lol;

    [SerializeField] private float viewAngle = 60f;

    //this is for once we are close enough to the enemy they register us
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;

            Debug.Log(message: "Player Enters Area");
            StartCoroutine(routine: DetectPlayer());
        }
    }
    //this is so when we get further away the enemy stops registering us
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;

            Debug.Log(message: "Player exit Area");
            StopCoroutine(detectPlayerCoroutine);
        }
    }
    IEnumerator DetectPlayer()
    {
        //this checks if we can be seen by the enemy or if perhabs we are hiding behind an object
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log(message: "Detect Player...");

            float distanceToPlayer = Vector3.Distance(a: this.transform.position, b: target.transform.position);
            Vector3 directionToPlayer = target.transform.position - this.transform.position;

            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            bool isCoverd = IsPlayerCoverd(directionToPlayer, distanceToPlayer);
            Debug.Log(message: "Player Coverd?" + isCoverd);

            if (!isCoverd && angleToPlayer < viewAngle)
            {
                lol.hasTarget = true;
            }
            else
            {
                lol.hasTarget = false;
            }
        }


    }
    // this way so the face direction of the enemy would show, but it didnt work for me maybe it was a settings issue tho
    bool IsPlayerCoverd(Vector3 direction, float distanceToTarget)
    {
        RaycastHit[] hits = Physics.RaycastAll(origin:this.transform.position, direction, distanceToTarget, layerToCover);

        foreach (RaycastHit hit in hits)
        {
            Debug.DrawRay(start: this.transform.position, dir: direction.normalized * distanceToTarget, Color.green);

            return true;
        }
        Debug.DrawRay(start: this.transform.position, dir: direction.normalized * distanceToTarget, Color.red);
        return false;

    }

}

