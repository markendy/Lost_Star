using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostStar;
public class Enemy_Skeleton : Unit
{
    public override void Start()
    {
        base.Start();
        //base.Init();
        Init();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            GetComponent<AttackSystemE>().OnAttack();
        }
    }
    public new void Init()
    {
        MaxHeals = 600;
        Heals = MaxHeals;
        RegenHeals = 2;
        Damage = 37;
        Speed = 1.1f;
        JumpForce = 1.7f;
    }
    public override void Deas(){
        Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y+10, 0), transform.rotation);
        Destroy(gameObject);
    }
}
