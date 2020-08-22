using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostStar
{
    public class CWeapon : MonoBehaviour
    {
        public int id;
        public CBonus BonusSheet;
        //public List<CSkill> SkillSheet;
        public float AddedAttackRange { get => addedAttackRange; set { if (addedAttackRange <= 0) { addedAttackRange = 0.01f;} else { addedAttackRange = value; } } }
        private float addedAttackRange;
        void Start(){
            Init();
        }
        public virtual void Init()
        {
            addedAttackRange= 0.01f;
        }
    }
}