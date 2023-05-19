using System;
using Character;
using GamePlay;
using UI;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    [SerializeField] private UiManager uiManager;

    private void Start()
    {
        UpdateHealth();
        UpdateScore(0);
    }

    public void UpdateHealth()
    {
        uiManager.UpdateHealth(PlayerManager.Instance.health);
    }

    public void UpdateScore(int score)
    {
        uiManager.UpdateScore(score);
    }
}