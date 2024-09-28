using SteelLotus.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;


    public void PlayOneSound()
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.AlertSource, clip);
    }
}
