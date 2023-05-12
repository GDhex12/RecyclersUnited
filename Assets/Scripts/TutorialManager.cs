using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }
    [SerializeField] private GameObject[] graphicalAssets;
    [SerializeField] private string[] expressions = new string[10]
    {"Hello and welcome to Recyclers United!",
    "I am the head of operations here - time to get started!",
    "Your objective is to make our areas squeaky clean.",
    "Let me present my two awesome sidekicks -",
    "The Cleaner, who is committed to collect any small trash they find",
    "The Loader, who will safely transport rubbish between these two places",
    "This bar indicates your level. Level up and unlock power-ups and areas.",
    "This circle fills up as you collect more trash. Storage isn't infinite!",
    "This circle represents vehicle's capacity. Press the car once in a while!",
    "Have fun out there!"};
    [SerializeField] private int layerIdx = -1;
    [SerializeField] private Light[] lightSources;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private TMP_Text tutorialText;
    public bool currentlyRunning = false;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Initialize()
    {
        PersistantData.Instance.playerData.NewGameTutorial = false;
        currentlyRunning = true;
        graphicalAssets[1].SetActive(true);
        MoveLayerForward();
    }

    private void ShowTutorial()
    {
        switch (layerIdx)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                tutorialText.text = expressions[layerIdx];
                break;
            case 4:
                graphicalAssets[0].SetActive(false); // head disappear
                graphicalAssets[2].SetActive(false); // blackout disappear
                cameras[0].Priority = 11; // more priority than main camera
                cameras[0].LookAt = GameObject.Find("VolunteerPicker(Clone)").transform;
                cameras[0].Follow = GameObject.Find("VolunteerPicker(Clone)").transform;
                tutorialText.text = expressions[layerIdx];
                break;
            case 5:
                cameras[1].Priority = 12; // more priority than main camera
                cameras[1].LookAt = GameObject.Find("VolunteerLoader(Clone)").transform;
                cameras[1].Follow = GameObject.Find("VolunteerLoader(Clone)").transform;
                tutorialText.text = expressions[layerIdx];
                break;
            case 6:
                cameras[2].Priority = 15; // back to main camera
                graphicalAssets[3].SetActive(true);
                tutorialText.text = expressions[layerIdx];
                break;
            case 7:
                graphicalAssets[3].SetActive(false);
                graphicalAssets[4].SetActive(true);
                tutorialText.text = expressions[layerIdx];
                break;
            case 8:
                graphicalAssets[4].SetActive(false);
                graphicalAssets[5].SetActive(true);
                tutorialText.text = expressions[layerIdx];
                break;
            case 9:
                graphicalAssets[5].SetActive(false);
                graphicalAssets[0].SetActive(true);
                graphicalAssets[2].SetActive(true);
                tutorialText.text = expressions[layerIdx];
                break;
            case 10:
                // We are done here, destroy them all
                Destroy(graphicalAssets[1]);
                break;
        }
    }

    public void MoveLayerForward ()
    {
        layerIdx++;
        ShowTutorial();
    }
}
