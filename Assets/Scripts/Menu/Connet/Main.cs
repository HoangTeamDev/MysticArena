using Menu.System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Menu.Connet
{
    public class Main : MonoBehaviourPunCallbacks
    {
        public static Main Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            MainLog.Log("Đã kết nối tới ", "Photon Master Server!", ReadColor.Gold);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4; // Tối đa 4 người chơi trong phòng

            PhotonNetwork.CreateRoom("MyRoom", roomOptions); // Tạo phòng tên "MyRoom"
                                                             //PhotonNetwork.JoinRoom("MyRoom"); // Vào phòng có tên "MyRoom"
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Đã vào lobby!");
        }

        public override void OnJoinedRoom()
        {
            MainLog.Log("Đã tham gia phòng!", "Photon Master Server!", ReadColor.Gold);
        }
        public override void OnConnected()
        {

        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
        }

        public override void OnLeftLobby()
        {
            base.OnLeftLobby();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
        }
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
        }
        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
        }
        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
        }
        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        }
    }
}

