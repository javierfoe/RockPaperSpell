using UnityEngine;

namespace RockPaperSpell.View
{
    public class RockPaperSpell : MonoBehaviour, Interface.RockPaperSpell
    {
        [Header("Board")]
        [SerializeField] private Board board = null;
        [Header("Wizard Party")]
        [SerializeField] private WizardParty wizardParty = null;
        [Header("Spell Book")]
        [SerializeField] private SpellBook spellBook = null;

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

        private void Awake()
        {
            SetView(6);
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