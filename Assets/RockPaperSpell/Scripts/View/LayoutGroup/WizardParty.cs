using System;

namespace RockPaperSpell.View
{
    public class WizardParty : HorizontalLayoutGroup<WizardToken>
    {
        public int IndexOf(Interface.WizardView element)
        {
            return Array.IndexOf(children, element);
        }
    }
}