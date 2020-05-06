using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text text;
    public GameObject logPanel;
    public InputField nicknameForm, roomForm;
    public string roomName;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        if(!PlayerPrefs.HasKey("Nickname"))
        {
            PlayerPrefs.SetString("Nickname", "Player " + Random.Range(1, 9999));
        }
        
        PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
        nicknameForm.placeholder.GetComponent<Text>().text = PhotonNetwork.NickName;
        Log("Player's nickname is set to " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HideLog();
        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("Game");
    }
    public void CreateOrJoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
    }

    public void SetNickname()
    {
        PlayerPrefs.SetString("Nickname", nicknameForm.text);
        UpdateNickname();
    }

    void UpdateNickname()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
        Log("Player's nickname is set to " + PhotonNetwork.NickName);
    }

    
    public void UpdateRoomName()
    {
        roomName = roomForm.text;
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to master server!");
    }
    // Update is called once per frame
    void Log(string message)
    {
        print(message);
        text.text += "\n";
        text.text += message; 
    }

    public void ShowLog()
    {
        logPanel.SetActive(true);
    }
    public void HideLog()
    {
        logPanel.SetActive(false);
    }
}
