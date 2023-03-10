using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;

    public void SpawnObjectInScene(Transform parent)
    {
        Instantiate(spawnObject, parent.position, Quaternion.identity, parent);
    }

    public GameObject SpawnObjectInSceneTemporary(Transform parent)
    {
       return Instantiate(spawnObject, parent.position, Quaternion.identity, parent);
    }
}
