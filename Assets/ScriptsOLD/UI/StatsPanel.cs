using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    public Unit unit;
    public Text Attack_Text;
    public Text Armor_Text;
    public Text Speed_Text;
    
    void Update()
    {
        ViewArmor();
        ViewAttack();
        ViewSpeed();
    }

    void ViewArmor(){
        Armor_Text.text = ((int)unit.Armor).ToString();
    }
    void ViewAttack(){
        Attack_Text.text = ((int)unit.Damage).ToString();
    }
    void ViewSpeed(){
        Speed_Text.text = ((int)(unit.Speed * 100)).ToString();
    }

}
