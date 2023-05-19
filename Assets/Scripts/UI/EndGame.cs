using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class EndGame:MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(ReloadScene);
        }

        private void ReloadScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}