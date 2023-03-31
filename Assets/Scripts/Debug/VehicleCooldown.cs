using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleCooldown : MonoBehaviour
{
    public int timeoutSeconds;
    public TMP_Text timeString;
    public GameObject cooldownContainer;
    public VehicleSystem vehicleManager;
    [SerializeField] private int lastSentCount = 0;

    public void Initialize ()
    {
        cooldownContainer.SetActive(true);
        StartCoroutine(nameof(CountdownLoop));
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
        TimerShutdown();
        vehicleManager.ExchangeGarbageToMoney(lastSentCount);
        yield return null;
    }
}
