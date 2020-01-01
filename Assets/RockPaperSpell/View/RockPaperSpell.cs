using UnityEngine;

namespace RockPaperSpell.View
{
    public class RockPaperSpell : MonoBehaviour
    {
        public static float WizardMovementTime { get; private set; }
        public static float SaturationOn { get; private set; }
        public static float SaturationOff { get; private set; }
        public static float BrightnessOn { get; private set; }
        public static float BrightnessOff { get; private set; }

        [Header("Wizard Movement Time")]
        [SerializeField] private float movementTime = 0;
        [Header("Highlighting")]
        [SerializeField] private float saturationOn = 0;
        [SerializeField] private float saturationOff = 0, brightnessOn = 0, brightnessOff = 0;
        [field: Header("Colors"), SerializeField] public Color[] Colors { get; private set; }
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
            for (int i = 0; i < players; i++)
            {
                WizardToken wizard = wizardParty[i];
                WizardRow wizardRow = board[i];
                wizard.WizardRow = wizardRow;
            }
        }

        private void SetSpellBook(int players)
        {
            spellBook.SetSpacingAndPadding(players);
        }

        private void Awake()
        {
            WizardMovementTime = movementTime;
            SaturationOff = saturationOff;
            SaturationOn = saturationOn;
            BrightnessOff = brightnessOff;
            BrightnessOn = brightnessOn;
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