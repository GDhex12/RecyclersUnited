using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    Rect safeArea;
    Vector2 min;
    Vector2 max;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        min = safeArea.position;
        max= min + safeArea.size;

        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;

        rectTransform.anchorMin= min;
        rectTransform.anchorMax= max;
    }

}
