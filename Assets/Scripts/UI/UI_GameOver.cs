using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] GameObject restartButton;
    TMP_Text gameOverTitle;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.ClientOnGameOver += ClientHandleGameOver;

        SetGameOverUI(false);
        gameOverTitle = GetComponentInChildren<TMP_Text>();

        restartButton.SetActive(false);
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

        if (NetworkServer.active)
        {
            restartButton.SetActive(true);
        }
    }

    void SetGameOverTitlePlayerName(string winnerName)
    {
        string gameOverTitleContent = $"{winnerName} won!";
        gameOverTitle.text = gameOverTitleContent;
    }

    public void RestartGame()
    {
        LumiNetworkPlayer player = NetworkClient.connection.identity.GetComponent<LumiNetworkPlayer>();

        player.CmdStartGame();        
    }

    public void LeaveGame()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }

        SceneManager.LoadScene(0);
    }
}
