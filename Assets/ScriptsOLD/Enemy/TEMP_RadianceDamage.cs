using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_RadianceDamage : MonoBehaviour
{
    public float damage;
    public bool isTriggeAttack;
    // Start is called before the first frame update
    void Start()
    {
        damage = 15f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTriggeAttack)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(gameObject.GetComponentInParent<Enemy_Skeleton>().transform.position, 200, LayerMask.NameToLayer("default"));
            for (int i = 0; i < enemies.Length; ++i)
            {
                if (enemies[i].CompareTag("Player"))
                {
                    if (enemies[i].GetComponent<Unit>().Heals - damage * Time.deltaTime <= 0)
                    {
                        GetComponent<Enemy_Skeleton>().ExpSystem.Exp += enemies[i].GetComponent<Unit>().GivingExp;
                    }
                    enemies[i].GetComponent<Unit>().TakeDamage(damage * Time.deltaTime, 1, gameObject);
                }

            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggeAttack = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggeAttack = false;
        }
    }
}
