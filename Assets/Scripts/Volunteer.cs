using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Volunteer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Movement Settings")]
    [SerializeField] private GameObject[] movementPoints;
    [SerializeField] private GameObject walkOffPoint;

    [SerializeField] private GameObject storage;
    [SerializeField] private GameObject vehicle;
    [SerializeField] private float timeBetweenMoves = 5f;

    [SerializeField] public ExperienceStats refToExpManager;
    [SerializeField] public int[] randomGain;
    [SerializeField] private bool vehicleFillerVolunteer = false;
    [SerializeField] private int bagStorage = 1;
    private int bagStorageCurrent = 0;

    private GameObject thrashInHand; // for enabling thrash model when volunteer is coming back

    void Start()
    {
        thrashInHand = transform.Find("pickedupThrash").gameObject; //gets thrash GameObject

        if (vehicleFillerVolunteer)
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


    IEnumerator PickUpAfter(float time)
    {
        thrashInHand.SetActive(false);
        GoToLocation(movementPoints[Random.Range(0, movementPoints.Length)]);
        yield return new WaitForSeconds(time);
        refToExpManager.experienceToIncrease += Random.Range(randomGain[0], randomGain[1]);
        StartCoroutine(PutDownAfter(time));
    }
    IEnumerator PutDownAfter(float time)
    {
        thrashInHand.SetActive(true);
        GoToLocation(storage);
        yield return new WaitForSeconds(time);
        storage.GetComponent<Storage>().AddGarbage(bagStorage);
        StartCoroutine(PickUpAfter(time));
    }
    IEnumerator TakeFromStorageAfter(float time)
    {
        thrashInHand.SetActive(false);
        GoToLocation(storage);
        yield return new WaitForSeconds(time);
        bagStorageCurrent = storage.GetComponent<Storage>().RemoveGarbage(bagStorage);
        StartCoroutine(PutToTruckAfterAfter(time));
    }
    IEnumerator PutToTruckAfterAfter(float time)
    {
        thrashInHand.SetActive(true);
        GoToLocation(vehicle);
        yield return new WaitForSeconds(time);
        vehicle.GetComponent<VehicleSystem>().AddGarbage(bagStorageCurrent);
        StartCoroutine(TakeFromStorageAfter(time));
    }

    public void WalkOutOfMap(Vector3 walkOffPoint)
	{
        StopAllCoroutines();
        StartCoroutine(WalkOffMapAndDestroy(walkOffPoint));
	}

    IEnumerator WalkOffMapAndDestroy(Vector3 walkOffPoint)
    {
        
        MoveTo(walkOffPoint);
        navMeshAgent.SetDestination(walkOffPoint);
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }

}
