using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
       PickUp,
       TruckEngine,
       CoinCollect
    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.SoundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }

    public static void PlaySound(Sound sound, float vol, float pitch)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = vol;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(GetAudioClip(sound));
        Object.Destroy(soundGameObject, 5f);
    }

    

   

}