using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RockPaperSpell.Network
{
    public class NetworkManager : NetworkRoomManager
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

        private RockPaperSpell network = null;
        private Controller.RockPaperSpell controller = null;
        private int connectedPlayers, players;
        private bool hostConnect = true;
        private NetworkConnectionPlayer[] allPlayers;
        private bool showStartButton;

        public override void OnRoomServerPlayersReady()
        {
            // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
            if (isHeadless)
                base.OnRoomServerPlayersReady();
            else
                showStartButton = true;
        }

        public override void OnGUI()
        {
            if (IsSceneActive(RoomScene))
                GUI.Box(new Rect(10f, 180f, 520f, 150f), "PLAYERS");

            if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                // set to false to hide it in the game scene
                showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }

        public override GameObject OnRoomServerCreateGamePlayer(NetworkConnection conn, GameObject roomPlayer)
        {
            if (hostConnect)
            {
                HostConnected();
            }
            int i;
            for (i = 0; i < players && allPlayers[i] != null; i++) ;
            Wizard player = network.GetElement(i) as Wizard;
            allPlayers[i] = new NetworkConnectionPlayer(conn, player);
            return player.gameObject;
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            connectedPlayers++;
            if (connectedPlayers == players)
                controller.StartMatch();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            if (!clientLoadedScene)
            {
                if (!ClientScene.ready) ClientScene.Ready(conn);
                ClientScene.AddPlayer();
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            int i;
            for (i = 0; i < players && allPlayers[i].conn != conn; i++) ;
            allPlayers[i] = null;
            connectedPlayers--;
        }

        public override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void HostConnected()
        {
            hostConnect = false;
            players = Controller.RockPaperSpell.PlayerAmount;
            connectedPlayers = 0;
            controller.Setup();
            allPlayers = new NetworkConnectionPlayer[players];
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            controller = FindObjectOfType<Controller.RockPaperSpell>();
            if (controller != null) network = controller.Network;
        }
    }
}