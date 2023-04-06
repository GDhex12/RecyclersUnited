using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject volunteer;
    [SerializeField] GameObject loader;
    [SerializeField] private VolunteerCountManager countManager;



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
            GameObject temp = Instantiate(volunteer, gameObject.transform.position, Quaternion.identity, gameObject.transform);
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
            Instantiate(volunteer, parent.position, Quaternion.identity, parent);
        }
    }
}
