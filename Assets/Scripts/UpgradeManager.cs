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
    [SerializeField] UpgradeData vehicleCapacityData = new();
    [SerializeField] UpgradeUI vehicleCapacityUI = new();
    [SerializeField] int vehicleCapacityIncrement = 100;

    [Header("Storage capacity")]
    [SerializeField] UpgradeData storageCapacityData = new();
    [SerializeField] UpgradeUI storageCapacityUI = new();
    [SerializeField] int storageCapacityIncrement = 100;

    [Header("Picker speed")]
    [SerializeField] UpgradeData pickerSpeedData = new();
    [SerializeField] UpgradeUI pickerSpeedUI = new();
    [SerializeField] int pickerSpeedIncrement = 5;

    [Header("Loader speed")]
    [SerializeField] UpgradeData loaderSpeedData = new();
    [SerializeField] UpgradeUI loaderSpeedUI = new();
    [SerializeField] int loaderSpeedIncrement = 5;

    [Header("Loader Bag")]
    [SerializeField] UpgradeData loaderBagData = new();
    [SerializeField] UpgradeUI loaderBagUI = new();
    [SerializeField] int loaderBagIncrement = 1;

    Storage _storage;
    VehicleSystem _vehicleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _storage = FindObjectOfType<Storage>();
        _vehicleSystem = FindObjectOfType<VehicleSystem>();

        vehicleCapacityUI.UpdateUI(vehicleCapacityData);
        storageCapacityUI.UpdateUI(storageCapacityData);
        pickerSpeedUI.UpdateUI(pickerSpeedData);
        loaderSpeedUI.UpdateUI(loaderSpeedData);
        loaderBagUI.UpdateUI(loaderBagData);

        
    }

    public void UpgradeStorageCapacity()
    {
        if (CurrencyManager.instance.IsAffordable(storageCapacityData.price))
        {
            if (storageCapacityData.Upgrade())
            {
                //Save in file
                PersistantData.Instance.playerData.StorageCapacityCurrentLevel = storageCapacityData.currentLvl;
                SaveSystem.SavePlayer(PersistantData.Instance.playerData);
                //Upgrade
                _storage.SetMaxGarbageCount(_storage.GetMaxGarbageCount() + storageCapacityIncrement);
                storageCapacityUI.UpdateUI(storageCapacityData);
            }
        }
    }

    public void UpgradeVehicleCapacity()
    {
        if (CurrencyManager.instance.IsAffordable(vehicleCapacityData.price))
        {
            if (vehicleCapacityData.Upgrade())
            {
                //Save in file
                PersistantData.Instance.playerData.VehicleCapacityCurrentLevel = vehicleCapacityData.currentLvl;
                SaveSystem.SavePlayer(PersistantData.Instance.playerData);
                //Upgrade
                _vehicleSystem.SetMaxGarbageCount(_vehicleSystem.GetMaxGarbageCount() + vehicleCapacityIncrement);
                vehicleCapacityUI.UpdateUI(vehicleCapacityData);
            }
        }
    }

    public void UpgradePickerVolunteerSpeed()
    {
        if (CurrencyManager.instance.IsAffordable(pickerSpeedData.price))
        {
            if (pickerSpeedData.Upgrade())
            {
                //Save in file
                PersistantData.Instance.playerData.PickerSpeedCurrentLevel = pickerSpeedData.currentLvl;
                SaveSystem.SavePlayer(PersistantData.Instance.playerData);
                //Upgrade
                GameManager.Instance.spawner.IncreasePickerSpeed(pickerSpeedIncrement);
                pickerSpeedUI.UpdateUI(pickerSpeedData);
            }
        }
    }
    public void UpgradeLoaderVolunteerSpeed()
    {
        if (CurrencyManager.instance.IsAffordable(loaderSpeedData.price))
        {
            if (loaderSpeedData.Upgrade())
            {
                //Save in file
                PersistantData.Instance.playerData.LoaderSpeedCurrentLevel = loaderSpeedData.currentLvl;
                SaveSystem.SavePlayer(PersistantData.Instance.playerData);
                //Upgrade
                GameManager.Instance.spawner.IncreaseLoaderSpeed(loaderSpeedIncrement);
                loaderSpeedUI.UpdateUI(loaderSpeedData);
            }
        }
    }
    public void UpgradeLoaderVolunteerBag()
    {
        if (CurrencyManager.instance.IsAffordable(loaderBagData.price))
        {
            if (loaderBagData.Upgrade())
            {
                //Save in file
                PersistantData.Instance.playerData.LoaderBagCurrentLevel = loaderBagData.currentLvl;
                SaveSystem.SavePlayer(PersistantData.Instance.playerData);
                //Upgrade
                GameManager.Instance.spawner.IncreaseLoaderVolunteerBag(loaderBagIncrement);
                loaderBagUI.UpdateUI(loaderBagData);
            }
        }
    }
}
