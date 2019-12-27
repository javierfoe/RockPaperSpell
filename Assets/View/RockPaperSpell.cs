using UnityEngine;

namespace RockPaperSpell.View
{
    public class RockPaperSpell : MonoBehaviour
    {
        public static Color[] WizardColors { get; private set; }
        public static float SaturationOn { get; private set; }
        public static float SaturationOff { get; private set; }
        public static float BrightnessOn { get; private set; }
        public static float BrightnessOff { get; private set; }

        public int initialWizards;
        [Header("Highlighting")]
        [SerializeField] private float saturationOn = 0;
        [SerializeField] private float saturationOff = 0, brightnessOn = 0, brightnessOff = 0;
        [Header("Colors")]
        [SerializeField] private Color[] wizardColors = null;
        [Header("Wizards")]
        [SerializeField] private WizardToken wizardPrefab = null;
        [SerializeField] private Transform wizards = null;
        [Header("Wizard Rows")]
        [SerializeField] private WizardRow wizardRowPrefab = null;
        [SerializeField] private Transform wizardRows = null;
        [Header("Spell Book")]
        [SerializeField] private Spell spellPrefab = null;
        [SerializeField] private Transform spellBook = null;

        public void AddWizards(int players)
        {
            Color color;
            for(int i = 0; i < players; i++)
            {
                WizardToken wizard = Instantiate(wizardPrefab, wizards);
                WizardRow wizardRow = Instantiate(wizardRowPrefab, wizardRows);
                color = wizardColors[i];
                wizard.SetColor(color);
                wizardRow.SetColor(color);
                wizard.AddListenerPointerEnter(() => Highlight(wizard, wizardRow, true));
                wizard.AddListenerPointerExit(() => Highlight(wizard, wizardRow, false));
            }
        }

        public void AddSpellsToSpellBook(int players)
        {
            players = players > 5 ? 5 : players;
            for(int i = 0; i < players; i++)
            {
                Instantiate(spellPrefab, spellBook);
            }
        }

        private void Highlight(Wizard wizard, Wizard wizardRow, bool on)
        {
            wizard.Highlight(on);
            wizardRow.Highlight(on);
        }

        private void Awake()
        {
            Model.Dungeon.SetupBoard(initialWizards);
            WizardColors = wizardColors;
            SaturationOff = saturationOff;
            SaturationOn = saturationOn;
            BrightnessOff = brightnessOff;
            BrightnessOn = brightnessOn;
            AddWizards(initialWizards);
            AddSpellsToSpellBook(initialWizards);
        }
    }
}