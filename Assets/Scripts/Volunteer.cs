using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Volunteer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent.SetDestination(new Vector3(-3f,0f,-3));
    }

    public void MoveTo(Vector3 moveLocation)
    {
        navMeshAgent.SetDestination(moveLocation);
    }
}
