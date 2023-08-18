using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip btnClick;

    public AudioClip lightTorch;
    [SerializeField] private AudioClip footSteps;
    [SerializeField] private AudioClip diceRoll;

    public AudioClip loseHp;
    public AudioClip loseSanity;
    public AudioClip loseCoins;
    public AudioClip gainHp;
    public AudioClip gainSanity;
    public AudioClip gainCoins;
    public AudioClip gainTorch;

    public AudioClip death;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayBtnSound()
    {
        source.PlayOneShot(btnClick, 1.0f);
    }


    public void PlayClip(AudioClip clip)
    {
        source.PlayOneShot(clip, 1.0f);
    }

    public void PlayDiceSound()
    {
        source.PlayOneShot(diceRoll, 1.0f);
    }

    public void PlayFootStepsSound(bool isLooping)
    {
        source.PlayOneShot(footSteps, 1.0f);
        source.loop = isLooping;
    }


}
