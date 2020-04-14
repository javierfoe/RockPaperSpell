using RockPaperSpell.Network;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.UI
{
    public class JoinGame : Button
    {
        [SerializeField] private InputField inputField = null;
        [SerializeField] private NetworkManager networkManager = null;

        protected override void Click()
        {
            networkManager.networkAddress = inputField.text;
            networkManager.StartClient();
        }
    }
}