using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostStar;
public class AttackSystem : MonoBehaviour
{
    public float timeBtwAttack;
    public float startTimeBtwAttack = 0.1f;
    public Transform attackPos;
    public LayerMask enemy;
    public Animator anim;
    public Unit unit;
    public bool isAt = false;
    void Start()
    {
        startTimeBtwAttack = 1.14f;
        unit = GetComponent<Hero>();        
    }
    void Update()
    {
        if(timeBtwAttack < -startTimeBtwAttack)
            timeBtwAttack = -startTimeBtwAttack;
        if (isAt){
            isAt = false;
            if (timeBtwAttack <= 0){
                anim.SetTrigger("attack");
                timeBtwAttack = startTimeBtwAttack;   
            }
        }
        timeBtwAttack -= Time.deltaTime;
    }
    public void DoAttack(){
        isAt = true;
    }
    public virtual void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, unit.AttackRange, enemy);
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (enemies[i].CompareTag("Enemy") && enemies[i].GetComponent<Collider2D>() is CapsuleCollider2D)
            {
                Unit enemyGO = enemies[i].GetComponent<Unit>();
                enemyGO.TakeDamage(unit.Damage, 1, gameObject);
            }
        }
    }
}
