using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateSounds : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        SaveManager.Load();
        slider.value = SaveManager.SoundVolume;
    }

    public void UpdateVolume()
    {
        SaveManager.SoundVolume = slider.value;
        SaveManager.Save();
        FindObjectOfType<AudioManager>().UpdateVolume(SaveManager.SoundVolume);
    }
}
