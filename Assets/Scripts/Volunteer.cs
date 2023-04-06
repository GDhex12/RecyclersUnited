using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Volunteer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Movement Settings")]

    [SerializeField] public GameObject storage;
    [Header("Exp Settings")]
    [SerializeField] public ExperienceStats refToExpManager;

    [SerializeField] public int[] randomGain;
    [SerializeField] public int bagStorage = 1;


    [Header("Shared Information")]
    public int bagStorageCurrent = 0;
    private bool stopMoving = false;
    private float distanceThreshold = 1f;

    public bool carryingTrash = false;



    public GameObject thrashInHand; // for enabling thrash model when volunteer is coming back

    void Start()
    {
        thrashInHand = transform.Find("pickedupThrash").gameObject; //gets thrash GameObject
        
    }

    public bool CloseToDestination()
    {
        return navMeshAgent.remainingDistance <= distanceThreshold;
    }
    public GameObject GetTrash()
    {
        return GameManager.Instance.trash.Dequeue();
    }
    public void MoveTo(Vector3 moveLocation)
    {
        navMeshAgent.SetDestination(moveLocation);
    }
    public void GoToLocation(GameObject location)
    {
        MoveTo(location.transform.position);
    }

    public bool CheckIfClose(GameObject targetObject)
    {
        return navMeshAgent.remainingDistance <= distanceThreshold && targetObject != null && Vector3.Distance(transform.position, targetObject.transform.position) <= distanceThreshold;
    }

    public void WalkOutOfMap(Vector3 walkOffPoint)
	{
        //Destroy(gameObject);
        StartCoroutine(WalkOffMapAndDestroy(walkOffPoint));
	}

    IEnumerator WalkOffMapAndDestroy(Vector3 walkOffPoint)
    {
        MoveTo(walkOffPoint);
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }

}
