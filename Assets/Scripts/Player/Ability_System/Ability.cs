using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string aName = "New ability";
    public string aDescription = "Ability description";
    public Sprite aSprite;
    public AudioClip aSound;
    public float aBaseCooldown = 1f;
    public float aEnergyCost = 0;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();

}
