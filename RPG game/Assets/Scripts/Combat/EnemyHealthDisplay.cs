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
            }
            else
            {
                textValue.text = String.Format("{0:0}%", fighter.GetTarget().GetPercentage());
            }
        }
    }
}