using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathEffect : MonoBehaviour
{

    //public List<GameObject> bones = new List<GameObject>();
    public GameObject bones;
    public GameObject spawnLoc;
    public EffectManager effectManager;
    public SoundManager soundManager;

    bool dead = false;

    void Start()
    {
        effectManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EffectManager>();
        soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            SpawnBones();
    }

    public void SpawnBones()
    {
        if(!dead)
        {
            dead = true;
            soundManager.PlayDeathSound();
            GameObject tempBone = Instantiate(bones, spawnLoc.transform.position, spawnLoc.transform.rotation);
        }
    }

}
