 using UnityEngine;
 namespace LostStar{
 public interface IUnit
    {
        double Heals { get; set; }
        double MaxHeals { get; set; }
        double RegenHeals { get; set; }
        double Damage { get; set; }
        float Speed { get; set; }
        float JumpForce { get; set; }
        float AttackRange{ get; set; }
        double Armor {get; set; }
        double PhysicResist {get; set;}
        double GivingExp {get; set;}
        ExpSystem ExpSystem {get; set;}
        void TakeDamage(double dmg, int t, GameObject g);
        void Deas();
        void RegenHP();
        void UpgradeStats();
    }
}