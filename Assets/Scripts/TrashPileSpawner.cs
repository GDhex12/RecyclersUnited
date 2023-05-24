using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPileSpawner : MonoBehaviour
{
    [Header("Main references")]
    public Transform parent;
    public List<TrashPile> possibleVariants = new();

    [Header("Random wait times (in seconds)")]
    public int timeToSpawnMin;
    public int timeToSpawnMax;

    public void LaunchRespawn (Vector3 removedLoc)
    {
        //spawn trash pile if total trash amount is bigger than 0
        if (TrashController.Instance.NeedsToSpawn())
        {
            StartCoroutine(InitiateNewSpawn(removedLoc));
        }
    }

    public IEnumerator InitiateNewSpawn (Vector3 removedLoc)
    {
        yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));
        if (TrashController.Instance.NeedsToSpawn())
        {
            GameObject newTrash = Instantiate(possibleVariants[Random.Range(0, possibleVariants.Count - 1)].gameObject, removedLoc, Quaternion.identity, parent);
            //newTrash.GetComponent<Animation>().Play();
            TrashPile newPile = newTrash.GetComponent<TrashPile>();
            TrashController.Instance.AddTrashPile(newPile);
            Debug.Log(newPile.GetTrashAmount());
        }
    }
}
