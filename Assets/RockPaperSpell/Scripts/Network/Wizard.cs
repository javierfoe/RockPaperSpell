using Mirror;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class Wizard : NetworkBehaviour, Interface.WizardView, Interface.WizardController
    {
        [SyncVar(hook = nameof(SetGoldView))] private int gold;
        [SyncVar(hook = nameof(SetPositionView))] private int position;
        [SyncVar(hook = nameof(SetSpeedPotionView))] private bool speedPotion;
        [SyncVar(hook = nameof(SetSpellView))] private Structs.Spell spell;
        [SyncVar(hook = nameof(SetTargetView))] private Structs.Wizard target;

        private Interface.WizardView wizardView;
        private Interface.WizardController wizardController;

        public void SetLocalPlayer()
        {
            transform.parent.GetComponentInParent<RockPaperSpell>().SetLocalPlayer(this);
            wizardView.SetLocalPlayer();
        }

        public void SetController(Interface.WizardController controller)
        {
            wizardController = controller;
        }

        public void SetView(Interface.WizardView view)
        {
            wizardView = view;
        }

        public void SetSpeedPotion(bool speedPotion)
        {
            this.speedPotion = speedPotion;
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

        public void SetColor(Color color)
        {
            RpcSetColor(color);
        }

        public void SetSpellTarget(int player, Structs.SpellTarget spellTarget)
        {
            CmdSetSpellTarget(player, spellTarget);
        }

        public override void OnStartLocalPlayer()
        {
            SetLocalPlayer();
        }

        private void SetSpeedPotionView(bool oldSpeedPotion, bool newSpeedPotion)
        {
            wizardView?.SetSpeedPotion(newSpeedPotion);
        }

        private void SetGoldView(int oldGold, int newGold)
        {
            wizardView?.SetGold(newGold);
        }

        private void SetPositionView(int oldPosition, int newPosition)
        {
            wizardView?.SetPosition(newPosition);
        }

        private void SetSpellView(Structs.Spell oldSpell, Structs.Spell newSpell)
        {
            wizardView?.SetSpell(newSpell);
        }

        private void SetTargetView(Structs.Wizard oldTarget, Structs.Wizard newTarget)
        {
            wizardView?.SetTarget(newTarget);
        }

        [ClientRpc]
        private void RpcSetColor(Color color)
        {
            wizardView.SetColor(color);
        }

        [Command]
        private void CmdSetSpellTarget(int player, Structs.SpellTarget spellTarget)
        {
            wizardController.SetSpellTarget(player, spellTarget);
        }
    }
}