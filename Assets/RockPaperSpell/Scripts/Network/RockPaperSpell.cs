using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class RockPaperSpell : NetworkBehaviour, Interface.View, Interface.Controller
    {
        [SerializeField] private View.RockPaperSpell rockPaperSpellViewGo = null;
        private Interface.View rockPaperSpellView;

        public Interface.Wizard this[int i] => transform.GetChild(0).GetChild(i).GetComponent<Wizard>();
        public Interface.SpellBook SpellBook => transform.GetChild(1).GetComponent<SpellBook>();

        public void SetView(int players)
        {
            RpcSetView(players);
        }

        public void EnableCast(bool value)
        {
            RpcEnableCast(value);
        }

        public void SetViews()
        {
            Transform wizards = transform.GetChild(0);
            for (int i = 0; i < wizards.childCount; i++)
            {
                (this[i] as Wizard).SetView(rockPaperSpellView[i]);
            }
            (SpellBook as SpellBook).SetView(rockPaperSpellView.SpellBook);
        }

        public override void OnStartClient()
        {
            rockPaperSpellView = rockPaperSpellViewGo.GetComponent<Interface.View>();
            SetViews();
        }

        [ClientRpc]
        private void RpcSetView(int players)
        {
            rockPaperSpellView.SetView(players);
        }

        [ClientRpc]
        private void RpcEnableCast(bool value)
        {
            rockPaperSpellView.EnableCast(value);
        }
    }
}