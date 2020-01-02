using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class RockPaperSpell : NetworkBehaviour, Interface.RockPaperSpell
    {
        [SerializeField] private GameObject viewComponent = null;
        private Interface.RockPaperSpell rockPaperSpellView;

        public void SetView(int players)
        {
            RpcSetView(players);
        }

        public override void OnStartClient()
        {
            rockPaperSpellView = viewComponent.GetComponent<Interface.RockPaperSpell>();
        }

        [ClientRpc]
        private void RpcSetView(int players)
        {
            rockPaperSpellView.SetView(players);
        }
    }
}