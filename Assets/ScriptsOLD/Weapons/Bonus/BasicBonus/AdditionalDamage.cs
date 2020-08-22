using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalDamage : CBasicBonus
{   
    public double additionalDamage;
    private double CacheDamage;
    private long CacheUnitLvl;
    public AdditionalDamage(){
        additionalDamage = 0;
        CacheUnitLvl = 0;
        CacheDamage = 0;
    }
    public override void Execude(){
        CacheUnitLvl = unit.ExpSystem.lvl;
    }
    public override void Active(){
        unit.Damage += GetAdditionalDamagePerLvl();
    }
    public override void DisableBonus(){
        double deltalvl = unit.ExpSystem.lvl - CacheUnitLvl;
        if(deltalvl != 0){
            unit.Damage -= GetAdditionalDamagePerLvl() * (deltalvl * 1.06);
        }
        else{
            unit.Damage -= GetAdditionalDamagePerLvl();
        }
    }
    public double GetAdditionalDamagePerLvl()
    {
        CacheDamage = additionalDamage + 10 * SkillLvl;
        return CacheDamage;
    }
    public double GetAdditionalDamageCustom()
    {
        CacheDamage = additionalDamage;
        return additionalDamage;
    }
}
