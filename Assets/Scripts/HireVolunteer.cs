using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HireVolunteer : MonoBehaviour
{
    [SerializeField] long price;
    //[SerializeField] int initPrice = 10;
    [SerializeField] float priceMultiplier = 1.2f;
    [SerializeField] TextMeshProUGUI btnText;

    SpawnObject _spawnObject;

    private void Awake()
    {
        _spawnObject = FindObjectOfType<SpawnObject>();
    }

    private void Start()
    {
        //CalculatePrice();
        price = 10;
    }

    public void BuyVolunteer()
    {
        if (CurrencyManager.instance.IsAffordable(price)) 
        {
            _spawnObject.SpawnVolunteerInSceneIfAfforded(price);
            CalculatePrice();
            CurrencyManager.instance.RemoveCurrency(price);
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
        return $"Hire volunteer\n-{CurrencyManager.instance.CurrencyStringFormat(price)}";
    }
}
