﻿using Discretos_Level_Designer.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEngine_5._0.Client
{
    public class ClientSender
    {

        public static void SendDistroyedObject(int index)
        {
            string packet = CreateTCPpacket(index.ToString(), NetPlay.PacketType.distroyedObject);
            Client.SendTCP(packet);
        }

        public static void SendCreatedItem(float x, float y, int id, float vx, float vy)
        {
            string packet = CreateTCPpacket(x + ";" + y + ";" + id + ";" + vx + ";" + vy, NetPlay.PacketType.createItem);
            Client.SendTCP(packet);
        }

        public static void SendCollectedKey(int index, int playerID)
        {
            string packet = CreateTCPpacket(playerID + ";" + index.ToString(), NetPlay.PacketType.collectedKey);
            Client.SendTCP(packet);
        }

        public static void SendOpenDoor(int index, int playerID)
        {
            string packet = CreateTCPpacket(playerID + ";" + index.ToString(), NetPlay.PacketType.openDoor);
            Client.SendTCP(packet);
        }




        public static void SendPositionPlayer(int PlayerID, float x, float y, bool isRight)
        {
            string packet = CreateUDPpacket(PlayerID + ";" + x + ";" + y + ";" + isRight, NetPlay.PacketType.playerPosition);
            Client.SendUDP(packet);
        }



        private static string CreateTCPpacket(string data, NetPlay.PacketType type)
        {
            return "$" + ((int)type).ToString("0000") + " " + data;
        }

        private static string CreateUDPpacket(string data, NetPlay.PacketType type)
        {
            return "$" + ((int)type).ToString("0000") + " " + NetPlay.MyPlayerID() + " " + data;
        }


    }
}
