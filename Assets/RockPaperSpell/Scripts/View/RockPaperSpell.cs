using System.Collections;
using UnityEngine;

namespace RockPaperSpell.View
{
    public class RockPaperSpell : MonoBehaviour, Interface.View
    {
        internal static bool CanCast, CastingSpell;
        internal static Interface.WizardView LocalPlayer;
        internal static float SaturationOn, SaturationOff, BrightnessOn, BrightnessOff;
        private static int localPlayerIndex;

        public static void SetSpellTarget(WizardToken target, Spell spell)
        {
            Structs.SpellTarget spellTarget = new Structs.SpellTarget
            {
                target = target.Index,
                spell = spell.Index
            };
            LocalPlayer.SetTarget(target.GetStruct());
            LocalPlayer.SetSpell(spell.GetStruct());
            LocalPlayer.SetSpellTarget(localPlayerIndex, spellTarget);
        }

        [SerializeField] private float saturationOn = 0, saturationOff = 0, brightnessOn = 0, brightnessOff = 0;
        [SerializeField] private Color[] colors = null;
        [SerializeField] private Board board = null;
        [SerializeField] private WizardParty wizardParty = null;
        [SerializeField] private SpellBook spellBook = null;

        public Interface.SpellBook SpellBook => spellBook;
        public Interface.WizardView GetElement(int i)
        {
            return wizardParty.GetChild(i);
        }

        public void SetLocalPlayer(int index)
        {
            localPlayerIndex = index;
            LocalPlayer = GetElement(index);
        }

        public void EnableCast(bool value)
        {
            CanCast = value;
        }

        public IEnumerator SetView(int players)
        {
            Controller.GameController.SetWizardColors(colors);
            SetWizards(players);
            SetSpellBook(players);
#pragma warning disable CS0162 // Se detectó código inaccesible
            if (false) yield return null;
#pragma warning restore CS0162 // Se detectó código inaccesible
        }

        private void SetWizards(int players)
        {
            wizardParty.SetSpacingAndPadding(players);
            board.SetSpacingAndPadding(players);
        }

        private void SetSpellBook(int players)
        {
            spellBook.SetSpacingAndPadding(players);
        }

        private void OnValidate()
        {
            wizardParty = GetComponent<WizardParty>();
            board = transform.GetChild(0).GetComponent<Board>();
            wizardParty = transform.GetChild(1).GetComponent<WizardParty>();
            spellBook = transform.GetChild(2).GetComponent<SpellBook>();
        }

        private void Awake()
        {
            SaturationOn = saturationOn;
            SaturationOff = saturationOff;
            BrightnessOn = brightnessOn;
            BrightnessOff = brightnessOff;
        }
    }
}