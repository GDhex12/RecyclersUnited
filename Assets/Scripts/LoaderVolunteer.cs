using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class LoaderVolunteer : Volunteer
{
    [Header("Loader Settings")]
    [SerializeField] private GameObject vehicle;

    private void Update()
    {
        if (!carryingTrash) // going to storage
        {
            if (GameManager.Instance.storage.IsEmpty())
            {
                GoToLocation(gameObject);
                SetAnimationVelocity(0);
                return;
            }
            GoToLocation(storage);
            SetAnimationVelocity(1);
            if (CheckIfClose(storage))
            {
                bagStorageCurrent = GameManager.Instance.storage.RemoveGarbage(bagStorage);
                carryingTrash = true;
                thrashInHand.SetActive(true);
            }
        }
        else // going to vehicle
        {
            if (GameManager.Instance.vehicle.IsFull() || !GameManager.Instance.vehicleCooldown.vehicleReturned)
            {
                GoToLocation(gameObject);
                SetAnimationVelocity(0);
                return;
            }

            GoToLocation(vehicle);
            SetAnimationVelocity(1);
            if (CheckIfClose(vehicle))
            {
                GameManager.Instance.vehicle.AddGarbage(bagStorageCurrent);
                carryingTrash = false;
                thrashInHand.SetActive(false);
            }
        }
        
    }
}
