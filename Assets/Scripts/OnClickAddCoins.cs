using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickAddCoins : MonoBehaviour
{
    [Header("Add Coins")]
    [SerializeField] private int coinsFrom = 10;
    [SerializeField] private int coinsTo = 100;
    public void OnClick()
    {
        if (gameObject.activeSelf)
        {
            CurrencyManager.instance.AddCurrency(Random.Range(coinsFrom, coinsTo));
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }
}
