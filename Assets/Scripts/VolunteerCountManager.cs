using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerCountManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int currrentVolunteersCount = 0;

    private int temporaryAddedVolunteers = 0;

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
	}

    public void AddVolunteersTemporary(int volunteersToAdd, float time)
	{
        currrentVolunteersCount += volunteersToAdd;
        temporaryAddedVolunteers = volunteersToAdd;
        FunctionTimer.Create(DecreaseVolunteersCount, time);
    }

    public void DecreaseVolunteersCount()
	{
        currrentVolunteersCount -= temporaryAddedVolunteers;
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
