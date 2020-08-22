using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : CBasicBonus
{
    public double VampireProcent;
    public Vampire()
    {
        VampireProcent = 0.15;
    }
    public override void Execude()
    {
        if (unit.GetComponentInParent<AttackSystem>().isAt)
        {
            unit.Heals += unit.Damage * VampireProcent;
            // += GetAdditionalDamagePerLvl();
        }
    }
    public override void Active()
    {

    }
    public override void DisableBonus()
    {

    }
}
