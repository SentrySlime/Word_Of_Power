using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLoot : MonoBehaviour
{

    public List<GameObject> lootPool = new List<GameObject>();
    public SoundManager soundManager;

    void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropLoot(Transform enemyPosition)
    {
        int dropLootChance = Random.Range(1, 10);

        if (dropLootChance >= 1)
        {
            soundManager.PlayLootDrop();
            print("loot dropped");
            //int randomLoot = Random.Range(0, 3);
            //Instantiate(lootPool[randomLoot], enemyPosition.position, Quaternion.identity);
        }
    }

}
