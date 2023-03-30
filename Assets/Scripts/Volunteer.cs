using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Volunteer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Movement Settings")]
    [Header("Normal volunteer")]
    [SerializeField] private GameObject[] movementPoints;    
    [SerializeField] private GameObject storage;
    [SerializeField] public ExperienceStats refToExpManager;
    
    [SerializeField] private float timeBetweenMoves = 5f;
    [SerializeField] public int[] randomGain;
    [SerializeField] private int bagStorage = 1;
    [Header("Transporter volunteer")]
    [SerializeField] private bool vehicleFillerVolunteer = false;
    [SerializeField] private GameObject vehicle;

    private int bagStorageCurrent = 0;
    private bool stopMoving = false;
    private float distanceThreshold = 1f;

    void Start()
    {
        if(vehicleFillerVolunteer)
        {
            StartCoroutine(TakeFromStorageAfter(timeBetweenMoves));
        }
        else
        {
            StartCoroutine(PickUpAfter(timeBetweenMoves));
        }
        
    }

    public void MoveTo(Vector3 moveLocation)
    {
        navMeshAgent.SetDestination(moveLocation);
    }
    public void GoToLocation(GameObject location)
    {
        MoveTo(location.transform.position);
    }

    private void CheckIfClose(GameObject targetObject)
    {
        if (navMeshAgent.remainingDistance <= distanceThreshold && targetObject != null && Vector3.Distance(transform.position, targetObject.transform.position) <= distanceThreshold)
        {
            // NavMeshAgent is close to the target object
            // Do something here, such as stopping the NavMeshAgent or triggering an event
        }
    }
    IEnumerator PickUpAfter(float time)
    {
        GoToLocation(movementPoints[Random.Range(0, movementPoints.Length)]);
        yield return new WaitForSeconds(time);
        refToExpManager.experienceToIncrease += Random.Range(randomGain[0], randomGain[1]);
        StartCoroutine(PutDownAfter(time));
    }
    IEnumerator PutDownAfter(float time)
    {
        GoToLocation(storage);
        yield return new WaitForSeconds(time);
        storage.GetComponent<Storage>().AddGarbage(bagStorage);
        StartCoroutine(PickUpAfter(time));
    }
    IEnumerator TakeFromStorageAfter(float time)
    {
        GoToLocation(storage);
        yield return new WaitForSeconds(time);
        bagStorageCurrent = storage.GetComponent<Storage>().RemoveGarbage(bagStorage);
        StartCoroutine(PutToTruckAfterAfter(time));
    }
    IEnumerator PutToTruckAfterAfter(float time)
    {
        GoToLocation(vehicle);
        yield return new WaitForSeconds(time);
        vehicle.GetComponent<VehicleSystem>().AddGarbage(bagStorageCurrent);
        StartCoroutine(TakeFromStorageAfter(time));
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
