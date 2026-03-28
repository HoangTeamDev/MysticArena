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

        [ContextMenu("GetALlCard")]
        public void GetallCard()
        {
            Message msg = new Message(9);

            Send(msg);
        }
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

    }

}
