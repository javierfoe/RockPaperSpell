using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RockPaperSpell.Network
{
    public class NetworkManager : NetworkRoomManager
    {
        private RockPaperSpell network = null;
        private int players;
        private NetworkConnection[] allPlayers;
        private bool showStartButton;

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
            players = Controller.GameController.PlayerAmount;
            allPlayers = new NetworkConnection[players];
        }

        public override void OnStartClient()
        {
            if (roomPlayerPrefab == null || roomPlayerPrefab.gameObject == null)
                Debug.LogError("NetworkRoomManager no RoomPlayer prefab is registered. Please add a RoomPlayer prefab.");
            else
                NetworkClient.RegisterPrefab(roomPlayerPrefab.gameObject);

            OnRoomStartClient();
        }

        public override void OnRoomServerSceneChanged(string sceneName)
        {            
            if (GameplayScene.Contains(SceneManager.GetActiveScene().name) && sceneName.Equals(GameplayScene) )
            {
                StartCoroutine(Controller.GameController.StartGame());
            }
        }

        public override GameObject OnRoomServerCreateGamePlayer(NetworkConnection conn, GameObject roomPlayer)
        {
            if (network == null) network = FindObjectOfType<RockPaperSpell>();
            int i;
            for (i = 0; i < players && allPlayers[i] != conn; i++) ;
            Wizard player = network.GetElement(i) as Wizard;
            return player.gameObject;
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            if (!NetworkClient.ready) NetworkClient.Ready();
            NetworkClient.AddPlayer();
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