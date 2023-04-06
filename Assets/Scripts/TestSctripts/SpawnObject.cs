using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnObject : MonoBehaviour
{
    [Header("SpawnBetween")]
    [SerializeField] private GameObject spawnFrom;
    [SerializeField] private GameObject spawnTo;

    [SerializeField] GameObject picker;
    [SerializeField] GameObject loader;
    [SerializeField] private VolunteerCountManager countManager;



    public void SpawnObjectInScene(Transform parent)
    {
        Instantiate(picker, parent.position, Quaternion.identity, parent).SetActive(true);
    }



    public GameObject SpawnObjectInSceneTemporary(Transform parent)
    {
       return Instantiate(picker, parent.position, Quaternion.identity, parent);
    }

    public void SpawnVolunteerInSceneIfAfforded(long price)
    {
        if(CurrencyManager.instance.IsAffordable(price))
        {
            PersistantData.Instance.playerData.VolunteerCount++;

            GameObject temp = Instantiate(picker, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            temp.SetActive(true);
            //Instantiate(volunteer, gameObject.transform.position, Quaternion.identity, gameObject.transform).SetActive(true);

            SaveSystem.SavePlayer(PersistantData.Instance.playerData);
            countManager.AddVolunteersToList(temp);
        }   
    }
    
    public void SpawnLoaderInSceneIfAfforded(long price)
    {
        if(CurrencyManager.instance.IsAffordable(price))
        {
            //PersistantData.Instance.playerData.VolunteerCount++;
            GameObject temp = Instantiate(loader, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            temp.SetActive(true);
            //SaveSystem.SavePlayer(PersistantData.Instance.playerData);
            countManager.AddVolunteersToList(temp);
        }   
    }

    public void SpawnCertainAmountOfVolunteers(Transform parent, int volunteerCount)
    {
        for (int i=0; i<volunteerCount; i++)
        {
            Instantiate(picker, RandomPositionBetween(spawnFrom, spawnTo), Quaternion.identity, parent).SetActive(true);
        }
    }
    private Vector3 RandomPositionBetween(GameObject from, GameObject to)
    {
        Vector3 vector1 = from.transform.position;
        Vector3 vector2 = to.transform.position;

        // Get a random point within the square
        return new Vector3(Random.Range(vector1.x, vector2.x), Random.Range(vector1.y, vector2.y), Random.Range(vector1.z, vector2.z));
    }

    public void IncreasePickerSpeed(float increaseAmount)
    {
        // Find all objects with the MyScript component
        PickerVolunteer[] objectsWithScript = FindObjectsOfType<PickerVolunteer>();

        // Loop through each object and change the value of myVariable
        foreach (PickerVolunteer picker in objectsWithScript)
        {
            picker.GetComponent<NavMeshAgent>().speed += increaseAmount;
        }
       
    }
    public void IncreaseLoaderSpeed(float increaseAmount)
    {
        LoaderVolunteer[] objectsWithScript = FindObjectsOfType<LoaderVolunteer>();

        // Loop through each object and change the value of myVariable
        foreach (LoaderVolunteer loader in objectsWithScript)
        {
            loader.GetComponent<NavMeshAgent>().speed += increaseAmount;
        }
    }
    public void IncreaseLoaderVolunteerBag(int increaseAmount)
    {
        LoaderVolunteer[] objectsWithScript = FindObjectsOfType<LoaderVolunteer>();

        // Loop through each object and change the value of myVariable
        foreach (LoaderVolunteer loader in objectsWithScript)
        {
            loader.bagStorage += increaseAmount;
        }        
    }
}
