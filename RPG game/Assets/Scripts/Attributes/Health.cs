using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] UnityEvent<float> takeDamage; 

        LazyValue<float> healthPoints;
        bool isDead = false;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private void Start()
        {
            healthPoints.ForceInit();            
        }
        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHeath;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHeath;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if (healthPoints.value <= 0)
            {
                Die();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            float exp = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            experience.GainExperience(exp); ;

        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;

            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetFraction()
        {
            return healthPoints.value / GetMaxHealthPoints();
        }

        public float GetPercentage()
        {
            return 100 * GetFraction(); 
        }

        private void RegenerateHeath()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage / 100;
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value <= 0)
            {
                Die();
            }
        }
    }
}
