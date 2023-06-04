using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] Text damageText;
        public void SetText(float damageAmount)
        {
            damageText.text = String.Format("{0:0}", damageAmount);
        }
    }
}