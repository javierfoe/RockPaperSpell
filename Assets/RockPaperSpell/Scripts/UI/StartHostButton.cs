using RockPaperSpell.Network;
using UnityEngine;

namespace RockPaperSpell.UI
{
    public class StartHostButton : NetworkManagerButton
    {
        [SerializeField] private Slider slider = null;

        protected override void Click()
        {
            networkManager.maxConnections = slider.Amount;
            networkManager.StartHost();
        }
    }
}