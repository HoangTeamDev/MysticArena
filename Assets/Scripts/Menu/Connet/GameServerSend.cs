using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UIScripts.SystemUI;
using Menu.System;
namespace Menu.Connet
{
    public partial class ClientMain
    {


        public void SendLogin(string user, string pass)
        {
            Message msg = new Message(1);
            msg.writeUTF(user);
            msg.writeUTF(pass);
            Send(msg);
        }




        public void CreateTK(string user, string pass)
        {
            try
            {
                Message msg = new Message(2);
                msg.writeUTF(user);
                msg.writeUTF(pass);
                Send(msg);
            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }
        public void CreatePlayer(string name)
        {
            try
            {
                Message msg = new Message(3);
                msg.writeUTF(name);

                Send(msg);
            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }

        public void AddCardToDeck(int cardID)
        {
            Message msg = new Message(9);
            try
            {
                msg.writeByte(1);
                msg.writeInt(cardID);
                Send(msg);

            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }
        public void RemoveCardFromDeck(int cardID)
        {
            Message msg = new Message(9);
            try
            {
                msg.writeByte(2);
                msg.writeInt(cardID);
                Send(msg);

            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }

        public void SendEffCard(int cardID)
        {
            Message msg = new Message(12);
            try
            {
                msg.writeInt(cardID);
                Send(msg);
            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }
        public void SendCreateRoom()
        {
            Message msg = new Message(13);
            try
            {
                msg.writeByte(1);
                Send(msg);
            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }
        public void SendJoinRoom(int roomID)
        {
            Message msg = new Message(13);
            try
            {
                msg.writeByte(2);
                msg.writeInt(roomID);
                Send(msg);
            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }
        public void SendLeaveRoom()
        {
            Message msg = new Message(13);
            try
            {
                msg.writeByte(3);
                Send(msg);
            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }
        public void SendStartGame()
        {
            Message msg = new Message(13);
            try
            {
                msg.writeByte(5);
                msg.writeBool(true);
                Send(msg);
            }
            catch (Exception ex)
            {
                MainLog.LogError("Xảy ra lỗi ", "case 4", ReadColor.Blue);
            }
        }
    }

}
