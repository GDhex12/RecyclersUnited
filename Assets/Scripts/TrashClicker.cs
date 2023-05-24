using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashClicker : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Pile"))
                {
                    if (!GameManager.Instance.storage.IsFull())
                    {
                        hit.transform.GetComponent<Animator>().SetTrigger("Press_in");
                        hit.transform.GetComponent<TrashPile>().RemoveTrash();
                        GameManager.Instance.storage.AddGarbage(1);
                        GameManager.Instance.storage.GetComponentInParent<Animator>().SetTrigger("Press_out");
                        GameManager.Instance.experienceManager.experienceToIncrease++;
                    }
                }
                else if (hit.transform.CompareTag("Storage"))
                {
                    if (!GameManager.Instance.vehicle.IsFull() && 
                        !GameManager.Instance.storage.IsEmpty() &&
                        GameManager.Instance.vehicleCooldown.vehicleReturned)
                    {
                        hit.transform.GetComponent<Animator>().SetTrigger("Press_in");
                        GameManager.Instance.storage.RemoveGarbage(1);
                        GameManager.Instance.vehicle.AddGarbage(1);
                        GameManager.Instance.VehiclePrefab.GetComponent<Animator>().SetTrigger("Press_out");
                    }
                }
            }
        }
    }
}
