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
        [SerializeField] private GameObject equipPrefab = null;
        [SerializeField] private WeaponStat[] stats = null;


        private WeaponSocket socket;

        public void Spawn(Transform rightHand)
        {   
            if (equipPrefab != null)
            {
                Transform handTransform = null;
                equipPrefab.GetComponent<BoxCollider2D>().enabled = true;
                equipPrefab.GetComponent<WeaponController>().isDropped = false;
                if (!rightHand.GetComponent<WeaponSocket>().hasWeapon)
                {
                    handTransform = rightHand;
                }
                GameObject go = Instantiate(equipPrefab, handTransform);
                socket = handTransform.GetComponent<WeaponSocket>();
                socket.SetupWeapon(go);
            }
        }
        
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

