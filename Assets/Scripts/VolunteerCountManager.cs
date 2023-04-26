using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VolunteerCountManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int currrentVolunteersCount = 1;

    private readonly List<int> temporaryAddedVolunteersCount = new();

    [SerializeField] private List<GameObject> temporaryAddedVolunteersList = new();
    [SerializeField] private List<GameObject> allVolunteers = new();

    public List<PickerVolunteer> spawnedPickerVolunteersList = new List<PickerVolunteer>();
    public List<LoaderVolunteer> spawnedLoaderVolunteersList = new List<LoaderVolunteer>();


    [SerializeField] private Transform volunteersSpawnTransform;
    [SerializeField] private SpawnObject spawnObjectScript;
    [SerializeField] private GameObject walkOffPoint;
    [SerializeField] private int saveSystem; //Placeholder for a save system

    [SerializeField] float powerUpSpeedMultiplier = 1.5f;

    void Start()
    {
        LoadVolunteersCount();
    }

    /// <summary>
    /// Used to load Volunteer count from saved data
    /// </summary>
	void LoadVolunteersCount()
	{
        
	}

    /// <summary>
    /// Add volunteers to a total count
    /// </summary>
    /// <param name="volunteersToAdd"></param>
    public void AddVolunteers(int volunteersToAdd)
	{
        currrentVolunteersCount += volunteersToAdd;
        spawnObjectScript.SpawnObjectInScene(volunteersSpawnTransform);
	}
    public void AddVolunteersToList(GameObject volunteer)
    {
        currrentVolunteersCount += 1;
        allVolunteers.Add(volunteer);

        if(volunteer.GetComponent<PickerVolunteer>() != null)
        {
            spawnedPickerVolunteersList.Add(volunteer.GetComponent<PickerVolunteer>());
        }
        if (volunteer.GetComponent<LoaderVolunteer>() != null)
        {
            spawnedLoaderVolunteersList.Add(volunteer.GetComponent<LoaderVolunteer>());
        }
    }

    public void AddVolunteersTemporary(int volunteersToAdd, float time)
	{
        currrentVolunteersCount += volunteersToAdd;
        temporaryAddedVolunteersCount.Add( volunteersToAdd);
        FunctionTimer.Create(DecreaseVolunteersCount, time);
        for(int i=0; i<volunteersToAdd; i++)
        {
            GameObject tempObj = spawnObjectScript.SpawnObjectInSceneTemporary(volunteersSpawnTransform);
            tempObj.SetActive(true);
            temporaryAddedVolunteersList.Add(tempObj);
        }
        
    }

    public void DecreaseVolunteersCount()
	{
       
        for(int i=0; i < temporaryAddedVolunteersCount[0]; i++)
        {
            
            temporaryAddedVolunteersList[i].GetComponent<Volunteer>().WalkOutOfMap(walkOffPoint.transform.position);
            


        }
        int count = temporaryAddedVolunteersCount[0];
        temporaryAddedVolunteersList.RemoveRange(0, count);
        temporaryAddedVolunteersCount.RemoveAt(0);


    }

    public void IncreaseVolunteersSpeed()
    {

        foreach (GameObject obj in allVolunteers)
		{
            obj.GetComponent<NavMeshAgent>().speed = obj.GetComponent<NavMeshAgent>().speed * powerUpSpeedMultiplier;
        }
        foreach (GameObject obj in temporaryAddedVolunteersList)
        {
            obj.GetComponent<NavMeshAgent>().speed = obj.GetComponent<NavMeshAgent>().speed * powerUpSpeedMultiplier;
        }
        FunctionTimer.Create(DecreaseVolunteersSpeed, 10f);
	}

    public void DecreaseVolunteersSpeed()
    {

        foreach (GameObject obj in allVolunteers)
        {
            obj.GetComponent<NavMeshAgent>().speed = obj.GetComponent<NavMeshAgent>().speed / powerUpSpeedMultiplier;
        }
        foreach (GameObject obj in temporaryAddedVolunteersList)
        {
            obj.GetComponent<NavMeshAgent>().speed = obj.GetComponent<NavMeshAgent>().speed / powerUpSpeedMultiplier;
        }
        //FunctionTimer.Create(DecreaseVolunteersSpeed, 10f);
    }
    /// <summary>
    /// Returns a volunteers count
    /// </summary>
    /// <returns></returns>
    public int ReturnVolunteersCount()
	{
        return currrentVolunteersCount;
	}


}
