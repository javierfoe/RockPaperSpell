using System.Collections.Generic;

namespace RockPaperSpell.Model
{
    public class Imprisonment : GoldSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            List<Wizard> poorers = Dungeon.PoorerWizards(target);
            int length = poorers.Count;
            for(int i = 0; i < length && target.Gold > 0; i++)
            {
                target.Gold -= 1;
                poorers[i].Gold += 1;
            }
        }
    }
}