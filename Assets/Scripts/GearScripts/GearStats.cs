﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GearStats : MonoBehaviour //IPointerEnterHandler, IPointerExitHandler
{


    int theNumber = 1;

    //public GameObject gear_Card;
    public GearCardInfo gearCardInfo;

    public Canvas inventoryCanvas;
    //public List<TextMeshProUGUI> statsText = new List<TextMeshProUGUI>();

    public DTInventory.Item thisItem;

    GameObject tempCard;

    public enum Rarity
    {
        White,
        Blue,
        Purple,
        Legendary,
    }

    public StatsList statList;

    public Rarity rarity;

    private void Awake()
    {
        //Put in code here which picks a card depending on the rarity of the Gear
        gearCardInfo = Resources.Load<GearCardInfo>("Gear/Gear_Cards/Gear_Card");


        inventoryCanvas = GameObject.Find("Inventory Canvas").GetComponent<Canvas>();
        thisItem = GetComponent<DTInventory.Item>();

        SetCardIngo();
    }

    void Start()
    {
        
     
    }

    
    public void SetCardIngo()
    {
        gearCardInfo.cardIcon.sprite = thisItem.icon;
        gearCardInfo.cardIcon.SetNativeSize();
        gearCardInfo.cardName.text = thisItem.title;
        statList.PrintStats(gearCardInfo, theNumber);
    }


    public void SetStats()
    {
        //if (lifeAddative < 0)
        //{
        //    //gear_Card.text = life + "%" + "Life";

        //}
    }

}

[System.Serializable]
public class StatsList
{
    [Header("Life")]
    public int increasedlife;
    public float moreLife;
    [Header("EnergyBarrier")]
    public int increasedEB;
    public float moreEB;
    [Header("Energy")]
    public int increasedEnergy;
    public float moreEnergy;
    [Header("Damage")]
    public int increasedDamage;
    public float moreDamage;
    [Header("Defence")]
    public int increasedDefence;
    public float moreDefence;
    [Header("Speed")]
    public int increasedSpeed;
    public float moreSpeed;


    public int critChance;
    public float speed;

    public void PrintStats(GearCardInfo tempGearCard, int number)
    {

        foreach (var property in this.GetType().GetFields())                                     //loops through every single stat
        {
            float temp = System.Convert.ToSingle(property.GetValue(this));                      //Checks if the stat is above Zero

            if (temp > 0)
            {
                if(number == 1)
                {
                    number++;
                    if (property.FieldType == typeof(System.Single))
                    {
                        float multiply = System.Convert.ToSingle(property.GetValue(this)) * 100;
                        tempGearCard.cardStat1.text = property.Name.ToString() + " " +  multiply + " %";        //If it's a float number add the % att the end

                    }
                    else
                        tempGearCard.cardStat1.text = property.Name.ToString() + " " + property.GetValue(this).ToString();               //otherwise just print the name + the value
                    
                }
                else if(number == 2)
                {
                    number++;
                    if (property.FieldType == typeof(System.Single))
                    {
                        float multiply = System.Convert.ToSingle(property.GetValue(this)) * 100;
                        tempGearCard.cardStat2.text = property.Name.ToString() + " " + multiply + " %";        //If it's a float number add the % att the end

                    }
                    else
                        tempGearCard.cardStat2.text = property.Name.ToString() + " " + property.GetValue(this).ToString();               //otherwise just print the name + the value

                }
                else if (number == 3)
                {
                    number++;
                    if (property.FieldType == typeof(System.Single))
                    {
                        float multiply = System.Convert.ToSingle(property.GetValue(this)) * 100;
                        tempGearCard.cardStat3.text = property.Name.ToString() + " " + multiply + " %";        //If it's a float number add the % att the end

                    }
                    else
                        tempGearCard.cardStat3.text = property.Name.ToString() + " " + property.GetValue(this).ToString();               //otherwise just print the name + the value
                }
            }
        }

    }

    public void IncreaseStats(CharacterStats characterStats)
    {
        characterStats.AddLife(increasedlife, moreLife);
        characterStats.AddEnergyBarrier(increasedEB, moreEB);
        characterStats.AddEnergy(increasedEnergy, moreEnergy);
        characterStats.AddPower(increasedDamage, moreDamage);
        characterStats.AddDefence(increasedDefence, moreDefence);
        characterStats.AddSpeed(increasedSpeed, moreSpeed);
    }

    public void DecreaseStats(CharacterStats characterStats)
    {
        characterStats.RemoveMaxLife(increasedlife, moreLife);
        characterStats.RemovePower(increasedDamage, moreDamage);
        characterStats.RemoveMaxEnergy(increasedEnergy, moreEnergy);
        characterStats.RemoveEnergyBarrier(increasedEB, moreEB);
        characterStats.RemoveDefence(increasedDefence, moreDefence);
        characterStats.RemoveSpeed(increasedSpeed, moreSpeed);
    }

    public virtual void LegendaryEffect()
    {

    }
}