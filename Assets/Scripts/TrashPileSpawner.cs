using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPileSpawner : MonoBehaviour
{
    [Header("Main references")]
    public List<TrashPile> possibleVariants = new();

    [Header("Random wait times (in seconds)")]
    public int timeToSpawnMin;
    public int timeToSpawnMax;

    public void LaunchRespawn (Vector3 removedLoc)
    {
        StartCoroutine(InitiateNewSpawn(removedLoc));
    }

    public IEnumerator InitiateNewSpawn (Vector3 removedLoc)
    {
        yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));
        Instantiate(possibleVariants[Random.Range(0, possibleVariants.Count-1)].gameObject, removedLoc, Quaternion.identity);
    }
}
