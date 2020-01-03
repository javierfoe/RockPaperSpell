using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class RockPaperSpell : NetworkBehaviour, Interface.RockPaperSpell
    {
        [SerializeField] private View.RockPaperSpell rockPaperSpellViewGo = null;
        private Interface.RockPaperSpell rockPaperSpellView;

        public Interface.Wizard this[int i] => transform.GetChild(0).GetChild(i).GetComponent<Wizard>();
        public Interface.SpellBook SpellBook => transform.GetChild(1).GetComponent<SpellBook>();

        public void SetView(int players)
        {
            RpcSetView(players);
        }

        public override void OnStartClient()
        {
            rockPaperSpellView = rockPaperSpellViewGo.GetComponent<Interface.RockPaperSpell>();
            SetViews();
        }

        private void SetViews()
        {
            Transform wizards = transform.GetChild(0);
            for (int i = 0; i < wizards.childCount; i++)
            {
                (this[i] as Wizard).SetView(rockPaperSpellView[i]);
            }
            (SpellBook as SpellBook).SetView(rockPaperSpellView.SpellBook);
        }

        [ClientRpc]
        private void RpcSetView(int players)
        {
            rockPaperSpellView.SetView(players);
        }
    }
}