using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField] private VolunteerCountManager countManager;
    [SerializeField] private SpawnObject spawnObject;
    [SerializeField] private Transform spawnTransform;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "PowerUp")
                { print("PowerUp!!!");
                    hit.transform.gameObject.GetComponent<PowerUp>().OnClick();
                  
                    countManager.AddVolunteersTemporary(3, 10f);
                    countManager.IncreaseVolunteersSpeed();
                }
            }
        }

        
    }

    private void AddTemporaryVolunteers()
    {
        spawnObject.SpawnObjectInScene(spawnTransform);
    }
}
