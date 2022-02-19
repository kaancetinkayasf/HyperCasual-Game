using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    AudioSource collectSound;

    [SerializeField]
    AudioSource ouchSound;

    [SerializeField]
    AudioSource gameOverSound;

    [SerializeField]
    AudioSource succesSound;


    private void Awake()
    {
        playerController.OnCollectDiamond += PlayCollectSound;
        playerController.OnTookDamage += PlayOuchSound;
        playerController.OnLevelEnded += PlaySuccesSound;
        uiManager.OnPlayerDied += PlayDieSound;
        
        
    }

    private void PlaySuccesSound()
    {
        succesSound.Play();
    }

    private void PlayDieSound()
    {
        gameOverSound.PlayDelayed(0.2f);
    }

    private void PlayOuchSound(GameObject obj)
    {
        ouchSound.Play();
    }

    private void PlayCollectSound(GameObject obj)
    {
        collectSound.Play();
    }

   
}
