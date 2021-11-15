using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Animator anim;
    // public Transform[] points; // the array of patrol points
    public Transform player;
    public int health = 10;

    public Transform target;
    public float detectionDistance = 10f;

    private int destPoint = 0;  // the currant point to go to
    private NavMeshAgent agent;

    private bool waiting = false;
    private bool startAttacking = false;
    private bool stopAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Start");
        agent = this.GetComponent<NavMeshAgent>();

        anim.SetInteger("Health", health);

        if(player == null)
        {
            player = GameObject.Find("RigidBodyFPSController").transform;
        }

        // keep it from stopping at each patrol point
        // agent.autoBreaking = false;

        // StartCoroutine(GoToNextPoint());
    }

    // IEnumerator GoToNextPoint()
    // {
    //     Debug.Log("Starting GoToNextPoint");
    //     // if no points exist
    //     if(points.Length == 0)
    //     {
    //        yield return new WaitForEndOfFrame();;   // exit this method()
    //     }

    //     //wait for 2 seconds
    //     Debug.Log("Starting ToWait");
    //     waiting = true;
    //     agent.destination = this.transform.position;
    //     yield return new WaitForSeconds(2);
    //     waiting = false;

    //     // Set the agent to go to the currently selected destination
    //     agent.destination = points[destPoint].position;

    //     // choose the next point in the array as the destination
    //     // cycling to the start if necessary.
    //     destPoint = (destPoint + 1) % points.Length;
    // }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        anim.SetBool("Attacking", startAttacking);

        // when the AI gets close to a destination
        // go to the next point
        // is the NOT operator

        float distanceFromTarget = Vector3.Distance(target.position, this.transform.position);

        if(distanceFromTarget < detectionDistance && startAttacking == false)
        {
            Debug.Log("<color=yellow>I see the target!</color>");
            startAttacking = true;
            stopAttacking = false;
        }

        if(distanceFromTarget > detectionDistance * 1.5f && stopAttacking == false)
        {
            Debug.Log("<color=cyan>Stop attacking!</color>");
            stopAttacking = true;
            startAttacking = false;
        }

        if(startAttacking)
        {
            agent.destination = target.position;
        }

        if(Vector3.Distance(this.transform.position, player.position) > 5)
        {
            Debug.Log("Not Following Player");
            if(!agent.pathPending && agent.remainingDistance < 0.5f && !waiting)
            {
                // StartCoroutine(GoToNextPoint());
            }   
        }
        else
        {
            Debug.Log("Following Player");
            agent.destination = player.position;
        }
        if(health <= 0)
        {    
            agent.destination = this.transform.position;
            Destroy(agent);
            Destroy(gameObject, 2);
        }  
    }

        void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            health -= 5;
            anim.SetInteger("Health", health);
        }
    }
}
