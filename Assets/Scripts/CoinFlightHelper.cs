using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlightHelper : MonoBehaviour
{
    [SerializeField] private GameObject refToCoinPileRoot;
    [SerializeField] private Vector3[] initialCoinPos;
    [SerializeField] private Quaternion[] initialCoinRot;
    [SerializeField] private Image refToCoinUI;
    [SerializeField] private RectTransform refToMainUICanvas;
    [SerializeField] private float flightDelay = 0f;

    void Start()
    {
        // init default array values
        initialCoinPos = new Vector3[10];
        initialCoinRot = new Quaternion[10];

        // get all current coin transforms in root object
        for (int i=0; i<refToCoinPileRoot.transform.childCount; i++)
        {
            initialCoinPos[i] = refToCoinPileRoot.transform.GetChild(i).position;
            initialCoinRot[i] = refToCoinPileRoot.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < refToCoinPileRoot.transform.childCount; i++)
        {
            refToCoinPileRoot.transform.GetChild(i).SetPositionAndRotation(initialCoinPos[i], initialCoinRot[i]);
        }
    }

    public void RewardCoins(int coinNum)
    {
        Reset();
        refToCoinPileRoot.SetActive(true);
        Transform originalPosition = refToCoinPileRoot.transform;
        Vector3 targetPosition = gameObject.transform.position;
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPosition);
        refToCoinPileRoot.GetComponent<RectTransform>().position = screenPosition;
        for (int i = 0; i < coinNum; i++)
        {
            // Enlarge coins
            refToCoinPileRoot.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(flightDelay).SetEase(Ease.OutBack);
            // Fly them to dest
            Vector3 screenPos = Camera.main.WorldToScreenPoint(refToCoinUI.rectTransform.position);
            screenPos /= refToMainUICanvas.GetComponent<Canvas>().scaleFactor;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(refToMainUICanvas, worldPos, Camera.main, out Vector2 anchoredPos);
            anchoredPos.y = -anchoredPos.y;
            // TO-DO: figure out why 1.25 fixes the issue
            originalPosition.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(anchoredPos, 1f)
                .SetDelay(flightDelay + 0.5f).SetEase(Ease.InBack);
            // Rotate to origin
            refToCoinPileRoot.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(flightDelay + 0.5f).SetEase(Ease.Flash);
            // Minimize them to 0
            refToCoinPileRoot.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(flightDelay + 1.8f).SetEase(Ease.OutBack);
            // Make each coin scale in a row
            flightDelay += 0.1f;
        }
    }
}
