using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons.Stats;


namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private WeaponStat[] stats = null;
        
        public float GetStat(WeaponStats weaponStat)
        {
            foreach (var stat in stats)
            {
                if (stat.weaponStat == weaponStat)
                {   
                    return stat.value;
                }
            }
            return 0;
        }
        
        [System.Serializable]
        class WeaponStat
        {
            public WeaponStats weaponStat;
            public float value;
        }
    }
}

