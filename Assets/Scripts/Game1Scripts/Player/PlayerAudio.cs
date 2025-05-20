using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;

    //音
    [Header("効果音")]
    public AudioClip pickMoney;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //音を鳴らす関数
    public void OnSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
