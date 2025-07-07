using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSoundSlider : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    GameObject obj;
    private void Start()
    {
        obj = GameObject.Find("SoundManager");
        bgmSlider.value = obj.GetComponent<AudioManager>().bgmVolume;
        sfxSlider.value = obj.GetComponent<AudioManager>().sfxVolume;
    }
}
