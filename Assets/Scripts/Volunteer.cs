using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Volunteer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Movement Settings")]
    [SerializeField] private GameObject[] movementPoints;
    [SerializeField] private GameObject storage;
    [SerializeField] private float timeBetweenMoves = 5f;

    [SerializeField] private ExperienceStats refToExp;
    [SerializeField] public int[] randomGain;

    private void Awake()
    {
    }
    void Start()
    {
        refToExp = GetComponent<ExperienceStats>();
        StartCoroutine(PickUpAfter(timeBetweenMoves));
    }

    public void MoveTo(Vector3 moveLocation)
    {
        navMeshAgent.SetDestination(moveLocation);
    }
    public void GoToPickUpLocation()
    {
        int randomInt = Random.Range(0, movementPoints.Length);
        MoveTo(movementPoints[randomInt].transform.position);
    }
    public void GoToPutDownLocation()
    {
        MoveTo(storage.transform.position);
    }

    IEnumerator PickUpAfter(float time)
    {      
        GoToPickUpLocation();
        yield return new WaitForSeconds(time);
        refToExp.experienceToIncrease += Random.Range(randomGain[0], randomGain[1]);
        StartCoroutine(PutDownAfter(time));
    }
    IEnumerator PutDownAfter(float time)
    {       
        GoToPutDownLocation();
        yield return new WaitForSeconds(time);
        //arvyvdo demo addition
        storage.GetComponent<Storage>().AddGarbage(1);
        StartCoroutine(PickUpAfter(time));
    }

}
