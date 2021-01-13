﻿using UnityEngine;

public class GameEndEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameEndModal GameEndModal;

    public static GameEndEvents Instance { get { return instance; } }
    private static GameEndEvents instance;

    public delegate void MethodContainerWithGameEndType(GameEndType GameEndType);
    public event MethodContainerWithGameEndType OnGameEnd;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnGameEnd += GameEnd;
    }

    private void GameEnd(GameEndType gameEndType)
    {
        switch (gameEndType)
        {
            case GameEndType.Win:
                GameEndModal.OpenWinModal();
                break;

            case GameEndType.Loose:
                GameEndModal.OpenLooseModal();
                break;
        }
    }

    public static void InvokeOnGameEndEvent(GameEndType gameEndType)
    {
        Instance.OnGameEnd?.Invoke(gameEndType);
    }
}

[System.Serializable]
public enum GameEndType
{
    Win,
    Loose
}