using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    [SerializeField] protected Image _bar;
    [SerializeField] protected Text _valueInPanel;
    [SerializeField] protected Unit _unit;
    private void Start()
    {
        _bar.fillAmount = 1f;     
    }
}
