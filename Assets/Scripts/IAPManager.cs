using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    public static IAPManager Instance;

    const string coins1000 = "com.trashpandas.recyclersunited.coins1000";
    const string coins2500 = "com.trashpandas.recyclersunited.coins2500";
    private void Awake()
    {
        Instance = this;
    }

    public void OnPurchaseComplete(Product product)
    {
        switch(product.definition.id)
        {
            case coins1000:
                CurrencyManager.instance.AddCurrency(1000);
                break;
            case coins2500:
                CurrencyManager.instance.AddCurrency(2500);
                break;
            default:
                break;
        }
    }
    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + " failed because " + reason);
    }
}
