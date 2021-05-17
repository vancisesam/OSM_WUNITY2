using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    //public float range = 100.0f;
    private Vector3 safety;
    private NavMeshPath path;
    public float range = 10.0f;

    [SerializeField] private NavMeshAgent agent; //set in inspector to reduce spawn time


    public NavMeshPath GetPath(GameObject target)
    {
        
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        safety = target.transform.position;
        //agent.destination = safety.position;
        //NavMesh.CalculatePath(transform.position, safety.position, NavMesh.AllAreas, path);
        agent.SetDestination(safety);
        return path;
        
    }

}