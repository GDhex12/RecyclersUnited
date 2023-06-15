using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetUpVehicleRow : MonoBehaviour
{
    [SerializeField] private Image vehicleImage;
    [SerializeField] private Button buyButton;
    [SerializeField] private TMPro.TextMeshProUGUI buttonText;
    [SerializeField] private VehicleShop vehicleShop;
    [SerializeField] private VehicleData data;
    [SerializeField] private Vehicle vehicle;
    private bool dataSet = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (dataSet)
		{
			if (vehicle.GetPrice() < CurrencyManager.instance.GetCurrency() && !data.isUnlocked)
			{
                buyButton.enabled = true;
                buyButton.interactable = true;
			}
			else if(vehicle.GetPrice() > CurrencyManager.instance.GetCurrency() && !data.isUnlocked)
			{
                buyButton.enabled = false;
                buyButton.interactable = false;
            }
		}
    }

    public void Setup(Vehicle vehicle, VehicleData data)
	{
        this.data = data;
        this.vehicle = vehicle;
        this.vehicleImage.sprite = vehicle.GetImage();
        if(data.isUnlocked && !data.isSelected)
		{
            buttonText.text = "Select";
            buyButton.onClick.AddListener(() => { vehicleShop.SelectVehilce(vehicle.GetId()); });
            buyButton.enabled = true;
            buyButton.interactable = true;

        }
        else if (!data.isUnlocked)
		{
            buttonText.text = "-"+CurrencyManager.instance.CurrencyStringFormat(vehicle.GetPrice());
            buyButton.onClick.AddListener(() => { vehicleShop.BuyNewVehicle(vehicle.GetId()); });
            buyButton.enabled = true;
            buyButton.interactable = true;
        }
		else
		{
            buyButton.enabled = false;
            buyButton.interactable = false;
            buttonText.text = "Selected";
            
        }
        
        
        
        dataSet = true;
	}

}
