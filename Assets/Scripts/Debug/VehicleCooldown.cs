using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VehicleCooldown : MonoBehaviour
{
    public int timeoutSeconds;
    public TMP_Text timeString;
    public GameObject cooldownContainer;
    public VehicleSystem vehicleManager;
    [SerializeField] private int lastSentCount = 0;

    //visual update
    [SerializeField] Image cooldownImage;
    [SerializeField] GameObject cooldownUI_container;

    public bool vehicleReturned = true;

    public void Initialize ()
    {
        cooldownUI_container.SetActive(true);
        //cooldownImage.fillAmount = 0;
        StartCoroutine(nameof(CountdownLoop));
    }

    public void TimerShutdown ()
    {
        vehicleReturned = true;
        cooldownUI_container.SetActive(false);
        GetComponent<Animator>().Play(string.Format("{0}Returns", gameObject.name));
    }

    private IEnumerator CountdownLoop ()
    {
        vehicleReturned = false;
        lastSentCount = vehicleManager.currentGarbageCount;
        vehicleManager.RemoveAllGarbage();
        for (int i=timeoutSeconds; i>=0; i--)
        {
            //timeString.text = string.Format("{0} s", i);
            cooldownImage.fillAmount = (float)(i / timeoutSeconds);
            yield return new WaitForSeconds(1f);
        }
        TimerShutdown();
        vehicleManager.ExchangeGarbageToMoney(lastSentCount);
        yield return null;
    }
}
