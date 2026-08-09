using CardData;
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
                    int turn = message.readShort();
                    int playerid = message.readInt();
                    string text = message.readUTF();
                    uIMainField.TextChangePhase(text,turn,playerid);
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
                            int cardIntanceId  = message.readInt();
                            int cardid = message.readInt();
                            Card cardIntance = GameData.Instance.GetCardByID(cardid);
                            if(playerid== gameData.CurrentRoom.HostPlayer.PlayerID)
                            {
                                gameData.CurrentRoom.HostPlayer.Hand.Add(cardIntanceId, cardIntance);
                            }
                            else
                            {
                                gameData.CurrentRoom.GuestPlayer.Hand.Add(cardIntanceId, cardIntance);
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
                        Dictionary<int, Card> card= new Dictionary<int, Card>();
                        for (int i = 0; i < count; i++)
                        {
                            int cardIntanceId = message.readInt();
                            int cardid = message.readInt();
                            Card cardIntance = GameData.Instance.GetCardByID(cardid);
                            card.Add(cardIntanceId, cardIntance);
                            if (playerid == gameData.CurrentRoom.HostPlayer.PlayerID)
                            {
                                gameData.CurrentRoom.HostPlayer.Hand.Add(cardIntanceId, cardIntance);
                            }
                            else
                            {
                                gameData.CurrentRoom.GuestPlayer.Hand.Add(cardIntanceId, cardIntance);
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
                case 3://nomal summon
                    {
                        CardIntance cardIntance= new CardIntance();
                        int playerid = message.readInt();
                        cardIntance.SlotIndex = message.readInt();
                        cardIntance.InstanceId = message.readInt();
                        cardIntance.CardId = message.readInt() ;
                        cardIntance.CurrentHp=message.readInt();
                        cardIntance.CurrentAtk=message.readInt();
                        cardIntance.HasAttack=message.readBool();
                        uIMainField.NomalSummon( playerid, cardIntance);
                        
                    }
                    break;
                case 4://set trap
                    {
                        CardIntance cardIntance = new CardIntance();
                        int playerid = message.readInt();
                        cardIntance.SlotIndex = message.readInt();
                        cardIntance.InstanceId = message.readInt();
                        cardIntance.CardId = message.readInt();
                        uIMainField.SetTrap(playerid, cardIntance);

                    }
                    break;
                case 5:
                    {
                        int playeridattack = message.readInt();
                        int instanceidattack = message.readInt();
                        int curenthp = message.readInt();
                        bool isdestroy = message.readBool();
                        int playeriddefend = message.readInt();
                        int instanceiddefend = message.readInt();
                        int curenthpdefend = message.readInt();
                        bool isdestroydefend = message.readBool();
                        if(playeridattack ==GameData.Instance._mainPlayer._playerid)
                        {
                            CardIntance cardIntance = uIMainField.GetMonsterZoneMe(instanceidattack);
                           CardIntance cardIntance1 = uIMainField.GetMonsterZoneOther(instanceiddefend);
                            uIMainField.ShootFireball(cardIntance.transform.position, cardIntance1.transform.position);
                            cardIntance1.UpdateHP(curenthpdefend);
                        }
                        else
                        {
                            CardIntance cardIntance = uIMainField.GetMonsterZoneOther(instanceidattack);
                            CardIntance cardIntance1 = uIMainField.GetMonsterZoneMe(instanceiddefend);
                            uIMainField.ShootFireball(cardIntance.transform.position, cardIntance1.transform.position);
                            cardIntance1.UpdateHP(curenthpdefend);
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
