using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationButton : MonoBehaviour
{
    public GameObject truck;
    public ChangeVehicles vehicleSelection;

    private void Awake()
    {
        //vehicleSelection = temp.GetComponent<ChangeVehicles>();
        GetComponent<Button>().onClick.AddListener(LaunchAnimation);
    }

    private void LaunchAnimation ()
    {
        //GameObject[] allVehicles = vehicleSelection.vehicles;
        //int currentIdx = vehicleSelection.arrayIdx;
        //truck.GetComponent<Animator>().Play(string.Format("{0}GoesAway", truck.name));
        truck.GetComponent<Animator>().Play("TruckGoesAway");
        if (GameManager.Instance.vehicleCooldown.vehicleReturned)
        {
            GameManager.Instance.VehiclePrefab.GetComponent<Animator>().SetTrigger("Press_in");
        }
    }
}
