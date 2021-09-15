using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("ButtonClick")]
    public AudioSource buttonSource;
    public AudioClip buttonClip;

    [Header("Ambience")]
    public AudioSource ambienceSource;
    public AudioClip ambienceClip;

    [Header("LootDrop")]
    public AudioSource lootDropSource;
    public AudioClip lootDropClip;

    [Header("LootPickUp")]
    public AudioSource lootPickUpSource;
    public AudioClip lootPickUpClip;

    [Header("EquipLoot")]
    public AudioSource lootEquipSource;
    public AudioClip lootEquipClip;

    [Header("AttackImpactSound1")]
    public AudioSource impactSoundSource1;
    public AudioClip impactSoundClip1;

    [Header("AttackImpactSound2")]
    public AudioSource impactSoundSource2;
    public AudioClip impactSoundClip2;

    [Header("AttackImpactSound3")]
    public AudioSource impactSoundSource3;
    public AudioClip impactSoundClip3;

    [Header("AttackImpactSound4")]
    public AudioSource impactSoundSource4;
    public AudioClip impactSoundClip4;

    [Header("AttackImpactSound5")]
    public AudioSource impactSoundSource5;
    public AudioClip impactSoundClip5;

    [Header("Enemy Death Sound")]
    public AudioSource deathSource;

    void Start()
    {
        ambienceSource.clip = ambienceClip;
        buttonSource.clip = buttonClip;
        lootDropSource.clip = lootDropClip;
        lootEquipSource.clip = lootEquipClip;
        impactSoundSource1.clip = impactSoundClip1;
        impactSoundSource2.clip = impactSoundClip2;
        impactSoundSource3.clip = impactSoundClip3;
        PlayAmbience();

    }

    void Update()
    {
        
    }

    public void PlayeButtonSound()
    {
        buttonSource.Play();
    }

    public void PlayAmbience()
    {
        ambienceSource.Play();
    }

    public void PlayLootDrop()
    {
        lootDropSource.PlayOneShot(lootDropClip);
    }

    public void PlayLootPickUp()
    {
        lootPickUpSource.PlayOneShot(lootPickUpClip);
    }

    public void PlayEquipLoot()
    {
        print("Equiped Loot");
        lootEquipSource.PlayOneShot(lootEquipClip);
        //lootEquipSource.Play();
    }

    public void PlayDeathSound()
    {
        deathSource.Play();
    }

    public void ImpactSound1()
    {
        impactSoundSource1.PlayOneShot(impactSoundClip1);
    }

    public void ImpactSound2()
    {
        impactSoundSource2.PlayOneShot(impactSoundClip2);
    }

    public void ImpactSound3()
    {
        impactSoundSource3.PlayOneShot(impactSoundClip3);
    }

    public void ImpactSound4()
    {
        impactSoundSource4.PlayOneShot(impactSoundClip4);
    }

    public void ImpactSound5()
    {
        impactSoundSource5.PlayOneShot(impactSoundClip5);
    }
}
