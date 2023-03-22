using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour
{
    public GameObject panel;
    public bool isopen;
    public void openExitMenu()
    {
        isopen = panel.activeSelf;

        if (!isopen)
        {
            isopen = true;
            panel.gameObject.SetActive(true);
        }
        else if (isopen)
        {
            isopen = false;
            panel.gameObject.SetActive(false);
        }
    }
}