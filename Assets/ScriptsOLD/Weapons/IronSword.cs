using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostStar;
public class IronSword : CWeapon
{
    void Start(){
        Init();
    }
    public new void Init()
    {
        id = 1;
        AddedAttackRange = 0.02f;
        BonusSheet = new IronSwordBonus();
    }
}
