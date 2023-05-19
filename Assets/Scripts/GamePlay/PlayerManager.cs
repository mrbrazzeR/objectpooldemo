using System;
using UnityEngine;

namespace GamePlay
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        public int health;
        public int damage;
        public float speed;

        private void Awake()
        {
            Instance = this;
        }
    }
}
