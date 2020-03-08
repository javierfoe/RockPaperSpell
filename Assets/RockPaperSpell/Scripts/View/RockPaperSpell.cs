using UnityEngine;

namespace RockPaperSpell.View
{
    public class RockPaperSpell : MonoBehaviour, Interface.View
    {
        public static bool CanCast, CastingSpell;
        public static WizardToken LocalPlayer;

        [Header("Board")]
        [SerializeField] private Board board = null;
        [Header("Wizard Party")]
        [SerializeField] private WizardParty wizardParty = null;
        [Header("Spell Book")]
        [SerializeField] private SpellBook spellBook = null;

        public Interface.Wizard this[int i] => wizardParty[i];
        public Interface.SpellBook SpellBook => spellBook;

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