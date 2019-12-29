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
        [Header("Colors")]
        [SerializeField] private Color[] colors = null;
        [Header("Wizard Party")]
        [SerializeField] private WizardParty wizardParty = null;
        [Header("Spell Book")]
        [SerializeField] private SpellBook spellBook = null;
        
        public void SetView(Model.Wizard[] wizards)
        {
            int players = wizards.Length;
            AddWizards(wizards);
            AddSpellsToSpellBook(players);
        }

        public void SetSpellBook(Model.SpellBook spellBook)
        {
            this.spellBook.SetSpellBookModel(spellBook);
        }

        private void OnValidate()
        {
            wizardParty = GetComponent<WizardParty>();
            spellBook = transform.GetChild(2).GetComponent<SpellBook>();
        }

        private void AddWizards(Model.Wizard[] wizards)
        {
            for(int i = 0; i < wizards.Length; i++)
            {
                wizards[i].Color = colors[i];
            }
            wizardParty.AddWizards(wizards);
        }

        private void AddSpellsToSpellBook(int players)
        {
            spellBook.AddSpellPrefabs(players);
            spellBook.SetSpellBookModel(Model.RockPaperSpell.SpellBook);
        }

        private void Awake()
        {
            WizardMovementTime = movementTime;
            SaturationOff = saturationOff;
            SaturationOn = saturationOn;
            BrightnessOff = brightnessOff;
            BrightnessOn = brightnessOn;
        }
    }
}