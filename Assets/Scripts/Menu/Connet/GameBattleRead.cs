using Menu.System;
using RoomAll;
using System;
using UI.SystemUI;
using UI.UIWindow;
using UnityEngine;

public class GameBattleRead : MonoBehaviour
{
    private GameData gameData => GameData.Instance;
    public void Handle(Message message)
    {
        MainLog.Log("Nhận mess", message.Command.ToString(), ReadColor.Green);
        switch (message.Command)
        {
            case 14:
                {
                    HandleCardBattle(message);
                }
                break;
            case 15:
                break;
        }
    }


    void HandleCardBattle(Message message)
    {
        try
        {
            byte typr=message.readByte();
            switch (typr)
            {
                case 1:// me draw start
                    {
                        int playerid = message.readInt();
                        int count = message.readByte();
                        Debug.Log("davao"+ playerid);
                        for(int i = 0; i < count; i++)
                        {
                            CardIntance cardIntance = new CardIntance();
                            cardIntance.InstanceId = message.readInt();
                            cardIntance.CardId = message.readInt();
                            if(playerid== gameData.CurrentRoom.HostPlayer.PlayerID)
                            {
                                gameData.CurrentRoom.HostPlayer.Hand.Add(cardIntance);
                            }
                            else
                            {
                                gameData.CurrentRoom.GuestPlayer.Hand.Add(cardIntance);
                            }
                        }
                        UIMainField uIMainField = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
                        if(uIMainField != null)
                        {
                            if (playerid == gameData._mainPlayer._playerid)
                            {
                                uIMainField.MeDrawCard();
                            }
                            else
                            {
                                uIMainField.EnemyDrawCard();
                            }
                        }
                        
                    }
                    break;
                case 2: 
                    {
                       
                    }
                    break;
            }

        }catch (Exception ex)
        {
            MainLog.LogError("Xảy ra lỗi", ex.Message, ReadColor.Chocolate);
        }
    }
}
