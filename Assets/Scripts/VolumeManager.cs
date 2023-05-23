using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Scrollbar volumeSlider;
    [SerializeField] private Scrollbar sfxSlider;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float musicVolume=0.2f;
    [SerializeField] private float sfxVolume=0.2f;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            CreatePrefs();
            SetSliderValues();
        }
        else
        {
            musicVolume = PlayerPrefs.GetFloat("Music");
            sfxVolume = PlayerPrefs.GetFloat("Sound");
            SetSliderValues();
            SetVolume();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(volumeSlider.value != musicVolume || sfxVolume != sfxSlider.value)
        {
            musicVolume =volumeSlider.value;
            sfxVolume=sfxSlider.value;
            SetVolume();
            UpdatePrefs();
        }
        
    }

    private void UpdateSFXVolumeValue()
    {
        PlayerPrefs.SetFloat("Music", 0.1234f);
        PlayerPrefs.SetFloat("Sound", 0.1234f);
    }

    private void CreatePrefs()
    {
        PlayerPrefs.SetFloat("Music", musicVolume);
        PlayerPrefs.SetFloat("Sound", sfxVolume);
    }

    private void UpdatePrefs()
    {
        PlayerPrefs.SetFloat("Music", musicVolume);
        PlayerPrefs.SetFloat("Sound", sfxVolume);
    }
    private void SetSliderValues()
    {
        volumeSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    private void SetVolume()
    {
        musicSource.volume = volumeSlider.value;
        sfxVolume = sfxSlider.value;
    }
}
