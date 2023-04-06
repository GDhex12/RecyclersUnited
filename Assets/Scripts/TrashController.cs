using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TrashController : MonoBehaviour
{
    public static TrashController Instance;
    public NavMeshAgent navMesh;
    [SerializeField] List<TrashPile> trashPiles;

    private void Awake()
    {
        Instance = this;
    }

    public int GetCount()
    {
        return trashPiles.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        trashPiles = FindObjectsOfType<TrashPile>().ToList();
    }

    //for volunteers
    public TrashPile GetRandomPile()
    {
        int index = Random.Range(0, trashPiles.Count);
        return trashPiles[index];
    }

    //for spawning
    public void AddTrashPile(TrashPile pile)
    {
        trashPiles.Add(pile);
    }
    
    //for collected piles
    public void RemoveTrashPile(TrashPile pile)
    {
        trashPiles.Remove(pile);
    }

    //if new items are added on field
    public void RegeneratePickupPoints()
    {
        navMesh.isStopped = true;
        /*
         * TO-DO: clear all NavMesh data and regenerate new paths based on newly spawned trash here
         */
    }
}
