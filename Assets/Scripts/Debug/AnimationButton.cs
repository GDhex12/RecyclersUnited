using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationButton : MonoBehaviour
{
    public GameObject temp;
    public ChangeVehicles vehicleSelection;

    private void Awake()
    {
        vehicleSelection = temp.GetComponent<ChangeVehicles>();
        GetComponent<Button>().onClick.AddListener(LaunchAnimation);
    }

    private void LaunchAnimation ()
    {
        GameObject[] allVehicles = vehicleSelection.vehicles;
        int currentIdx = vehicleSelection.arrayIdx;
        allVehicles[currentIdx].GetComponent<Animator>().Play(string.Format("{0}GoesAway", allVehicles[currentIdx].name));
    }
}
