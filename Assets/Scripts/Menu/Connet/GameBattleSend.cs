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
        public void SendNomalSummon(int intanceId)
        {
            Message msg = new Message(14);
            msg.writeByte(3);
            msg.writeInt(intanceId);
          
            Send(msg);
        }
        public void SendSetTrap(int intanceId)
        {
            Message msg = new Message(14);
            msg.writeByte(4);
            msg.writeInt(intanceId);
            Send(msg);
        }
        //attack
        public void SendAttack(int attackerId, int targetId)
        {
            Message msg = new Message(14);
            msg.writeByte(5);
            msg.writeInt(attackerId);
            msg.writeInt(targetId);
            Send(msg);
        }
    }
}

