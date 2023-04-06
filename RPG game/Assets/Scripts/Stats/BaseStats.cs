using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] GameObject levelUpParticleEffect;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();
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
                LevelUpEffect();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }
        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            
            if (experience == null) return startingLevel;
            
            float currentXP = experience.GetPoints();
            int allLevels = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= allLevels; level++)
            {
                float XPtoLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPtoLevelUp > currentXP)
                {
                    return level;
                }
            }
            return allLevels + 1;
        }
    }
}
