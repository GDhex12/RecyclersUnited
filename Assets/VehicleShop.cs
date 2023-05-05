using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleShop : MonoBehaviour
{
    [SerializeField] private List<Vehicle> allVehicles;
    [SerializeField] private List<Button> buyButtons;
    [SerializeField] private VehicleManager vehicleManager;

    // Start is called before the first frame update
    void Start()
    {
       // BuyNewVehicle(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyNewVehicle(int id=1)
	{

		if (allVehicles[1].GetPrice() < 100000)
		{
            WriteData(id);
            //buy
        }
		else
		{
            //
		}


	}

    void WriteData(int id)
    {
        VehicleDataList vehicleDataList = SaveSystem.LoadShopData();
        foreach(VehicleData vehicle in vehicleDataList.dataList)
		{
			if (vehicle.isSelected)
			{
                vehicle.isSelected = false;
			}
            if(vehicle.id == id)
			{
                vehicle.isSelected = true;
                vehicle.isUnlocked = true;
			}


		}
        SaveSystem.SaveShopData(vehicleDataList);
        vehicleManager.ChangeVehicle();



    }



}
