using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : MonoBehaviour
{
    [SerializeField] List<GameObject> trashList;
    [SerializeField] int trashAmount;
    public TrashPileSpawner trashSpawner;
    int _trashIndex;
    int _trashPerChange, _trashLeftAfterDivision;
    int _trashTaken = 0;


    // Start is called before the first frame update
    void Start()
    {

        if(trashList.Count > 0)
        {
            if (trashAmount < trashList.Count)
                trashAmount = trashList.Count;

            _trashIndex = trashList.Count - 1;
            trashSpawner = FindObjectOfType<TrashPileSpawner>();
            _trashPerChange = trashAmount / trashList.Count;
            _trashLeftAfterDivision = trashAmount % trashList.Count;
        }
        else
        {
            Debug.LogWarning($"Empty list in {gameObject.name}");
            gameObject.SetActive(false);
        }
    }

    //evens amount of trash to take between visual change
    public void RemoveTrash()
    {
        trashAmount--;
        _trashTaken++;
        int trashToTake = _trashLeftAfterDivision > 0 ? _trashPerChange + 1 : _trashPerChange;
        if (trashToTake <= _trashTaken)
        {
            ReduceVisualy();
            if (_trashLeftAfterDivision > 0)
                _trashLeftAfterDivision--;
            _trashTaken = 0;
        }

        if(trashAmount <= 0)
        {
            trashSpawner.LaunchRespawn(gameObject.transform.position);
            TrashController.Instance.RemoveTrashPile(this);
            Destroy(gameObject);
        }
    }

    public void RemoveTrash(int amount)
    {
        for (int i=0; i < amount; i++)
        {
            RemoveTrash();
        }
    }

    void ReduceVisualy()
    {
        trashList[_trashIndex].SetActive(false);
        _trashIndex--;
    }
}
