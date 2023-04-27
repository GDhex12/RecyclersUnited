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

    public void SpawnVolunteerPickerInSceneIfAfforded(long price)
    {
        if(CurrencyManager.instance.IsAffordable(price))
        {
            PersistantData.Instance.sceneData.VolunteerPickerCount++;

            GameObject temp = Instantiate(picker, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            temp.SetActive(true);
            //Instantiate(volunteer, gameObject.transform.position, Quaternion.identity, gameObject.transform).SetActive(true);

            SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
            countManager.AddVolunteersToList(temp);
        }   
    }
    
    public void SpawnLoaderInSceneIfAfforded(long price)
    {
        if(CurrencyManager.instance.IsAffordable(price))
        {
            PersistantData.Instance.sceneData.VolunteerLoaderCount++;
            GameObject temp = Instantiate(loader, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            temp.SetActive(true);
            SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
            countManager.AddVolunteersToList(temp);
        }   
    }

    public void SpawnCertainAmountOfPickerVolunteers(int volunteerCount)
    {
        for (int i=0; i<volunteerCount; i++)
        {
            GameObject temp = Instantiate(picker, RandomPositionBetween(spawnFrom, spawnTo), Quaternion.identity, gameObject.transform);
            temp.SetActive(true);
            countManager.AddVolunteersToList(temp);
        }
    }
    public void SpawnCertainAmountOfLoaderVolunteers(int volunteerCount)
    {
        for (int i = 0; i < volunteerCount; i++)
        {
            GameObject temp = Instantiate(loader, RandomPositionBetween(spawnFrom, spawnTo), Quaternion.identity, gameObject.transform);
            temp.SetActive(true);
            countManager.AddVolunteersToList(temp);
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
        for (int i = 0; i < countManager.spawnedPickerVolunteersList.Count; i++)
        {
            countManager.spawnedPickerVolunteersList[i].GetComponent<NavMeshAgent>().speed += increaseAmount;
        }
        picker.GetComponent<NavMeshAgent>().speed += increaseAmount;       
    }
    public void IncreaseLoaderSpeed(float increaseAmount)
    {
        for(int i = 0; i< countManager.spawnedLoaderVolunteersList.Count;i++)
        {
            countManager.spawnedLoaderVolunteersList[i].GetComponent<NavMeshAgent>().speed += increaseAmount;
        }
        loader.GetComponent<NavMeshAgent>().speed += increaseAmount;
    }
    public void IncreaseLoaderVolunteerBag(int increaseAmount)
    {
        for (int i = 0; i < countManager.spawnedLoaderVolunteersList.Count; i++)
        {
            countManager.spawnedLoaderVolunteersList[i].bagStorage += increaseAmount;
        }
        loader.GetComponent<LoaderVolunteer>().bagStorage += increaseAmount;
    }
}
