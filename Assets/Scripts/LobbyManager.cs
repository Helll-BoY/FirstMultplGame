using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text logtext;
    void Start()
    {
        Screen.SetResolution(640, 480, false);
        PhotonNetwork.NickName = "Player" + Random.RandomRange(0, 100);
        Log(PhotonNetwork.NickName +  " " +  "has been connected");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 10 }); ;
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to Dungeon-Master");
    }
    public void Log(string Message)
    {
        Debug.Log(Message);
        logtext.text += "\n";
        logtext.text += Message;
    }

}
