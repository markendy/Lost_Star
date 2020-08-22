using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CBonus
{
        public bool Active;
        public List <CBasicBonus> BasicBonusList;
        public CBonus(){
                BasicBonusList = new List<CBasicBonus>();
                Active = false;
        }
}