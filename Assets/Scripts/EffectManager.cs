using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    public GameObject levelUpEffect;
    public GameObject gearDropEffect;
    public GameObject deathEffect;

    GameObject playerPos;

    public GameObject specialEffectPos;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(LevelUP());
        //LevelUpEffect();
    }

    
    void Update()
    {
        
    }

    public void DeathEffect(Vector3 position)
    {
        Instantiate(deathEffect, position, Quaternion.identity);
    }

    public void GearDropEffect()
    {
        GameObject tempEffect = Instantiate(gearDropEffect, playerPos.transform);
    }

    public void LevelUpEffect()
    {

        StartCoroutine(LevelUP());
        
    }

    IEnumerator LevelUP()
    {
        GameObject tempEffect = Instantiate(levelUpEffect, specialEffectPos.transform.position, specialEffectPos.transform.rotation, playerPos.transform);
        yield return new WaitForSeconds(2);
        Destroy(tempEffect);
        //GameObject tempEffect = Instantiate(levelUpEffect, playerPos.transform);
    }

}
