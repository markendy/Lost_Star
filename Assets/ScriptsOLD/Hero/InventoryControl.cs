using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostStar;
public class InventoryControl : MonoBehaviour
{
    public static CWeapon weapon;
    void Start()
    {
        weapon = GetComponentInChildren<IronSword>();
        foreach(var t in weapon.BonusSheet.BasicBonusList)
        {
            t.unit = GetComponent<Unit>();
        }
    }
}
