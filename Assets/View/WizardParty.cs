using UnityEngine;

namespace RockPaperSpell.View
{
    public class WizardParty : MonoBehaviour
    {
        [SerializeField] private Board board = null;
        [SerializeField] private WizardPartyStatus wizardPartyStatus = null;
        
        public void AddWizards(Model.Wizard[] wizards)
        {
            int players = wizards.Length;
            wizardPartyStatus.SetSpacingAndPadding(players);
            for (int i = 0; i < players; i++)
            {
                WizardToken wizard = wizardPartyStatus.AddObject();
                WizardRow wizardRow = board.AddObject();
                wizard.WizardRow = wizardRow;
                wizard.SetWizardModel(wizards[i]);
            }
        }
    }
}