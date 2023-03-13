using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;

    [Header("Spawn References")]
    [SerializeField] GameObject volunteer;


    public void SpawnObjectInScene(Transform parent)
    {
        Instantiate(spawnObject, parent.position, Quaternion.identity, parent);
    }

    public void SpawnVolunteerInSceneIfAfforded(int price)
    {
        if(CurrencyManager.instance.IsAffordable(price))
        {
            PersistantData.Instance.playerData.VolunteerCount++;
            Instantiate(spawnObject, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            SaveSystem.SavePlayer(PersistantData.Instance.playerData);
        }   
    }

}
