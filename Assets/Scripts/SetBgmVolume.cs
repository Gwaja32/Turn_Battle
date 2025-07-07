using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBgmVolume : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    GameObject obj;
    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(delegate { bgmValueChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { sfxValueChanged(); });
        obj = GameObject.Find("SoundManager");
    }

    void bgmValueChanged()
    {
        obj.GetComponent<AudioManager>().SetBgmVolume(bgmSlider.value);
    }
    void sfxValueChanged()
    {
        obj.GetComponent<AudioManager>().SetSfxVolume(sfxSlider.value);
    }
}
