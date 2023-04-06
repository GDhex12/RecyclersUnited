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
            Instantiate(picker, gameObject.transform.position, Quaternion.identity, gameObject.transform).SetActive(true);
            SaveSystem.SavePlayer(PersistantData.Instance.playerData);
        }   
    }
    
    public void SpawnLoaderInSceneIfAfforded(long price)
    {
        if(CurrencyManager.instance.IsAffordable(price))
        {
            //PersistantData.Instance.playerData.VolunteerCount++;
            Instantiate(loader, gameObject.transform.position, Quaternion.identity, gameObject.transform).SetActive(true);
            //SaveSystem.SavePlayer(PersistantData.Instance.playerData);
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
        picker.GetComponent<NavMeshAgent>().speed += increaseAmount;
    }
    public void IncreaseLoaderSpeed(float increaseAmount)
    {
        loader.GetComponent<NavMeshAgent>().speed += increaseAmount;
    }
    public void IncreaseLoaderVolunteerBag(int increaseAmount)
    {
        loader.GetComponent<Volunteer>().bagStorage += increaseAmount;
    }
}
