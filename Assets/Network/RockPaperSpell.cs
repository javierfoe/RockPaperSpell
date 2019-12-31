using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    [RequireComponent(typeof(Controller.RockPaperSpell))]
    public class RockPaperSpell : NetworkManager
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

        [SerializeField] private int players = 0;
        private int connectedPlayers;
        private Controller.RockPaperSpell controller;
        private Wizard[] wizardControllers;
        private NetworkConnectionPlayer[] allPlayers;

        public override void OnStartServer()
        {
            connectedPlayers = 0;
            controller = GetComponent<Controller.RockPaperSpell>();
            controller.Setup(players);
            allPlayers = new NetworkConnectionPlayer[players];
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            if(wizardControllers == null)
                wizardControllers = controller.WizardControllers.GetComponentsInChildren<Wizard>();

            int i;
            for (i = 0; i < players && allPlayers[i] != null; i++) ;
            Wizard player = wizardControllers[i];
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