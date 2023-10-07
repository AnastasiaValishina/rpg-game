using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

namespace RPG.Inventories
{
    public class RandomDropper : ItemDropper
    {
        [SerializeField] float scatterDistance = 1f;
        [SerializeField] DropLibrary dropLibrary;
        [SerializeField] int numberOfDrops = 2;

        const int ATTEMPTS = 30;

        public void RandomDrop()
        {
            var baseStats = GetComponent<BaseStats>();
            var drops = dropLibrary.GetRandomDrops(baseStats.GetLevel());
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);
            }            
        }
        protected override Vector3 GetDropLocation()
        {
            for (int i = 0; i < ATTEMPTS; i++)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }
    }
}

