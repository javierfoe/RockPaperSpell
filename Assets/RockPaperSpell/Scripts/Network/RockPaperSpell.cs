using Mirror;
using RockPaperSpell.Interface;
using System;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class RockPaperSpell : NetworkBehaviour, Interface.View, Interface.Controller
    {
        [SerializeField] private View.RockPaperSpell rockPaperSpellViewGo = null;
        private Interface.View rockPaperSpellView;
        private Wizard[] wizards;

        public Interface.SpellBook SpellBook => transform.GetChild(1).GetComponent<SpellBook>();
        public Interface.WizardView GetElement(int i)
        {
            GetDependencies();
            return wizards[i];
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
            GetDependencies();
            WizardView viewAux;
            for (int i = 0; i < wizards.Length; i++)
            {
                viewAux = rockPaperSpellView.GetElement(i);
                wizards[i].SetView(viewAux);
                viewAux.SetController(wizards[i]);
            }
            (SpellBook as SpellBook).SetView(rockPaperSpellView.SpellBook);
        }

        public void SetLocalPlayer(Wizard wizard)
        {
            GetDependencies();
            int index = Array.IndexOf(wizards, wizard);
            rockPaperSpellViewGo.SetLocalPlayer(index);
        }

        public override void OnStartClient()
        {
            rockPaperSpellView = rockPaperSpellViewGo.GetComponent<Interface.View>();
            SetViews();
        }

        private void GetDependencies()
        {
            if (wizards == null)
                wizards = transform.GetChild(0).GetComponentsInChildren<Wizard>(true);
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