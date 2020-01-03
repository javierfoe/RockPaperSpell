using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class Wizard : NetworkBehaviour, Interface.Wizard
    {
        public static Wizard LocalPlayer;

        [SerializeField] private View.Wizard wizardViewGo = null;

        [SyncVar(hook = nameof(SetGoldView))] private int gold;
        [SyncVar(hook = nameof(SetPositionView))] private int position;
        [SyncVar(hook = nameof(SetSpellView))] private Structs.Spell spell;
        [SyncVar(hook = nameof(SetTargetView))] private Structs.Wizard target;

        private Interface.Wizard wizardView;

        public void SetColor(Color color)
        {
            RpcSetColor(color);
        }

        public void SetInitialPosition(int position)
        {
            RpcSetPosition(position);
        }

        public void SetGold(int gold)
        {
            this.gold = gold;
        }

        public void SetPosition(int position)
        {
            this.position = position;
        }

        public void SetSpell(Structs.Spell spell)
        {
            this.spell = spell;
        }

        public void SetTarget(Structs.Wizard target)
        {
            this.target = target;
        }

        public void SetSpellTarget(Structs.SpellTarget spellTarget)
        {
            CmdSetSpellTarget(spellTarget);
        }

        public override void OnStartClient()
        {
            GetDependencies();
        }

        public override void OnStartLocalPlayer()
        {
            LocalPlayer = this;
        }

        private void OnValidate()
        {
            GetDependencies();
        }

        private void GetDependencies()
        {
            wizardView = wizardViewGo as Interface.Wizard;
        }

        private void SetGoldView(int gold)
        {
            wizardView?.SetGold(gold);
        }

        private void SetPositionView(int gold)
        {
            wizardView?.SetPosition(gold);
        }

        private void SetSpellView(Structs.Spell spell)
        {
            wizardView?.SetSpell(spell);
        }

        private void SetTargetView(Structs.Wizard target)
        {
            wizardView?.SetTarget(target);
        }

        [ClientRpc]
        private void RpcSetColor(Color color)
        {
            wizardView.SetColor(color);
        }

        [ClientRpc]
        private void RpcSetPosition(int position)
        {
            wizardView.SetInitialPosition(position);
        }

        [Command]
        private void CmdSetSpellTarget(Structs.SpellTarget spellTarget)
        {

        }
    }
}