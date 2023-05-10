using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class VehicleCooldown : MonoBehaviour
{
    public float timeoutSeconds, timeLeft;
    public TMP_Text timeString;
    public GameObject cooldownContainer;
    public VehicleSystem vehicleManager;
    [SerializeField] private int lastSentCount = 0;

    //visual update
    [SerializeField] Image cooldownImage;
    [SerializeField] GameObject cooldownUI_container;
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] Animator garbageFullnessAnimator;

    public bool vehicleReturned = true;

    bool _isTimerRunning;
    [SerializeField] bool isFilling = true;

    string PREF_IS_TIME_RUNNING_NAME;
    string PREF_TIME_LEFT_NAME;
    string PREF_LAST_SENT_COUNT_NAME;

    private void Awake()
    {
        PREF_IS_TIME_RUNNING_NAME = $"{SceneManager.GetActiveScene().name}_isTimeRunning";
        PREF_TIME_LEFT_NAME = $"{SceneManager.GetActiveScene().name}_timeLeft";
        PREF_LAST_SENT_COUNT_NAME = $"{SceneManager.GetActiveScene().name}_lastSentCount";
    }

    private void Start()
    {
        int tempIsTimerRunning = PlayerPrefs.GetInt(PREF_IS_TIME_RUNNING_NAME, 0);

        if (tempIsTimerRunning == 1)
        {
            float timeDiff = TimeManager.instance.TimeDiffBtwExitAndStart();
            timeLeft = PlayerPrefs.GetFloat(PREF_TIME_LEFT_NAME);
            lastSentCount = PlayerPrefs.GetInt(PREF_LAST_SENT_COUNT_NAME);
            if (timeDiff < timeLeft)
            {
                timeLeft -= timeDiff;
                InitializeOnStart();
            }
            else
            {
                vehicleReturned = true;
                _isTimerRunning = false;
                vehicleManager.ExchangeGarbageToMoney(lastSentCount);
            }
        }
        else
        {
            _isTimerRunning = false;
        }
        
    }

    public void Initialize ()
    {
        SendVehicle();

        if (isFilling)
            cooldownImage.fillAmount = 0f;
        else
            cooldownImage.fillAmount = 1f;

        timeLeft = timeoutSeconds;
        _isTimerRunning = true;
        cooldownUI_container.SetActive(true);
        //StartCoroutine(nameof(CountdownLoop));
    }

    void InitializeOnStart()
    {
        GetComponent<Animator>().Play("TruckAway");    
        vehicleReturned = false;
        currency.text = vehicleManager.GetGarbageToMoneyToString(lastSentCount);
        _isTimerRunning= true;
        cooldownUI_container.SetActive(true);
    }

    void SendVehicle()
    {
        SoundManager.PlaySound(SoundManager.Sound.TruckEngine);
        PlayParticleEffect();
        garbageFullnessAnimator.Play("Object_Dissapear");
        vehicleManager.fullWarning.gameObject.SetActive(false);
        vehicleReturned = false;
        lastSentCount = vehicleManager.currentGarbageCount;
        currency.text = vehicleManager.GetGarbageToMoneyToString(lastSentCount);
        vehicleManager.RemoveAllGarbage();

    }
    void PlayParticleEffect()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                gameObject.transform.GetChild(i).GetComponent<PlaySmokeEffect>().PlayParticles();
            }
        }
    }

    public void TimerShutdown ()
    {
        PlayParticleEffect();
        vehicleReturned = true;
        cooldownUI_container.SetActive(false);
        //GetComponent<Animator>().Play(string.Format("{0}Returns", gameObject.name));
        GetComponent<Animator>().Play("TruckReturns");
        garbageFullnessAnimator.Play("Object_Appear");
        _isTimerRunning = false;
    }

    private void Update()
    {
        if (_isTimerRunning)
        {
            timeLeft -= Time.deltaTime;

            if (isFilling)
            {
                cooldownImage.fillAmount = (float)((timeoutSeconds - timeLeft) / timeoutSeconds); //filling
            }
            else
            {
                cooldownImage.fillAmount = (float)(timeLeft / timeoutSeconds); // emptying
            }


            if (timeLeft <= 0)
            {
                TimerShutdown();
                vehicleManager.ExchangeGarbageToMoney(lastSentCount);
            }
        }
    }

    //private IEnumerator CountdownLoop ()
    //{
    //    vehicleReturned = false;
    //    lastSentCount = vehicleManager.currentGarbageCount;
    //    currency.text = vehicleManager.GetGarbageToMoneyToString(lastSentCount);
    //    vehicleManager.RemoveAllGarbage();
    //    for (int i=(int)timeoutSeconds; i>=0; i--)
    //    {
    //        //timeString.text = string.Format("{0} s", i);
    //        //cooldownImage.fillAmount = (float)((timeoutSeconds - i) / timeoutSeconds); //filling 
    //        cooldownImage.fillAmount = (float)(i / timeoutSeconds); //emptying
    //        yield return new WaitForSeconds(1f);
    //    }
    //    TimerShutdown();
    //    vehicleManager.ExchangeGarbageToMoney(lastSentCount);
    //    yield return null;
    //}

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(PREF_IS_TIME_RUNNING_NAME, _isTimerRunning ? 1 : 0);
        if (_isTimerRunning)
        {
            PlayerPrefs.SetFloat(PREF_TIME_LEFT_NAME, timeLeft);
            PlayerPrefs.SetInt(PREF_LAST_SENT_COUNT_NAME, lastSentCount);
        }
    }
}
