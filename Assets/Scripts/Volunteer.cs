using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Volunteer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Movement Settings")]

    [SerializeField] private GameObject storage;
    [SerializeField] public ExperienceStats refToExpManager;
    [Header("Base volunteer")]
    [SerializeField] private float timeBetweenMoves = 5f;
    [SerializeField] public int[] randomGain;
    [SerializeField] private int bagStorage = 1;
    [Header("Transporter volunteer")]
    [SerializeField] private bool vehicleFillerVolunteer = false;
    [SerializeField] private GameObject vehicle;

    private int bagStorageCurrent = 0;
    private bool stopMoving = false;
    private float distanceThreshold = 1f;

    private bool carryingTrash = false;
    private GameObject trashImGoingTo;



    void Start()
    {
        if(vehicleFillerVolunteer)
        {
            //StartCoroutine(TakeFromStorageAfter(timeBetweenMoves));
        }
        else
        {
            //StartCoroutine(PickUpAfter(timeBetweenMoves));
        }
        
    }
    
    private void Update()
    {

        //Debug.Log(GameManager.Instance.trash.Count);
        if(vehicleFillerVolunteer)
        {
            if (!carryingTrash) // going to storage
            {
                if (GameManager.Instance.storage.IsEmpty())
                {
                    GoToLocation(gameObject);
                    return;
                }
                GoToLocation(storage);
                if (CheckIfClose(storage))
                {
                    bagStorageCurrent = GameManager.Instance.storage.RemoveGarbage(bagStorage);
                    carryingTrash = true;
                }
            }
            else // going to vehicle
            {
                if(GameManager.Instance.vehicle.IsFull() /*|| vehicle isvaziavusi*/)//  if(GameManager.Instance.)
                {
                    GoToLocation(gameObject);
                    return;
                }

                GoToLocation(vehicle);
                if (CheckIfClose(vehicle))
                {
                    Debug.Log("adding to vehicle");
                    GameManager.Instance.vehicle.AddGarbage(bagStorageCurrent);
                    carryingTrash = false;
                }
            }
        }
        else
        {
            if(!carryingTrash) // going to get trash
            {
                if (GameManager.Instance.trash.Count <= 0 && trashImGoingTo == null)
                {
                    GoToLocation(gameObject);
                    return;
                }                    
                if(trashImGoingTo == null)
                {
                    trashImGoingTo = GetTrash();
                    if(trashImGoingTo != null)
                    {
                        GoToLocation(trashImGoingTo);
                    }
                    else
                    {
                        GoToLocation(gameObject);
                    }
                   
                    return;
                }
                if(CloseToDestination())
                {
                    Destroy(trashImGoingTo);
                    carryingTrash = true;
                    GoToLocation(storage);
                }
                
            }
            else // going to storage
            {
                
                if (GameManager.Instance.storage.IsFull())
                {
                    GoToLocation(gameObject);
                    return;
                }               
                GoToLocation(storage);
                if (CloseToDestination())
                {

                    GameManager.Instance.storage.AddGarbage(bagStorage);
                    carryingTrash = false;
                }
            }
        }
    }
    bool CloseToDestination()
    {
        return navMeshAgent.remainingDistance <= distanceThreshold;
    }
    GameObject GetTrash()
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

    private bool CheckIfClose(GameObject targetObject)
    {
        return navMeshAgent.remainingDistance <= distanceThreshold && targetObject != null && Vector3.Distance(transform.position, targetObject.transform.position) <= distanceThreshold;
        

    }
    IEnumerator PickUpAfter(float time)
    {
        //GoToLocation(movementPoints[Random.Range(0, movementPoints.Length)]);
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
