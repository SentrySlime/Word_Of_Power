using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Traits/BleedTrait")]
public class BleedTrait : Traits
{
    public float bleedDuration = 1f;

    public float bleedPercentage = 1f;

    public override void TraitInitialize(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerTrait()
    {
        throw new System.NotImplementedException();
    }



}
