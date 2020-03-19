using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class RockPaperSpell : NetworkBehaviour, Interface.View, Interface.Controller
    {
        [SerializeField] private View.RockPaperSpell rockPaperSpellViewGo = null;
        private Interface.View rockPaperSpellView;

        public Interface.WizardView this[int i] => GetWizard<Wizard>(i);
        public Interface.SpellBook SpellBook => transform.GetChild(1).GetComponent<SpellBook>();

        public T GetWizard<T>(int index)
        {
            return transform.GetChild(0).GetChild(index).GetComponent<T>();
        }

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
            Wizard aux;
            for (int i = 0; i < wizards.childCount; i++)
            {
                (this[i] as Wizard).SetView(rockPaperSpellView[i]);
                aux = GetWizard<Wizard>(i);
                rockPaperSpellView[i].SetController(aux);
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