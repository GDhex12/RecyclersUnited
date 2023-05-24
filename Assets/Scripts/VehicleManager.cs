using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allVehiclesPrefabs;
    [SerializeField] private List<Vehicle> allVehiclesSO;


    // Start is called before the first frame update
    void Start()
    {

		if (SaveSystem.IsVehicleDataCreated())
		{
            ReadData();
		}
		else
		{
            WriteData();
 
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WriteData()
	{
        VehicleDataList vehicleDataList = new VehicleDataList();
        foreach (Vehicle so in allVehiclesSO)
		{
			if (so.IsDefault())
			{
                vehicleDataList.dataList.Add(new VehicleData(so.GetId(),true, true));
            }
			else
			{
                vehicleDataList.dataList.Add(new VehicleData(so.GetId(), false, false));
            }
           
        }
        SaveSystem.SaveShopData(vehicleDataList);
        ReadData();
	}

    void ReadData()
	{
        VehicleDataList vehicleDataList = SaveSystem.LoadShopData();
        foreach(VehicleData vehicle in vehicleDataList.dataList)
		{
            if(vehicle.isSelected && vehicle.isUnlocked)
			{
                allVehiclesPrefabs[vehicle.id].SetActive(true);

			}
			else
			{
                allVehiclesPrefabs[vehicle.id].SetActive(false);
            }
		}
        GameManager.Instance.vehicleCooldown.GetComponent<VehicleOutliner>().UpdateOutline();
    }

    public void ChangeVehicle()
	{
        foreach(GameObject obj in allVehiclesPrefabs)
		{
            obj.SetActive(false);
        }
        ReadData();
	}
}
