using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel()
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
