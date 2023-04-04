using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabController : MonoBehaviour
{
    public Button button1; //PWRtabBTN
    public Button button2; //UPGtabBTN
    public GameObject PowerupTab;
    public GameObject UpgradesTab;
    public Sprite SelectedSprite;
    public Sprite NotSelectedSprite;

    public void ChangeTab()
    {
        string clickedBTN = EventSystem.current.currentSelectedGameObject.name;
        if (clickedBTN == "PWRtabBTN")
        {
            button1.GetComponent<Image>().sprite = SelectedSprite;
            button2.GetComponent<Image>().sprite = NotSelectedSprite;

            PowerupTab.SetActive(true);
            UpgradesTab.SetActive(false);
        }
        else if (clickedBTN == "UPGtabBTN")
        {
            button2.GetComponent<Image>().sprite = SelectedSprite;
            button1.GetComponent<Image>().sprite = NotSelectedSprite;

            UpgradesTab.SetActive(true);
            PowerupTab.SetActive(false);
        }
        else return;
        
    } 
}
