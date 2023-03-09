using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    [Header("Location info")]
    [SerializeField] int buildIndex;
    [SerializeField] bool isUnlocked = false;
    [SerializeField] bool isBought = false;
    [SerializeField] long price = 100;

    [Header("Visuals")]
    [SerializeField] Image imageRef;
    [SerializeField] Color lockedColor;
    [SerializeField] Color unlockedColor;
    [SerializeField] Color boughtColor;

    //for later iteration
    //[SerializeField] Sprite lockedSprite;
    //[SerializeField] Sprite unlockedSprite;
    //[SerializeField] Sprite boughtSprite;

    private void Start()
    {
        UpdateUI();
    }

    public int GetBuildIndex()
    {
        return buildIndex;
    }

    public void SetIsUnlocked(bool isUnlocked)
    {
        this.isUnlocked = isUnlocked;
    }

    public bool GetIsUnlocked()
    {
        return isUnlocked;
    }

    public void SetIsBought(bool isBought)
    {
        this.isBought = isBought;
    }
    public bool GetIsBought()
    {
        return isBought;
    }

    void UpdateUI()
    {
        if (!isUnlocked && !isBought)
        {
            //imageRef.sprite = lockedSprite;
            imageRef.color = lockedColor;
        }
        else if (isUnlocked && !isBought)
        {
            //imageRef.sprite = unlockedSprite;
            imageRef.color = unlockedColor;
        }
        else if (isUnlocked && isBought)
        {
            //imageRef.sprite = boughtSprite;
            imageRef.color = boughtColor;
        }
        else
        {
            Debug.LogWarning("Location is bought but not unlocked!");
        }
    }

    public void LoadLocation()
    {
        if (IsLoadable())
        {
            //SceneManager.LoadScene(buildIndex);
            Debug.Log($"Loading to scene {buildIndex}");
        }
        else
        {
            Debug.LogWarning("Location is not bought or unlocked");
        }
    }

    public bool IsLoadable()
    {
        return isUnlocked && isBought;
    }

    public void UnlockLocation()
    {
        if (!isUnlocked)
        {
            SetIsUnlocked(true);
            UpdateUI();
        }
        else
        {
            Debug.Log("Already unlocked");
        }
    }

    public void BuyLocation()
    {
        if (isUnlocked && !isBought)
        {
            if (CurrencyManager.instance.IsAffordable(price))
            {
                CurrencyManager.instance.RemoveCurrency(price);
                SetIsBought(true);
                UpdateUI();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else
        {
            Debug.Log("Not unlocked or already bought");
        }
    }

    public void OnLocationPress()
    {
        if (!isUnlocked && !isBought)
        {
            UnlockLocation();
        }
        else if (isUnlocked && !isBought)
        {
            BuyLocation();
        }
        else if (isUnlocked && isBought)
        {
            LoadLocation();
        }
        
    }
}