using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSwordBonus : CBonus
{
    public IronSwordBonus()
    {
        BasicBonusList.Add(new AdditionalDamage());
        BasicBonusList.Add(new Vampire());
        foreach (var t in BasicBonusList)
        {
            t.Active();
        }
    }
    void Update()
    {
        if (Active)
            foreach (var t in BasicBonusList)
            {
                t.Execude();
            }
    }
}
