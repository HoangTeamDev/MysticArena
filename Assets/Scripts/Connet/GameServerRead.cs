using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public partial class GameServer
{
    [PunRPC]
    public void OnCardPlayed(string playerName, string cardName)
    {
        Debug.Log(playerName + " đã chơi lá bài: " + cardName);
    }
}
