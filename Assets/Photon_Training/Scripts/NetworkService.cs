using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class NetworkService : MonoBehaviourPunCallbacks
{
    [SerializeField] string nickName;
    [SerializeField] string roomName;
    [SerializeField] GameObject _playerArmature;
    [SerializeField] TMP_InputField _ipfRoomName;
    [SerializeField] TMP_InputField _ipfRegion;

    [SerializeField] Button _btnJoinRoom;
    [SerializeField] Button _btnLeaveRoom;
    [SerializeField] Button _btnReconnect;

    public string nextRoom = null;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _btnJoinRoom?.onClick.AddListener(OnButtonJoinRoomClicked);
        _btnLeaveRoom?.onClick.AddListener(OnButtonLeaveRoomClicked);
        _btnReconnect?.onClick.AddListener(OnButtonReconnectClicked);
    }

    private void OnButtonReconnectClicked()
    {
        if (!PhotonNetwork.IsConnected)
        {
            AppSettings appSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
            appSettings.FixedRegion = "asia";
            PhotonNetwork.ConnectUsingSettings(appSettings);
        }
    }

    private void OnButtonLeaveRoomClicked()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Disconnect();
        }
    }

    private void OnButtonJoinRoomClicked()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ConnectPhotonNetwork();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            
        }

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
        PhotonNetwork.NickName = $"{nickName}";
        Debug.Log($"Photon Joined Lobby");

        PhotonNetwork.JoinOrCreateRoom(_ipfRoomName.text, new RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Photon Joined Room {PhotonNetwork.CurrentRoom.Name} {PhotonNetwork.CurrentRoom.PlayerCount}");
        PhotonNetwork.Instantiate(_playerArmature.name, Random.insideUnitSphere, Quaternion.identity);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Photon Joined Room Failed [return code: {returnCode}] [message: {message}]");
        PhotonNetwork.JoinOrCreateRoom(_ipfRoomName.text, new RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"new player {newPlayer.NickName}");  
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"new player {otherPlayer.NickName}");
    }
}
