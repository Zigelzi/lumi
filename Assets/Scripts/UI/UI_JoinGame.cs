using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class UI_JoinGame : MonoBehaviour
{
    [SerializeField] Button connectButton;
    [SerializeField] TMP_InputField ipInput;
    [SerializeField] UI_MainMenu mainMenu;

    string connectButtonName = "Button_Connect";
    string ipInputName = "InputField_IPInput";
    // Start is called before the first frame update
    void OnEnable()
    {
        mainMenu = GetComponentInParent<UI_MainMenu>();

        ipInput = SetInputField();
        SetDefaultIpAddress();

        connectButton = transform.Find(connectButtonName).GetComponent<Button>();

        LumiNetworkManager.ClientOnConnected += HandleClientConnected;
        LumiNetworkManager.ClientOnDisconnected += HandleClientDisconnected;
    }

    void OnDisable()
    {
        LumiNetworkManager.ClientOnConnected -= HandleClientConnected;
        LumiNetworkManager.ClientOnDisconnected -= HandleClientDisconnected;
    }
    TMP_InputField SetInputField()
    {
        string ipInputParentName = "Input_ServerInfo";
        GameObject ipInputGameObject = null;
        TMP_InputField foundInput = null;

        foreach (Transform child in transform)
        {
            if (child.name == ipInputParentName)
            {
                ipInputGameObject = child.gameObject.transform.Find(ipInputName).gameObject;
                foundInput = ipInputGameObject.GetComponent<TMP_InputField>();
            }
        }

        return foundInput;
    }

    void SetDefaultIpAddress()
    {
        string defaultIp = NetworkManager.singleton.networkAddress;
        ipInput.text = defaultIp;
    }

    public void JoinGame()
    {
        string serverIp = ipInput.text;
        NetworkManager.singleton.networkAddress = serverIp;

        NetworkManager.singleton.StartClient();

        connectButton.interactable = false;

    }

    private void HandleClientConnected()
    {
        connectButton.interactable = true;

        gameObject.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        connectButton.interactable = true;
    }
}
