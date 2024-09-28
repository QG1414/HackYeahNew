using SteelLotus.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControll : MonoBehaviour
{
    string musicKey = "MusicVolume";
    string effectsKey = "EffectsVolume";

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider effectsSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey(musicKey))
        {
            ChangeMusicVolume(PlayerPrefs.GetFloat(musicKey));
            musicSlider.value = PlayerPrefs.GetFloat(musicKey);
        }
        else
        {
            ChangeMusicVolume(-20);
        }
        if(PlayerPrefs.HasKey(effectsKey))
        {
            ChangeEffectsVolume(PlayerPrefs.GetFloat(effectsKey));
            effectsSlider.value = PlayerPrefs.GetFloat(effectsKey);
        }
        else
        {
            ChangeEffectsVolume(-20);
        }
    }

    public void ChangeMusicVolume(float value)
    {
        if (value <= -39.8f)
            value = -80;
        SoundManager.Instance.MusicCollection.mixer.SetFloat(SoundManager.Instance.AudioMixerVolumePath, value);
        PlayerPrefs.DeleteKey(musicKey);
        PlayerPrefs.SetFloat(musicKey, value);
        PlayerPrefs.Save();
    }

    public void ChangeEffectsVolume(float value)
    {
        if (value <= -39.8f)
            value = -80;
        SoundManager.Instance.AmbientCollection.mixer.SetFloat(SoundManager.Instance.AudioMixerVolumePath, value);
        PlayerPrefs.DeleteKey(effectsKey);
        PlayerPrefs.SetFloat(effectsKey, value);
        PlayerPrefs.Save();
    }
}
