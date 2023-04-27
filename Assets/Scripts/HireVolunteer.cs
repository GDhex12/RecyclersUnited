using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HireVolunteer : MonoBehaviour
{
    [SerializeField] long price;
    [SerializeField] int initPrice = 10;
    [SerializeField] float priceMultiplier = 1.2f;
    [SerializeField] TextMeshProUGUI btnText;
    //[SerializeField] string unitName = "volunteer";
    [SerializeField] VolunteerType volunteerType;
    
    [Serializable]
    enum VolunteerType
    {
        volunteer, loader
    };

    SpawnObject _spawnObject;

    private void Awake()
    {
        _spawnObject = FindObjectOfType<SpawnObject>();
    }

    private void Start()
    {
        CalculatePriceOnStart();
        
        UpdateUI();
    }

    public void BuyVolunteer()
    {
        if (CurrencyManager.instance.IsAffordable(price)) 
        {
            SpawnUnit();
            CurrencyManager.instance.RemoveCurrency(price);
            CalculatePrice();
        }
    }

    void CalculatePrice()
    {
        //int count = PersistantData.Instance.playerData.VolunteerCount;
        price = (long)(price * priceMultiplier );
        UpdateUI();
    }
    
    void CalculatePriceOnStart()
    {

        switch (volunteerType)
        {
            case VolunteerType.volunteer:
                price = (long)(initPrice * MathF.Pow(priceMultiplier, PersistantData.Instance.playerData.VolunteerPickerCount - 1));
                break;
            case VolunteerType.loader:
                price = (long)(initPrice * MathF.Pow(priceMultiplier, PersistantData.Instance.playerData.VolunteerLoaderCount - 1));
                break;
        }
    }

    public void UpdateUI()
    {
        btnText.text = StringFormat();
    }

    public string StringFormat()
    {
        return $"Hire {volunteerType}\n-{CurrencyManager.instance.CurrencyStringFormat(price)}";
    }

    void SpawnUnit()
    {
        switch (volunteerType)
        {
            case VolunteerType.volunteer:
                _spawnObject.SpawnVolunteerPickerInSceneIfAfforded(price);
                break;
            case VolunteerType.loader:
                _spawnObject.SpawnLoaderInSceneIfAfforded(price);
                break;
        }
    }
    
    
}
