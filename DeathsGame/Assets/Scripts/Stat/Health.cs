using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;
using UnityEngine.Serialization;
using Saving;

namespace Stats
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private float healthPoints = 100f;

        [SerializeField] private float regenerantionPercentage = 70f;
        //[SerializeField] private GameObject instigator;

        private bool isDead = false;
        
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Vitality);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints <= 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(!experience) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExpReward));
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            
            //Animations and Others
            Destroy(this.gameObject);
        }

        public float GetHealth()
        {
            return healthPoints;
        }

        public void ModifyHealth(float heal)
        {
            if (isDead) return;
            float maxHealth  = GetComponent<BaseStats>().GetStat(Stat.Vitality);
            healthPoints = Mathf.Max(healthPoints + heal, 0);
            if (healthPoints > maxHealth)
            {
                healthPoints = maxHealth;
            }
        }

        public void RegenerateHealth()
        {
            float regenHP =  healthPoints = GetMaxHealth() * (regenerantionPercentage / 100f);
            healthPoints = Mathf.Max(healthPoints, regenHP);
        }

        public void FullHealth()
        {
            healthPoints = GetMaxHealth();
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Vitality);
        }
        
        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
                
        }
    }
    
}
