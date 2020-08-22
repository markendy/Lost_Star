using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostStar;
public class AttackSystemE : AttackSystem
{
    public bool needAt;
    void Start()
    {
        unit = GetComponent<Enemy_Skeleton>();
        //weapon = InventoryControl.GetWeaponId(InventoryControl.id_weapon);
    }
    void checkedAttack()
    {
        if (timeBtwAttack < -startTimeBtwAttack)
            timeBtwAttack = -startTimeBtwAttack;
        if (timeBtwAttack <= 0)
        {
            anim.SetTrigger("attack");
            timeBtwAttack = startTimeBtwAttack;
            OnAttack();
        }
        timeBtwAttack -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            checkedAttack();
        }
    }
    public override void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, unit.AttackRange, enemy);
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (enemies[i].CompareTag("Player"))
            {
                enemies[i].GetComponent<Unit>().TakeDamage(unit.Damage, 1, gameObject);
            }
        }
    }
}
