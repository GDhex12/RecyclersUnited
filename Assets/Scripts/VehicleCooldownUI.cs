using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCooldownUI : MonoBehaviour
{
    public void FastenCar()
    {
        GameManager.Instance.vehicleCooldown.FastenTimer(0.5f);
    }
}
