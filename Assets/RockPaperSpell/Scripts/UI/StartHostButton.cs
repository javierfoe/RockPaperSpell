using RockPaperSpell.Network;
using UnityEngine;

namespace RockPaperSpell.UI
{
    public class StartHostButton : Button
    {
        [SerializeField] private Slider slider = null;
        [SerializeField] private NetworkManager networkManager = null;

        protected override void Click()
        {
            networkManager.maxConnections = slider.Amount;
            networkManager.StartHost();
        }
    }
}