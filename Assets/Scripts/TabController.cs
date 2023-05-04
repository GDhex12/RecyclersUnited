using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabController : MonoBehaviour
{
    public Button button1; //PWRtabBTN
    public Button button2; //UPGtabBTN
    public Button button3; //VehicleBTN
    public GameObject PowerupTab;
    public GameObject UpgradesTab;
    public GameObject VehiclesTab;
    public Sprite SelectedSprite;
    public Sprite NotSelectedSprite;

    public void ChangeTab()
    {
        GameObject clickedBTN = EventSystem.current.currentSelectedGameObject;
        if (clickedBTN.name == "PWRtabBTN")
        {
            button1.GetComponent<Image>().sprite = SelectedSprite;
            button2.GetComponent<Image>().sprite = NotSelectedSprite;
            button3.GetComponent<Image>().sprite = NotSelectedSprite;

            PowerupTab.SetActive(true);
            UpgradesTab.SetActive(false);
            VehiclesTab.SetActive(false);
        }
        else if (clickedBTN.name == "UPGtabBTN")
        {
            button1.GetComponent<Image>().sprite = NotSelectedSprite;
            button2.GetComponent<Image>().sprite = SelectedSprite;           
            button3.GetComponent<Image>().sprite = NotSelectedSprite;

            PowerupTab.SetActive(false);
            UpgradesTab.SetActive(true);           
            VehiclesTab.SetActive(false);
        }
        else if (clickedBTN.name == "VehicleBTN")
        {
            button1.GetComponent<Image>().sprite = NotSelectedSprite;
            button2.GetComponent<Image>().sprite = NotSelectedSprite;
            button3.GetComponent<Image>().sprite = SelectedSprite;

            PowerupTab.SetActive(false);
            UpgradesTab.SetActive(false);
            VehiclesTab.SetActive(true);
        }
        else return;
        
    } 
}
