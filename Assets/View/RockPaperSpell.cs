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

        [Header("Highlighting")]
        [SerializeField] private float saturationOn = 0;
        [SerializeField] private float saturationOff = 0, brightnessOn = 0, brightnessOff = 0;
        [Header("Colors")]
        [SerializeField] private Color[] wizardColors = null;
        [Header("Wizards")]
        [SerializeField] private WizardToken wizardPrefab = null;
        [SerializeField] private HorizontalLayoutGroup wizards = null;
        [Header("Wizard Rows")]
        [SerializeField] private WizardRow wizardRowPrefab = null;
        [SerializeField] private Transform wizardRows = null;
        [Header("Spell Book")]
        [SerializeField] private Spell spellPrefab = null;
        [SerializeField] private HorizontalLayoutGroup spellBook = null;
        
        public void SetView(int players)
        {
            AddWizards(players);
            AddSpellsToSpellBook(players);
        }

        private void OnValidate()
        {
            wizards = transform.GetChild(1).GetComponent<HorizontalLayoutGroup>();
            spellBook = transform.GetChild(2).GetComponent<HorizontalLayoutGroup>();
        }

        private void AddWizards(int players)
        {
            wizards.SetSpacingAndPadding(players);
            Color color;
            for (int i = 0; i < players; i++)
            {
                WizardToken wizard = wizards.AddObject(wizardPrefab);
                WizardRow wizardRow = Instantiate(wizardRowPrefab, wizardRows);
                wizard.WizardRow = wizardRow;
                color = wizardColors[i];
                wizard.SetColor(color);
            }
        }

        private void AddSpellsToSpellBook(int players)
        {
            int spells = players > 5 ? 5 : players;
            spellBook.SetSpacingAndPadding(spells);
            for (int i = 0; i < spells; i++)
            {
                spellBook.AddObject(spellPrefab);
            }
        }

        private void Awake()
        {
            WizardColors = wizardColors;
            SaturationOff = saturationOff;
            SaturationOn = saturationOn;
            BrightnessOff = brightnessOff;
            BrightnessOn = brightnessOn;
        }
    }
}