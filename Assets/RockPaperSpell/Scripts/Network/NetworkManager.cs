using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class NetworkManager : NetworkRoomManager
    {
        private RockPaperSpell network = null;
        private Controller.RockPaperSpell controller = null;
        private int players;
        private bool hostConnect = true;
        private NetworkConnection[] allPlayers;
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

        public override void OnStartServer()
        {
            players = Controller.RockPaperSpell.PlayerAmount;
            allPlayers = new NetworkConnection[players];
        }

        public override void OnStartClient()
        {
            if (roomPlayerPrefab == null || roomPlayerPrefab.gameObject == null)
                Debug.LogError("NetworkRoomManager no RoomPlayer prefab is registered. Please add a RoomPlayer prefab.");
            else
                ClientScene.RegisterPrefab(roomPlayerPrefab.gameObject);

            OnRoomStartClient();
        }

        public override void OnRoomServerSceneChanged(string sceneName)
        {
            controller = FindObjectOfType<Controller.RockPaperSpell>();
            if (controller != null) network = controller.Network;
            if (sceneName.Equals(GameplayScene))
            {
                controller.StartMatch();
            }
        }

        public override GameObject OnRoomServerCreateGamePlayer(NetworkConnection conn, GameObject roomPlayer)
        {
            if (hostConnect)
            {
                hostConnect = false;
            }
            int i;
            for (i = 0; i < players && allPlayers[i] != conn; i++) ;
            Wizard player = network.GetElement(i) as Wizard;
            return player.gameObject;
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            if (!ClientScene.ready) ClientScene.Ready(conn);
            ClientScene.AddPlayer();
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);
            int i;
            for (i = 0; i < players && allPlayers[i] != null; i++) ;
            allPlayers[i] = conn;
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            int i;
            for (i = 0; i < players && allPlayers[i] != conn; i++) ;
            allPlayers[i] = null;
        }
    }
}