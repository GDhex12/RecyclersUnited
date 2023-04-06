using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    public static TrashController Instance;

    [SerializeField] List<TrashPile> trashPiles;

    private void Awake()
    {
        Instance = this;
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
}
