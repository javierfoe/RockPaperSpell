﻿using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class WizardToken : Wizard, Interface.WizardView
    {
        [SerializeField] private WizardRow wizardRow = null;
        [SerializeField] private Text gold = null, spell = null;
        [SerializeField] private Image target = null;
        [SerializeField] private GameObject speedPotion = null, localPlayer = null;

        private Color color;
        private Interface.WizardController wizardController;

        public Structs.Wizard GetStruct()
        {
            return new Structs.Wizard
            {
                color = color
            };
        }

        public void SetLocalPlayer()
        {
            localPlayer.SetActive(true);
        }

        public void SetController(Interface.WizardController controller)
        {
            wizardController = controller;
        }

        public override void Highlight(bool on)
        {
            base.Highlight(on);
            wizardRow.Highlight(on);
        }

        public override void SetColor(Color color)
        {
            base.SetColor(color);
            this.color = color;
            wizardRow.SetColor(color);
        }

        public void SetSpeedPotion(bool speedPotion)
        {
            this.speedPotion.SetActive(speedPotion);
        }

        public void SetGold(int gold)
        {
            this.gold.text = gold.ToString();
        }

        public void SetSpell(Structs.Spell spell)
        {
            this.spell.text = spell.name;
        }

        public void SetTarget(Structs.Wizard target)
        {
            this.target.color = target.color;
        }

        public void SetPosition(int position)
        {
            StartCoroutine(wizardRow.MoveTo(position));
        }

        public void SetInitialPosition(int position)
        {
            wizardRow.SetPosition(position);
        }

        public void SetSpellTarget(int player, Structs.SpellTarget spellTarget)
        {
            wizardController.SetSpellTarget(player, spellTarget);
        }
    }
}