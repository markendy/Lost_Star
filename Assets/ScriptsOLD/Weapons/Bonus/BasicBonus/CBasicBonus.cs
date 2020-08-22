using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CBasicBonus
{
    public CBasicBonus(){
        MaxSkillLvl = 10;
        skillLvl = 0;
    }
    public Unit unit = GameObject.Find("Player").GetComponent<Unit>();
    private long skillLvl;
    private long MaxSkillLvl;
    public long SkillLvl { get => skillLvl; set { skillLvl = value; } }
    public abstract void Execude();
    public abstract void Active();
    public abstract void DisableBonus();
}
