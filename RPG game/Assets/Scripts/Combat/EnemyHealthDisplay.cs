using RPG.Attributes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Text textValue;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            textValue = GetComponent<Text>();
        }

        private void Update()
        {
            if (fighter.GetTarget() == null)
            {
                textValue.text = "N/A";
                return;
            }
            else
            {
                Health health = fighter.GetTarget();
                textValue.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
            }
        }
    }
}