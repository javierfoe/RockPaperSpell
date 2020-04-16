using Mirror;

namespace RockPaperSpell.UI
{
    public abstract class NetworkManagerButton : Button
    {
        protected Network.NetworkManager networkManager;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            networkManager = NetworkManager.singleton as Network.NetworkManager;
        }
    }
}