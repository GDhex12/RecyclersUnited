using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VolunteerCountManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int currrentVolunteersCount = 1;

    private List<int> temporaryAddedVolunteersCount =new List<int>();

    [SerializeField] private List<GameObject> temporaryAddedVolunteersList = new List<GameObject>();

    [SerializeField] private Transform volunteersSpawnTransform;
    [SerializeField] private SpawnObject spawnObjectScript;
    [SerializeField] private GameObject walkOffPoint;
    [SerializeField]
    private int saveSystem;//Placeholder for a save system
    void Start()
    {
        LoadVolunteersCount();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void AddVolunteersTemporary(int volunteersToAdd, float time)
	{
        currrentVolunteersCount += volunteersToAdd;
        temporaryAddedVolunteersCount.Add( volunteersToAdd);
        FunctionTimer.Create(DecreaseVolunteersCount, time);
        for(int i=0; i<volunteersToAdd; i++)
        {
            
            temporaryAddedVolunteersList.Add(spawnObjectScript.SpawnObjectInSceneTemporary(volunteersSpawnTransform));
        }
        
    }

    public void DecreaseVolunteersCount()
	{
       
        for(int i=0; i < temporaryAddedVolunteersCount[0]; i++)
        {
            
            temporaryAddedVolunteersList[i].gameObject.GetComponent<Volunteer>().WalkOutOfMap(walkOffPoint.transform.position);
            


        }
        int count = temporaryAddedVolunteersCount[0];
        temporaryAddedVolunteersList.RemoveRange(0, count);
        temporaryAddedVolunteersCount.RemoveAt(0);


    }

    public void IncreaseVolunteersSpeed()
	{
       GameObject[] allVolunteers =  GameObject.FindGameObjectsWithTag("Volunteer");

        for(int i =0; i< allVolunteers.Length; i++)
		{
            allVolunteers[i].GetComponent<NavMeshAgent>().speed = allVolunteers[i].GetComponent<NavMeshAgent>().speed * 1.2f;

        }
        FunctionTimer.Create(DecreaseVolunteersSpeed, 10f);
	}

    public void DecreaseVolunteersSpeed()
    {
        GameObject[] allVolunteers = GameObject.FindGameObjectsWithTag("Volunteer");

        for (int i = 0; i < allVolunteers.Length; i++)
        {
            allVolunteers[i].GetComponent<NavMeshAgent>().speed = allVolunteers[i].GetComponent<NavMeshAgent>().speed / 1.2f;

        }
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
