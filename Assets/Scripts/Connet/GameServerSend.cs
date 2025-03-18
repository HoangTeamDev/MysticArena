using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public partial class GameServer { 
    public void PlayCard(string cardName)
    {
        // Gửi RPC đến tất cả người chơi trong phòng
        photonView.RPC("OnCardPlayed", RpcTarget.All, PhotonNetwork.NickName, cardName);
    }
}
