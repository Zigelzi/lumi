using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GameOver : MonoBehaviour
{
    TMP_Text gameOverTitle;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.ClientOnGameOver += ClientHandleGameOver;

        SetGameOverUI(false);
        gameOverTitle = GetComponentInChildren<TMP_Text>();
    }

    void OnDestroy()
    {
        GameManager.ClientOnGameOver -= ClientHandleGameOver;
    }

    void ClientHandleGameOver(string winnerName)
    {
        SetGameOverUI(true);
        SetGameOverTitlePlayerName(winnerName);

    }

    void SetGameOverUI(bool uiEnabled)
    {
        Canvas gameOverCanvas = GetComponent<Canvas>();
        gameOverCanvas.enabled = uiEnabled;
    }

    void SetGameOverTitlePlayerName(string winnerName)
    {
        string gameOverTitleContent = $"{winnerName} won!";
        gameOverTitle.text = gameOverTitleContent;
    }
}
