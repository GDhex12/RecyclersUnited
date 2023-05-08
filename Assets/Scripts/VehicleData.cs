using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VehicleData 
{
    public int id;
    public bool isUnlocked;
    public bool isSelected;
    public VehicleData(int id, bool isUnlocked, bool isSelected)
    {
        this.id = id;
        this.isSelected = isSelected;
        this.isUnlocked = isUnlocked;
    }

    public VehicleData()
    {
        
    }
}

[System.Serializable]
public class VehicleDataList
{
    public List<VehicleData> dataList;
    public VehicleDataList()
    {
        dataList = new List<VehicleData>();
    }
    
    
}


