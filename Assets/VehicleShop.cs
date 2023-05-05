using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleShop : MonoBehaviour
{
    [SerializeField] private List<Vehicle> allVehicles;
    [SerializeField] private List<Button> buyButtons;
    [SerializeField] private VehicleManager vehicleManager;
    [SerializeField] private List<GameObject> vehicleRows;

    // Start is called before the first frame update
    void Start()
    {

        CreateShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyNewVehicle(int id)
	{

		if (allVehicles[id].GetPrice() < 100000000)
		{
            WriteData(id);
            CreateShop();
        }



	}
    public void SelectVehilce(int id)
    {


        WriteData(id);
        CreateShop();


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
	public void CreateShop()
	{
        VehicleDataList vehicleDataList = SaveSystem.LoadShopData();
        for (int i=0; i<allVehicles.Count; i++)
		{
            vehicleRows[i].SetActive(true);
            vehicleRows[i].GetComponent<SetUpVehicleRow>().Setup(allVehicles[i], vehicleDataList.dataList[i]);
		}
	}



}
