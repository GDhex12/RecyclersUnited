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
            price = initPrice;
        }

        public void SetCurrLvl(int currentLvl)
        {
            this.currentLvl = currentLvl;
            price = (long)(initPrice * Mathf.Pow(priceMultiplier, currentLvl - 1));
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

        SetupData();

        
    }

    void SetupData()
    {
        LoadStorageCapacity();
        LoadVehicleCapacity();
        LoadPickerSpeed();
        LoadLoaderSpeed();
        LoadLoaderBag();
    }

    //------------------Upgrade--------------------
    public void UpgradeStorageCapacity()
    {
        if (CurrencyManager.instance.IsAffordable(storageCapacityData.price))
        {
            if (storageCapacityData.Upgrade())
            {
                //Save in file
                PersistantData.Instance.sceneData.StorageCapacityCurrentLevel = storageCapacityData.currentLvl;
                SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
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
                PersistantData.Instance.sceneData.VehicleCapacityCurrentLevel = vehicleCapacityData.currentLvl;
                SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
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
                PersistantData.Instance.sceneData.PickerSpeedCurrentLevel = pickerSpeedData.currentLvl;
                SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
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
                PersistantData.Instance.sceneData.LoaderSpeedCurrentLevel = loaderSpeedData.currentLvl;
                SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
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
                PersistantData.Instance.sceneData.LoaderBagCurrentLevel = loaderBagData.currentLvl;
                SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
                //Upgrade
                GameManager.Instance.spawner.IncreaseLoaderVolunteerBag(loaderBagIncrement);
                loaderBagUI.UpdateUI(loaderBagData);
            }
        }
    }

    //-----------------load-----------------------
    void LoadStorageCapacity()
    {
        int level = PersistantData.Instance.sceneData.StorageCapacityCurrentLevel; // load from file here
        storageCapacityData.SetCurrLvl(level);
        int amount = storageCapacityIncrement * (storageCapacityData.currentLvl-1);
        _storage.SetMaxGarbageCount(_storage.GetMaxGarbageCount() + amount);
        storageCapacityUI.UpdateUI(storageCapacityData);
    }
    
    void LoadVehicleCapacity()
    {
        int level = PersistantData.Instance.sceneData.VehicleCapacityCurrentLevel; // load from file here
        vehicleCapacityData.SetCurrLvl(level);
        int amount = vehicleCapacityIncrement * (vehicleCapacityData.currentLvl-1);
        _vehicleSystem.SetMaxGarbageCount(_vehicleSystem.GetMaxGarbageCount() + amount);
        vehicleCapacityUI.UpdateUI(vehicleCapacityData);
    }
    
    void LoadPickerSpeed()
    {
        int level = PersistantData.Instance.sceneData.PickerSpeedCurrentLevel; // load from file here
        pickerSpeedData.SetCurrLvl(level);
        int amount = pickerSpeedIncrement * (pickerSpeedData.currentLvl-1);
        GameManager.Instance.spawner.IncreasePickerSpeed(amount);
        pickerSpeedUI.UpdateUI(pickerSpeedData);
    }
    
    void LoadLoaderSpeed()
    {
        int level = PersistantData.Instance.sceneData.LoaderSpeedCurrentLevel; // load from file here
        loaderSpeedData.SetCurrLvl(level);
        int amount = loaderSpeedIncrement * (loaderSpeedData.currentLvl-1);
        GameManager.Instance.spawner.IncreaseLoaderSpeed(amount);
        loaderSpeedUI.UpdateUI(loaderSpeedData);
    }
    
    void LoadLoaderBag()
    {
        int level = PersistantData.Instance.sceneData.LoaderBagCurrentLevel; // load from file here
        loaderBagData.SetCurrLvl(level);
        int amount = loaderBagIncrement * (loaderBagData.currentLvl-1);
        GameManager.Instance.spawner.IncreaseLoaderVolunteerBag(amount);
        loaderBagUI.UpdateUI(loaderBagData);
    }
}
