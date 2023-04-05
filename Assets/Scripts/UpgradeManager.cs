using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UpgradeManager : MonoBehaviour
{
    [Serializable] 
    public class UpgradeData
    {
        public string UpgradeName;
        public float priceMultiplier = 1.2f;
        public int currentLvl = 1;
        public int maxLvl = 10;
        public int initPrice = 10;
        public long price;

        public UpgradeData()
        {
            //currentLvl = (save file data)
            price = (long)(initPrice * Mathf.Pow(priceMultiplier, currentLvl-1));
        }

        public bool Upgrade()
        {
            if (currentLvl < maxLvl)
            {
                CurrencyManager.instance.RemoveCurrency(price);
                price = (long)(price * priceMultiplier);
                currentLvl++;
                return true;
            }
            else
            {
                Debug.LogWarning("Already max level");
                return false;
            }
        }

        public string BtnStringFormat()
        {
            return $"-{CurrencyManager.instance.CurrencyStringFormat(price)}";
        }
    }

    [Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI TitleText;
        public TextMeshProUGUI BtnText;

        public void UpdateUI(UpgradeData data)
        {
            if (TitleText!= null)
            {
                TitleText.text = data.UpgradeName;
            }

            if (BtnText!= null)
            {
                BtnText.text = data.BtnStringFormat();
            }
        }
    }

    [Header("Vehicle capacity")]
    [SerializeField] UpgradeData vehicleCapacityData = new UpgradeData();
    [SerializeField] UpgradeUI vehicleCapacityUI = new UpgradeUI();
    [SerializeField] int vehicleCapacityIncrement = 100;

    [Header("Storage capacity")]
    [SerializeField] UpgradeData storageCapacityData = new UpgradeData();
    [SerializeField] UpgradeUI storageCapacityUI = new UpgradeUI();
    [SerializeField] int storageCapacityIncrement = 100;

    Storage _storage;
    VehicleSystem _vehicleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _storage = FindObjectOfType<Storage>();
        _vehicleSystem = FindObjectOfType<VehicleSystem>();

        vehicleCapacityUI.UpdateUI(vehicleCapacityData);
        storageCapacityUI.UpdateUI(storageCapacityData);

        
    }

    public void UpgradeStorageCapacity()
    {
        if (CurrencyManager.instance.IsAffordable(storageCapacityData.price))
        {
            if (storageCapacityData.Upgrade())
            {
                _storage.SetMaxGarbageCount(_storage.GetMaxGarbageCount() + storageCapacityIncrement);
                storageCapacityUI.UpdateUI(storageCapacityData);
            }
        }
    }

    void SetStorageCapacity(int n)
    {
        if (_storage != null)
        {
            _storage.SetMaxGarbageCount(n);
        }
        else
        {
            Debug.LogWarning("Storage not set");
        }
    }

    public void UpgradeVehicleCapacity()
    {
        if (CurrencyManager.instance.IsAffordable(vehicleCapacityData.price))
        {
            if (vehicleCapacityData.Upgrade())
            {
                _vehicleSystem.SetMaxGarbageCount(_vehicleSystem.GetMaxGarbageCount() + vehicleCapacityIncrement);
                vehicleCapacityUI.UpdateUI(vehicleCapacityData);
            }
        }
    }

    void SetVehicleCapacity(int n)
    {
        if (_vehicleSystem != null)
        {
            _vehicleSystem.SetMaxGarbageCount(n);
        }
        else
        {
            Debug.LogWarning("Vehicle system not set");
        }
    }
}
