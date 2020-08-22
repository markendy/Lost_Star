using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpSystem : MonoBehaviour
{
    private long preExpLvl = 0;
    private int MaxLvl = 100;
    private long nextLvl = 100;
    private long NextLvl { get => nextLvl; set { nextLvl = value; } }
    public int lvl = 0;
    private double exp = 0;
    public Unit unit;
    public Image bar;
    public Text text;
    public Text LevelTxt;
    public double Exp
    {
        get => exp;
        set
        {
            if (exp < 0)
                exp = 0;
            if (lvl == MaxLvl)
            {
                exp = NextLvl;
            }
            else
            {
                exp = value;
                if (exp >= NextLvl)
                {
                    ++lvl;
                    preExpLvl = NextLvl;
                    Exp = Exp - NextLvl;
                    NextLvl += 25 * lvl;
                    unit.UpgradeStats();
                }
            }
        }
    }
    public ExpSystem()
    {
        preExpLvl = 0;
        nextLvl = 100;
        lvl = 0;
        exp = 0;
    }
    void Update()
    {
        if (unit == null)
            return;
        UpdateLvl();
        if (bar != null)
            bar.fillAmount = (float)(exp / NextLvl);
        if (text != null)
            text.text = $"{(int)exp} / {(int)NextLvl}";
        if (LevelTxt != null)
            LevelTxt.text = $"{lvl}";
    }
    void UpdateLvl()
    {
        Exp = Exp;
    }
    public void Give25Exp()
    {
        Exp = NextLvl;
    }
}
