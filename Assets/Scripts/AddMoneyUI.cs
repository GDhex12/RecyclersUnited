using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class AddMoneyUI : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        IAPManager.Instance.OnPurchaseComplete(product);
    }
    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        IAPManager.Instance.OnPurchaseFailure(product, reason);
    }
}
