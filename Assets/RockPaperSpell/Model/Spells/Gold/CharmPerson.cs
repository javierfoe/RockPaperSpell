using System.Collections.Generic;
using UnityEngine;

namespace RockPaperSpell.Model
{
    public class CharmPerson : GoldSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            List<Wizard> poorers = RockPaperSpell.PoorerWizards(target);
            if (poorers.Count > 0)
            {
                int random = Random.Range(0, poorers.Count);
                target.Gold -= 2;
                poorers[random].Gold += 2;
            }
        }
    }
}