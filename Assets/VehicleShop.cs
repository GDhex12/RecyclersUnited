using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleShop : MonoBehaviour
{
    [SerializeField] private List<Vehicle> allVehicles;
    [SerializeField] private List<Button> buyButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyNewVehicle(int id)
	{
		if (allVehicles[id].GetPrice() < 1000)
		{
            //buy
		}
		else
		{
            //
		}


	}



}
