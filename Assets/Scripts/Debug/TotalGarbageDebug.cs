using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGarbageDebug : MonoBehaviour
{
    public void ResetTotalGarbageCount()
    {
        TrashController.Instance.ResetTotalGarbageCount();
    }

    
}
