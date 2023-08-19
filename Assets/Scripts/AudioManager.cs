using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip btnClick;

    public AudioClip lightTorch;
    [SerializeField] private AudioClip footSteps;
    [SerializeField] private AudioClip preRoll;
    [SerializeField] private AudioClip diceRoll;

    public AudioClip loseHp;
    public AudioClip loseSanity;
    public AudioClip loseCoins;
    public AudioClip gainHp;
    public AudioClip gainSanity;
    public AudioClip gainCoins;
    public AudioClip gainTorch;
    public AudioClip loseTorch;

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

    public void PlayFootStepsSound(bool isLooping)
    {   
        source.clip = footSteps;
        source.loop = isLooping;
        source.Play();
    }

    public void PlayDiceSound()
    {
        source.clip = preRoll;
        source.loop = true;
        source.Play();
        Invoke("PlayDiceRollSound", 1.4f);
    }

    void PlayDiceRollSound()
    {
        source.loop = false;
        source.PlayOneShot(diceRoll, 1.0f);
    }

}
