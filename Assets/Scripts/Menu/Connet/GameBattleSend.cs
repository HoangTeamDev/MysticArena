using UnityEngine;
namespace Menu.Connet
{
    public partial class ClientMain
    {

        public void SendConfirmDrawStart()
        {
            Message message = new Message(14);
            message.writeByte(1);
            message.writeBool(true);
            Send(message);
        }

        public void SendEndTurn()
        {
            Message message=new Message(15);
            message.writeByte(2);
            message.writeBool(true);
            Send(message);
        }
    }
}

