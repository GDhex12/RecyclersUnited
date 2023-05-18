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
                    SetAnimationVelocity(0);
                    return;
                }
                if (trashImGoingTo == null)
                {
                    trashImGoingTo = TrashController.Instance.GetRandomPile();
                    if (trashImGoingTo != null)
                    {
                        GoToLocation(trashImGoingTo.gameObject);
                        SetAnimationVelocity(1);
                    }
                    else
                    {
                        GoToLocation(gameObject);
                        SetAnimationVelocity(0);
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
                    SetAnimationVelocity(1);

                }

            }
            
        }
        else // going to storage
        {

            if (GameManager.Instance.storage.IsFull())
            {
                GoToLocation(gameObject);
                SetAnimationVelocity(0);
                return;
            }
            GoToLocation(storage);
            SetAnimationVelocity(1);
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
