using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerVolunteer : Volunteer
{
    private TrashPile trashImGoingTo;

    private void Update()
    {   
        if (!carryingTrash) // going to get trash
        {
            if (!isGoingOfMap)
            {
                if (TrashController.Instance.GetCount() <= 0 && trashImGoingTo == null)
                {
                    GoToLocation(gameObject);
                    return;
                }
                if (trashImGoingTo == null)
                {
                    trashImGoingTo = TrashController.Instance.GetRandomPile();
                    if (trashImGoingTo != null)
                    {
                        GoToLocation(trashImGoingTo.gameObject);
                    }
                    else
                    {
                        GoToLocation(gameObject);
                    }
                    return;
                }
                if (CloseToDestination())
                {

                    //Destroy(trashImGoingTo);
                    // trashImGoinTo -1 right now
                    trashImGoingTo.RemoveTrash();
                    trashImGoingTo = null;
                    carryingTrash = true;
                    thrashInHand.SetActive(true);
                    GoToLocation(storage);

                }

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
                refToExpManager.experienceToIncrease ++;
                carryingTrash = false;
                thrashInHand.SetActive(false);
                if (isGoingOfMap)
                {
                    MoveTo(walkOffPoint);
                }

            }
        }

    }
}
