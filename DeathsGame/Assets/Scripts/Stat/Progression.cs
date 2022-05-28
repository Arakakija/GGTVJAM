using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stats
{
    [CreateAssetMenu(fileName = "Progression",menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClasses,Dictionary<Stat,float[]>> lookUpTable = null;
        public float GetStat(Stat stat, CharacterClasses characterClass, int level)
        {
            BuildLookUp();
            if (!lookUpTable[characterClass].ContainsKey(stat))
            {
                return 0;
            }

            float[] levels = lookUpTable[characterClass][stat];

            if (levels.Length == 0)
            {
                return 0;
            }

            if (levels.Length < level)
            {
                return levels[levels.Length - 1];
            }
            return levels[level - 1];
        }

        void BuildLookUp()
        {
            if (lookUpTable != null) return;
            lookUpTable = new Dictionary<CharacterClasses, Dictionary<Stat, float[]>>();
            foreach (var character in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();
                foreach (var progressionStat in character._stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }
                lookUpTable[character.characterClass] = statLookupTable;
            }
        }

        public int GetLevels(Stat stat, CharacterClasses characterClass)
        {
            BuildLookUp();
            float[] levels = lookUpTable[characterClass][stat];
            return levels.Length;
        }
        
        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] public CharacterClasses characterClass;
            [SerializeField] public ProgressionStat[] _stats;
        }
        
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}

