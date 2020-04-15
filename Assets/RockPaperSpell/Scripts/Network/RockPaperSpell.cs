using Mirror;
using RockPaperSpell.Interface;
using System;
using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class RockPaperSpell : NetworkBehaviour, Interface.View, Interface.Controller
    {
        [SerializeField] private View.RockPaperSpell rockPaperSpellViewGo = null;
        private Interface.View rockPaperSpellView;
        private Wizard[] wizards;
        private int playersReady;

        public Interface.SpellBook SpellBook => transform.GetChild(1).GetComponent<SpellBook>();
        public Interface.WizardView GetElement(int i)
        {
            GetDependencies();
            return wizards[i];
        }

        public IEnumerator SetView(int players)
        {
            playersReady = 0;
            while (playersReady < players) yield return null;
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

        public void PlayerReady()
        {
            playersReady++;
        }

        [ClientRpc]
        private void RpcEnableCast(bool value)
        {
            rockPaperSpellView.EnableCast(value);
        }

        [ClientRpc]
        private void RpcSetView(int players)
        {
            Debug.Log("RpcSetView");
            StartCoroutine(rockPaperSpellView.SetView(players));
        }
    }
}