using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour
{
    public GameObject panel;
    public bool isopen;
    public void openExitMenu()
    {
        if (isopen == false)
        {
            isopen = true;
            panel.gameObject.SetActive(true);
        }
        else if (isopen == true)
        {
            isopen = false;
            panel.gameObject.SetActive(false);
        }
    }
}
