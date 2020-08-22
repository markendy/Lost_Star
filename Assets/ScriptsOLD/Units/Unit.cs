using UnityEngine;
using LostStar;
public class Unit : MonoBehaviour, IUnit
{
    public double heals = 1;
    public double Heals
    {
        get => heals;
        set
        {
            if (value <= 0){
                //Deas();
                heals = value;
            }
            else if (value > MaxHeals)
            {
                heals = MaxHeals;
            }
            else { heals = value; }
        }
    }
    public double maxHeals = 1;
    public double MaxHeals
    {
        get => maxHeals;
        set
        {
            if (maxHeals <= 0) maxHeals = 100;
            if(maxHeals == heals){
                maxHeals = value;
                heals = maxHeals;
            }
            else { maxHeals = value; }
        }
    }
    public double regenHeals = 1;
    public double RegenHeals { get => regenHeals; set { if (regenHeals < 0) regenHeals = 0; else { regenHeals = value; } } }
    public double damage = 1;
    public double Damage { get => damage; set { if (damage < 0) damage = 0; else { damage = value; } } }
    public float speed = 1f;
    public float Speed { get => speed; set { if (speed <= 0) speed = 0.1f; else { speed = value; } } }
    public float jumpForce = 1f;
    public float JumpForce { get => jumpForce; set { if (jumpForce <= 0) jumpForce = 0.1f; else { jumpForce = value; } } }
    public float attackRange = 1f;
    public float AttackRange { get => attackRange; set { if (attackRange <= 0) attackRange = 0.1f; else { attackRange = value; } } }
    public double armor;
    public double Armor { get => armor; set { armor = value; } }
    public double physicResist;
    public double PhysicResist { get => physicResist = (0.05 * Armor) / (1 + 0.05 * Armor); set { physicResist = value; } }
    public ExpSystem expSystem;
    public ExpSystem ExpSystem { get => expSystem; set { expSystem = value; } }
    public double givingExp;
    public double GivingExp { get => givingExp; set { givingExp = value; } }
    void FixedUpdate()
    {
        RegenHP();
        PhysicResist = PhysicResist;// UPDATE RESIST
    }
    public virtual void Start(){
        ExpSystem = GetComponent<ExpSystem>();
        Init();
    }
    public virtual void Init()
    {
        MaxHeals = 100;
        Heals = MaxHeals;
        RegenHeals = 5;
        Damage = 40;
        Speed = 1.5f;
        JumpForce = 2.5f;
        AttackRange = 0.15f;
        Armor = 5;
        GivingExp = 50*(ExpSystem.lvl+1);
    }
    public virtual void Deas()
    {
        //Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y+10, 0), transform.rotation);
        Destroy(gameObject);
    }
    public void RegenHP()
    {
        Heals += RegenHeals * Time.deltaTime;
    }
    public void TakeDamage(double dmg, int type, GameObject ktodmg)
    {
        switch (type)
        {
            case 1://PHYSICS
                Heals -= dmg * (1 - PhysicResist);
                if(Heals < 0){
                    ktodmg.GetComponent<Unit>().ExpSystem.Exp += GetComponent<Unit>().GivingExp;
                    Deas();
                }
                break;
            case 2://MAGIC
                break;
            default:
                Heals -= dmg;
                break;
        }
    }
    public void UpgradeStats(){
        MaxHeals += 25 * ExpSystem.lvl;
        Damage *= 1.06;
        Speed *= 1.005f;
        RegenHeals *= 1.07;// * expSystem.lvl;
    }
    //void OnTriggerEnter
}