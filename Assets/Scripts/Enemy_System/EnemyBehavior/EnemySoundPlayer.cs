using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundPlayer : MonoBehaviour
{
    [Header("Walk Sounds")]
    public AudioSource walkSource;
    public AudioClip walkClip;

    [Header("Hit sounds")]
    public AudioSource hitSource;
    public AudioClip hitClip;

    
    void Start()
    {
        //hitSource.clip = hitClip;
        walkSource.clip = walkClip;
    }

    
    void Update()
    {
        
    }

    public void PlayWalkSound()
    {
        if (!walkSource.isPlaying)
            walkSource.Play();
    }

    public void StopWalkSound()
    {
        walkSource.Stop();
    }

    public void PlayHitSound()
    {
        if(!hitSource.isPlaying)
            hitSource.Play();

    }

}
