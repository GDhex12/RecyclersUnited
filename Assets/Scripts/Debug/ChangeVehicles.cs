using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeVehicles : MonoBehaviour
{
    public GameObject[] vehicles;
    public int arrayIdx;

    [SerializeField] private TMP_Dropdown dd;

    private void Awake()
    {
        dd = GetComponent<TMP_Dropdown>();
        vehicles[dd.value].SetActive(true);
        arrayIdx = dd.value;
    }

    public void OnDropdownChanged (int value)
    {
        vehicles[arrayIdx].SetActive(false);
        vehicles[value].SetActive(true);
        arrayIdx = value;
        //Debug.Log(string.Format("Field changed to {0}", value));
    }
}
