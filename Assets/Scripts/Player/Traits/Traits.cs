using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traits : ScriptableObject
{
    public string aName = "New Trait";
    public string aDescription = "Trait Description";
    public Sprite aSprite;
    public AudioClip aSound;
    public float baseCooldown = 1f;

    public abstract void TraitInitialize(GameObject obj);

    public abstract void TriggerTrait();
    
}
