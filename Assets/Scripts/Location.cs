using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    enum UnlockedSaveIndex { _0,_1,_2,_3,_4,_5,_6,_7,_8,_9}

    [Header("Location info")]
    [SerializeField] int buildIndex;
    [SerializeField] bool isUnlocked = false;
    [SerializeField] bool isBought = false;
    [SerializeField] long price = 100;
    [SerializeField] string sceneName;
    [SerializeField] int lvlToUnlock = 0;
    [SerializeField] UnlockedSaveIndex sceneSaveIndex = UnlockedSaveIndex._0;

    [Header("Visuals")]
    [SerializeField] Image imageRef;


    //for later iteration
    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite unlockedSprite;
    [SerializeField] Sprite boughtSprite;

    bool _popupActive;
    Animator _animator;
    ExperienceStats _experienceStats;

    private void Awake()
    {
        _experienceStats = FindObjectOfType<ExperienceStats>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _popupActive = false;
        isBought = PersistantData.Instance.playerData.isAreaUnlocked[(int)sceneSaveIndex];
        
        UpdateUI();
    }

    private void Update()
    {
        if (!isUnlocked)
        {
            CheckUnlockLocation();
        }
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
        SaveIsBought();
    }

    public bool GetIsBought()
    {
        return isBought;
    }

    void UpdateUI()
    {
        if (!isUnlocked && !isBought)
        {
            imageRef.sprite = lockedSprite;
        }
        else if (isUnlocked && !isBought)
        {
            imageRef.sprite = unlockedSprite;
        }
        else if (isUnlocked && isBought)
        {
            imageRef.sprite = boughtSprite;
        }
        else
        {
            //Debug.LogWarning("Location is bought but not unlocked!");
        }
    }

    public void LoadLocation()
    {
        if (IsLoadable())
        {
            LocationLoader.Instance.LoadScene_Transition(sceneName);
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

    void CheckUnlockLocation()
    {
        if (_experienceStats != null)
        {
            if (_experienceStats.level >= lvlToUnlock)
            {
                UnlockLocation();
            }
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
                SetPopupActive(false);
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
            //UnlockLocation();
        }
        else if (isUnlocked && !isBought)
        {
            //BuyLocation();

            TogglePopup();
        }
        else if (isUnlocked && isBought)
        {
            LoadLocation();
        }
        
    }

    void TogglePopup()
    {
        _popupActive = !_popupActive;
        SetPopupActive(_popupActive);
        //_popup.SetActive(_popupActive);
    }

    void SetPopupActive(bool active)
    {
        //_popup.SetActive(active);
        _animator.SetBool("popup", active);
    }

    void SaveIsBought()
    {
        PersistantData.Instance.playerData.isAreaUnlocked[(int)sceneSaveIndex] = isBought;
        SaveSystem.SavePlayerData(PersistantData.Instance.playerData);
    }
}
