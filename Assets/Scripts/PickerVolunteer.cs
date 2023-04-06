using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PickerVolunteer : Volunteer
{
    private GameObject trashImGoingTo;
    private void Update()
    {
        if (!carryingTrash) // going to get trash
        {
            if (GameManager.Instance.trash.Count <= 0 && trashImGoingTo == null)
            {
                GoToLocation(gameObject);
                return;
            }
            if (trashImGoingTo == null)
            {
                trashImGoingTo = GetTrash();
                if (trashImGoingTo != null)
                {
                    GoToLocation(trashImGoingTo);
                }
                else
                {
                    GoToLocation(gameObject);
                }

                return;
            }
            if (CloseToDestination())
            {
                Destroy(trashImGoingTo);
                carryingTrash = true;
                thrashInHand.SetActive(true);
                GoToLocation(storage);
            }

        }
        else // going to storage
        {

            if (GameManager.Instance.storage.IsFull())
            {
                GoToLocation(gameObject);
                return;
            }
            GoToLocation(storage);
            if (CloseToDestination())
            {

                GameManager.Instance.storage.AddGarbage(bagStorage);
                carryingTrash = false;
                thrashInHand.SetActive(false);
            }
        }
    }
}
