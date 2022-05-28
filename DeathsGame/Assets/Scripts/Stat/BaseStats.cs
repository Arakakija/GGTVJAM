using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)] [SerializeField] private int startingLevel = 1;
        [SerializeField] CharacterClasses characterClass;
        [SerializeField] private Progression progression = null;
        public event Action onLevelUp;
        private int currentLevel = 0;
        private void Start()
        {
            currentLevel = GetLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                onLevelUp();
            }
        }

        public float GetStat(Stat stat)
        {
            if (currentLevel < 1) currentLevel = CalculateLevel();
            return progression.GetStat(stat, characterClass, GetLevel())  + GetAdditiveModifier(stat);
        }



        public int GetLevel()
        {
            return currentLevel;
        }
        
        private float GetAdditiveModifier(Stat stat)
        {
            
            return 0;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXp = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExpToLevel,characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExpToLevel, characterClass, level);
                if (xpToLevelUp > currentXp)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }
        
    }
}

