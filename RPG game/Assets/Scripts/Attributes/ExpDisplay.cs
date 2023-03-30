using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class ExpDisplay : MonoBehaviour
    {
        Experience experience;
        Text textValue;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            textValue = GetComponent<Text>();
        }

        private void Update()
        {
            textValue.text = String.Format("{0:0}", experience.GetPoints());
        }
    }
}
