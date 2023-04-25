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
        //CalculatePrice();
        price = initPrice;
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
