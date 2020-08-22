using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LostStar;
using System;
public class HealPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healsBarI;
    public Text text;
    public Text textRG;
    public Unit unit;
    void Start()
    {
        healsBarI.fillAmount = 1f;     
    }
    void FixedUpdate(){
        UpdateHP();
    }
    void UpdateHP(){
        text.text = $"{(int)unit.Heals}  / {(int)unit.MaxHeals}";
        if(textRG != null){
            textRG.text = $"+{Math.Round(unit.RegenHeals, 2)}";
        }
        healsBarI.fillAmount = (float)(unit.Heals / unit.MaxHeals);
    }   
}
