using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : MonoBehaviour
{
    [SerializeField] List<GameObject> trashList;
    int _trashIndex;


    // Start is called before the first frame update
    void Start()
    {
        if(trashList.Count > 0)
        {
            _trashIndex = trashList.Count - 1;
        }
        else
        {
            Debug.LogWarning($"Empty list in {gameObject.name}");
            gameObject.SetActive(false);
        }
    }

    public void RemoveTrash()
    {
        trashList[_trashIndex].SetActive(false);
        _trashIndex--;

        if(_trashIndex < 0)
        {
            Destroy(gameObject);
        }
    }
}
