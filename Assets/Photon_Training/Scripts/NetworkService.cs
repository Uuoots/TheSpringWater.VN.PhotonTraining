using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Text;

public class NetworkService : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectPhotonNetwork();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConnectPhotonNetwork() 
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnected()
    {
        Debug.Log($"Photon Connected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Photon Connected to Master");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Photon Created Room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Photon Created Room Failed");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Photon Disconnected");
    }

    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log($"Photon Friend List Update");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"Photon Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Photon Joined Room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Photon Joined Room Failed [return code: {returnCode}] [message: {message}]");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"Photon Joined Random Failed [return code: {returnCode}] [message: {message}]");
    }

    public override void OnLeftRoom()
    {
        Debug.Log($"Photon Left Room");
    }

    public override void OnLeftLobby()
    {
        Debug.Log($"Photon Left Lobby");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        StringBuilder stringBuilder = new StringBuilder();

        if (roomList != null && roomList.Count != 0)
        { 
            foreach(RoomInfo roomInfo in roomList) 
            {
                stringBuilder.AppendLine($"[Room Info: <Room Name: {roomInfo.Name}> <Player Count: {roomInfo.PlayerCount}>]");
            }

            Debug.Log($"[Photon Room List Update: {stringBuilder.ToString()}]");
        }
    }
}
