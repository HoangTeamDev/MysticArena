using Menu.System;
using RoomAll;
using System;
using System.Collections.Generic;
using UI.SystemUI;
using UI.UIWindow;
using UnityEngine;

public class GameBattleRead : MonoBehaviour
{
    private GameData gameData => GameData.Instance;
    UIMainField uIMainField => UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
    public void Handle(Message message)
    {
        MainLog.Log("Nhận mess", message.Command.ToString(), ReadColor.Green);
        switch (message.Command)
        {
            case 14://thay đổi card hand, field, mo, san
                {
                    HandleCardBattle(message);
                }
                break;
            case 15:// thay đổi phase
                {
                    HandlePhase(message);
                }
                break;
        }
    }
    void HandlePhase(Message message)
    {
        byte type = message.readByte();
        switch (type)
        {
            case 1:
                {
                    string text = message.readUTF();
                    uIMainField.TextChangePhase(text);
                }
                break;
            case 2:
                {

                }
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
                case 1://  draw start
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
                case 2: //draw card
                    {
                       int playerid= message.readInt();
                        int count= message.readByte();
                        List<CardIntance> card= new List<CardIntance>();
                        for (int i = 0; i < count; i++)
                        {
                            CardIntance cardIntance = new CardIntance();
                            cardIntance.InstanceId = message.readInt();
                            cardIntance.CardId = message.readInt();
                            card.Add(cardIntance);
                            if (playerid == gameData.CurrentRoom.HostPlayer.PlayerID)
                            {
                                gameData.CurrentRoom.HostPlayer.Hand.Add(cardIntance);
                            }
                            else
                            {
                                gameData.CurrentRoom.GuestPlayer.Hand.Add(cardIntance);
                            }
                        }
                        UIMainField uIMainField = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
                        if (uIMainField != null)
                        {
                            if (playerid == gameData._mainPlayer._playerid)
                            {
                                uIMainField.MeDrawCard(card);
                            }
                            else
                            {
                                uIMainField.EnemyDrawCard(card);
                            }
                        }
                    }
                    break;
            }

        }catch (Exception ex)
        {
            MainLog.LogError("Xảy ra lỗi", ex.Message, ReadColor.Chocolate);
        }
    }
}
