using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform target;

    UnityEngine.AI.NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if(Vector3.Distance(this.transform.position, target.position) < 30)
        {
            agent.destination = target.position;
        }
    }
}
