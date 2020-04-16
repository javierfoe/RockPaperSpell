using RockPaperSpell.Network;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.UI
{
    public class JoinGame : NetworkManagerButton
    {
        [SerializeField] private InputField inputField = null;

        protected override void Click()
        {
            networkManager.networkAddress = inputField.text;
            networkManager.StartClient();
        }
    }
}