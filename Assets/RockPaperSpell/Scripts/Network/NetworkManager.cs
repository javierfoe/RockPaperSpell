using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class NetworkManager : Mirror.NetworkManager
    {
        private class NetworkConnectionPlayer
        {
            public NetworkConnection conn;
            public Wizard behaviour;

            public NetworkConnectionPlayer(NetworkConnection conn, Wizard behaviour)
            {
                this.conn = conn;
                this.behaviour = behaviour;
            }
        }

        [SerializeField] private RockPaperSpell network = null;
        [SerializeField] private Controller.RockPaperSpell controller = null;
        private int connectedPlayers, players;
        private NetworkConnectionPlayer[] allPlayers;

        public override void OnStartServer()
        {
            players = Controller.RockPaperSpell.PlayerAmount;
            connectedPlayers = 0;
            controller.Setup();
            allPlayers = new NetworkConnectionPlayer[players];
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            int i;
            for (i = 0; i < players && allPlayers[i] != null; i++) ;
            Wizard player = network[i] as Wizard;
            allPlayers[i] = new NetworkConnectionPlayer(conn, player);
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
            connectedPlayers++;
            if (connectedPlayers == players)
                controller.StartMatch();
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            int i;
            for (i = 0; i < players && allPlayers[i].conn != conn; i++) ;
            allPlayers[i] = null;
            connectedPlayers--;
        }
    }
}