using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class RockPaperSpell : NetworkBehaviour, Interface.RockPaperSpell
    {
        [SerializeField] private View.RockPaperSpell rockPaperSpellViewGo = null;
        private Interface.RockPaperSpell rockPaperSpellView;

        public Wizard this[int i] => transform.GetChild(0).GetChild(i).GetComponent<Wizard>();

        public void SetView(int players)
        {
            RpcSetView(players);
        }

        public override void OnStartClient()
        {
            rockPaperSpellView = rockPaperSpellViewGo.GetComponent<Interface.RockPaperSpell>();
        }

        [ClientRpc]
        private void RpcSetView(int players)
        {
            rockPaperSpellView.SetView(players);
        }
    }
}