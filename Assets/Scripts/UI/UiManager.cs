using GamePlay;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text health;

        [SerializeField] private TMP_Text score;
        private int _currentScore;


        
        public void UpdateHealth(int currentHealth)
        {
            health.text = currentHealth.ToString();
        }

        public void UpdateScore(int addScore)
        {
            _currentScore += addScore;
            score.text = _currentScore.ToString();
        }
    }
}
