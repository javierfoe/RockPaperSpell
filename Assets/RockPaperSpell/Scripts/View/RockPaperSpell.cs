using UnityEngine;

namespace RockPaperSpell.View
{
    public class RockPaperSpell : MonoBehaviour, Interface.View
    {
        internal static bool CanCast, CastingSpell;
        internal static Interface.WizardView LocalPlayer;
        private static int localPlayerIndex;

        public static void SetSpellTarget(Structs.SpellTarget spellTarget)
        {
            LocalPlayer.SetSpellTarget(localPlayerIndex, spellTarget);
        }

        [Header("Board")]
        [SerializeField] private Board board = null;
        [Header("Wizard Party")]
        [SerializeField] private WizardParty wizardParty = null;
        [Header("Spell Book")]
        [SerializeField] private SpellBook spellBook = null;

        public Interface.WizardView this[int i] => wizardParty[i];
        public Interface.SpellBook SpellBook => spellBook;

        public void SetLocalPlayer(int index)
        {
            localPlayerIndex = index;
            LocalPlayer = this[index];
        }

        public void EnableCast(bool value)
        {
            CanCast = value;
        }

        public void SetView(int players)
        {
            SetWizards(players);
            SetSpellBook(players);
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
    }
}