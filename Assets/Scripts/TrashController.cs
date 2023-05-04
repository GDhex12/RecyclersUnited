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

    [Header("Total location trash amount")]
    [SerializeField] int maxTotalTrashAmount = 1000;
    [SerializeField] int currentTotalTrashAmount = 0;
    [SerializeField] int completionPercentage = 100;

    bool _isCompleted = false;

    public bool IsCompleted() 
    { 
        return _isCompleted;
    }

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
        currentTotalTrashAmount = maxTotalTrashAmount;
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

    //----------------- total location trash amount ----------------

    public void DecreaseTotalTrashAmount(int amount)
    {
        if (!_isCompleted)
        {
            currentTotalTrashAmount -= amount;
            completionPercentage = GetCompletionPercentage();
            if (currentTotalTrashAmount <= 0)
            {
                _isCompleted = true;
            }
        }
    }

    public void DecreaseTotalTrashAmount()
    {
        DecreaseTotalTrashAmount(1);
    }

    public int GetCompletionPercentage()
    {
        float max = maxTotalTrashAmount;
        float curr = currentTotalTrashAmount;
        int percentige = (int)Mathf.Round((curr / max) * 100);
        return percentige < 0 ? 0 : percentige;
    }

    public bool NeedsToSpawn()
    {
        int sum = 0;
        foreach (TrashPile pile in trashPiles)
            sum += pile.GetTrashAmount();

        return currentTotalTrashAmount - sum > 0;
    }
}
