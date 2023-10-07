using GameDevTV.Inventories;
using RPG.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = "RPG/Inventory/Equipable Item")]
    public class StatsEquipableItem : EquipableItem, IModifierProvider
    {
        [SerializeField] Modifier[] additiveModifiers;
        [SerializeField] Modifier[] percentageModifiers;

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (Modifier modifier in additiveModifiers)
            {
                if (modifier.stat == stat)
                    yield return modifier.value;
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (Modifier modifier in percentageModifiers)
            {
                if (modifier.stat == stat)
                    yield return modifier.value;
            }
        }

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float value;
        }
    }
}

