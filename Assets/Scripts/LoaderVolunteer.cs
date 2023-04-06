using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

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
                return;
            }
            GoToLocation(storage);
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
                return;
            }

            GoToLocation(vehicle);
            if (CheckIfClose(vehicle))
            {
                GameManager.Instance.vehicle.AddGarbage(bagStorageCurrent);
                carryingTrash = false;
                thrashInHand.SetActive(false);
            }
        }
    }
}
