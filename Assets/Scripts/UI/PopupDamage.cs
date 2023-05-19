using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PopupDamage:MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void SetText(int damage)
        {
            text.text = damage.ToString();
        }
    }
}