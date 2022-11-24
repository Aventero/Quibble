using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateSounds : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = StaticVariables.SoundVolume;
    }

    public void UpdateVolume()
    {
        StaticVariables.SoundVolume = slider.value;
        FindObjectOfType<AudioManager>().UpdateVolume(StaticVariables.SoundVolume);
    }
}
