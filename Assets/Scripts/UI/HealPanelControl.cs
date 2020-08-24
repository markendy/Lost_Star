using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LostStar;
using System;
public class HealPanelControl : PanelControl
{
    [SerializeField] private Text _regenHPText;
    
    private void FixedUpdate(){
        UpdateHP();
    }
    private void UpdateHP(){
        _valueInPanel.text = $"{(int)_unit.Heals} / {(int)_unit.MaxHeals}";
        if(_regenHPText != null){
            _regenHPText.text = $"+{Math.Round(_unit.RegenHeals, 2)}";
        }
        _bar.fillAmount = (float)(_unit.Heals / _unit.MaxHeals);
    }   
}
