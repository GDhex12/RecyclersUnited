using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObject : MonoBehaviour
{
    [Header("SpawnBetween")]
    [SerializeField] private GameObject spawnFrom;
    [SerializeField] private GameObject spawnTo;

    [SerializeField] GameObject volunteer;
    [SerializeField] GameObject loader;



    public void SpawnObjectInScene(Transform parent)
    {
        Instantiate(volunteer, parent.position, Quaternion.identity, parent).SetActive(true);
    }



    public GameObject SpawnObjectInSceneTemporary(Transform parent)
    {
       return Instantiate(volunteer, parent.position, Quaternion.identity, parent);
    }

    public void SpawnVolunteerInSceneIfAfforded(long price)
    {
        if(CurrencyManager.instance.IsAffordable(price))
        {
            PersistantData.Instance.playerData.VolunteerCount++;
            Instantiate(volunteer, gameObject.transform.position, Quaternion.identity, gameObject.transform).SetActive(true);
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
            Instantiate(volunteer, RandomPositionBetween(spawnFrom, spawnTo), Quaternion.identity, parent).SetActive(true);
        }
    }
    private Vector3 RandomPositionBetween(GameObject from, GameObject to)
    {
        Vector3 vector1 = from.transform.position;
        Vector3 vector2 = to.transform.position;

        // Get a random point within the square
        return new Vector3(Random.Range(vector1.x, vector2.x), Random.Range(vector1.y, vector2.y), Random.Range(vector1.z, vector2.z));
    }
}
