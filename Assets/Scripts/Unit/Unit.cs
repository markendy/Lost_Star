using UnityEngine;
using LostStar;
using System;
public class Unit : MonoBehaviour, IUnit
{
    public Action<Unit, Unit[]> TakeDamageHendler;
    public Action DeadHendler;
    public Action DeltaHPHendler;
    public Action DeltaEXPHendler;


    [SerializeField] private double _heals = 1;
    public double Heals
    {
        get => _heals;
        set
        {
            if(DeltaHPHendler != null)
                DeltaHPHendler();
            if (value <= 0)
            {
                Deas();
                _heals = 0;
            }
            else if (value > MaxHeals)
            {
                _heals = MaxHeals;
            }
            else { _heals = value; }//revorc in %
        }
    }
    [SerializeField] private double _maxHeals = 1;
    public double MaxHeals
    {
        get => _maxHeals;
        set
        {
            if (_maxHeals <= 0) _maxHeals = 100;
            if (_maxHeals == _heals)
            {
                _maxHeals = value;
                _heals = _maxHeals;
            }
            else 
            { 
                double procentTemp = Heals / _maxHeals;
                _maxHeals = value; 
                Heals = procentTemp * _maxHeals;
            }
        }
    }
    [SerializeField] private double _regenHeals = 1;
    public double RegenHeals { get => _regenHeals; set { if (_regenHeals < 0) _regenHeals = 0; else { _regenHeals = value; } } }


    [SerializeField] private double _damage = 1;
    public double Damage { get => _damage; set { if (_damage < 0) _damage = 0; else { _damage = value; } } }


    [SerializeField] private float _speed = 1f;
    public float Speed { get => _speed; set { if (_speed <= 0) _speed = 0.1f; else { _speed = value; } } }
    [SerializeField] private float _jumpForce = 1f;
    public float JumpForce { get => _jumpForce; set { if (_jumpForce <= 0) _jumpForce = 0.1f; else { _jumpForce = value; } } }


    [SerializeField] private float _attackRange = 1f;
    public float AttackRange { get => _attackRange; set { if (_attackRange <= 0) _attackRange = 0.1f; else { _attackRange = value; } } }


    [SerializeField] private double _armor;
    public double Armor { get => _armor; set { _armor = value; } }
    [SerializeField] private double _physicResist;
    public double PhysicResist { get => _physicResist = (0.05 * Armor) / (1 + 0.05 * Armor); set { _physicResist = value; } }


    //[SerializeField] private ExpSystem _expSystem;
    //public ExpSystem ExpSystem { get => _expSystem; set { _expSystem = value; } }
    [SerializeField] private double _givingExp;
    public double GivingExp { get => _givingExp; set { _givingExp = value; } }


    private void FixedUpdate()
    {
        RegenHP();
        PhysicResist = PhysicResist;// UPDATE RESIST
    }
    protected virtual void Start()
    {
        //ExpSystem = GetComponent<ExpSystem>();
        Init();
    }
    protected virtual void Init()
    {
        MaxHeals = 100;
        Heals = MaxHeals;
        RegenHeals = 5;
        Damage = 40;
        Speed = 5.5f;
        JumpForce = 4.5f;
        AttackRange = 0.15f;
        Armor = 5;
        //        GivingExp = 50*(ExpSystem.lvl+1);
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
                /*if (Heals < 0)
                {
                    //ktodmg.GetComponent<Unit>().ExpSystem.Exp += GetComponent<Unit>().GivingExp;
                    Deas();
                }*/
                break;
            case 2://MAGIC
                break;
            default:
                Heals -= dmg;
                break;
        }
    }
    //TEMP
    public void TESTGiveDamage(){
        TakeDamage(50 + MaxHeals * 0.3, 1, gameObject);
    }
    public void UpgradeStats()
    {
        MaxHeals += 25;// * ExpSystem.lvl;
        Damage *= 1.06;
        Speed *= 1.005f;
        RegenHeals *= 1.07;// * _expSystem.lvl;
    }
    //void OnTriggerEnter
}