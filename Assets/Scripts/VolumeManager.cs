using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Scrollbar volumeSlider;
    [SerializeField] private Scrollbar sfxSlider;
    [SerializeField] private AudioSource musicVolume;
    [SerializeField] private float sfxVolume;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        musicVolume.volume = volumeSlider.value;
        sfxVolume = sfxSlider.value;
    }
}
