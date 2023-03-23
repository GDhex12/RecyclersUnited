using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleCooldown : MonoBehaviour
{
    [SerializeField]
    public int timeoutSeconds;
    [SerializeField]
    public TMP_Text timeString;
    public GameObject cooldownContainer;
    public VehicleSystem vehicleManager;
    private int lastSentCount = 0;

    private bool cooldownFinished = false; 

    public void Initialize ()
    {
        cooldownContainer.SetActive(true);
        StartCoroutine(nameof(CountdownLoop));
    }

    private void Update()
    {
        if (cooldownFinished)
        {
            TimerShutdown();
        }
    }

    public void TimerShutdown ()
    {
        cooldownContainer.SetActive(false);
        GetComponent<Animator>().Play(string.Format("{0}Returns", gameObject.name));
    }

    private IEnumerator CountdownLoop ()
    {
        lastSentCount = vehicleManager.currentGarbageCount;
        vehicleManager.RemoveAllGarbage();
        for (int i=timeoutSeconds; i>=0; i--)
        {
            timeString.text = string.Format("{0} s", i);
            yield return new WaitForSeconds(1f);
        }
        cooldownFinished = true;
        vehicleManager.ExchangeGarbageToMoney(lastSentCount);
        yield return null;
    }
}
