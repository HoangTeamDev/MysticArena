using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Menu.System;
namespace Menu.Connet
{
    public partial class GameServer
    {
        [PunRPC]
        public void OnCardPlayed(string playerName, string cardName)
        {
            try
            {
                MainLog.LogError($"Người chơi {playerName} vừa chơi lá bài", cardName, ReadColor.Yellow);
            }
            catch (Exception ex)
            {
                MainLog.LogError($"Xuất Hiện lỗi  ", ex.ToString(), ReadColor.Yellow);
            }
        }
    }
}

