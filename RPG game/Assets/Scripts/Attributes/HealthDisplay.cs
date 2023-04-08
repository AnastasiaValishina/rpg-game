using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text textValue;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            textValue = GetComponent<Text>();
        }

        private void Update()
        {
            textValue.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
