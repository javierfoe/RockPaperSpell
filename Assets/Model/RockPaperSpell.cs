using System.Collections.Generic;

namespace RockPaperSpell.Model
{
    public static class RockPaperSpell
    {
        public static Wizard[] Wizards { get; private set; }
        private static SpellLibrary spellLibrary;

        public static SpellBook SpellBook => spellLibrary.SpellBook;

        public static void SetupBoard(int players)
        {
            SetupSpells(players);
            SetupWizards(players);
        }

        public static void SplitLoot()
        {
            int closestPosition = -1;
            int secondClosestPosition = -1;
            int currentPosition;
            foreach (Wizard wizard in Wizards)
            {
                currentPosition = wizard.Position;
                if (currentPosition > closestPosition)
                {
                    secondClosestPosition = closestPosition;
                    closestPosition = currentPosition;
                }
                if (currentPosition > secondClosestPosition && currentPosition < closestPosition)
                {
                    secondClosestPosition = currentPosition;
                }
            }
            foreach (Wizard wizard in Wizards)
            {
                currentPosition = wizard.Position;
                if (currentPosition == closestPosition)
                {
                    wizard.Gold += 5;
                }
                else if (currentPosition == secondClosestPosition)
                {
                    wizard.Gold += 3;
                }
            }
        }

        public static void Reposition()
        {
            foreach(Wizard wizard in Wizards)
            {
                wizard.Reposition();
            }
        }

        public static Spell WildSurge()
        {
            return spellLibrary.WildSurge();
        }

        public static int WizardCountCloserToLoot(Wizard wizard)
        {
            int position = wizard.Position;
            int res = 0;
            foreach (Wizard other in Wizards)
            {
                res += other.Position > position ? 1 : 0;
            }
            return res;
        }

        public static int SpellsBefore(Spell spell)
        {
            return spellLibrary.SpellsBefore(spell);
        }

        public static int PoorerWizardCount(Wizard than)
        {
            int gold = than.Gold;
            int res = 0;
            foreach (Wizard wizard in Wizards)
            {
                res += wizard.Gold < gold ? 1 : 0;
            }
            return res;
        }

        public static List<Wizard> PoorerWizards(Wizard than)
        {
            int gold = than.Gold;
            List<Wizard> wizards = new List<Wizard>();

            Wizard aux = than;
            do
            {
                aux = aux.Next;
                if (aux.Gold < gold)
                {
                    wizards.Add(aux);
                }
            } while (aux != than);

            return wizards;
        }

        private static void SetupWizards(int players)
        {
            Wizards = new Wizard[players];
            for (int i = 0; i < players; i++)
                Wizards[i] = new Wizard();

            Wizard wizard;
            for (int i = 0; i < players; i++)
            {
                wizard = Wizards[i];
                wizard.Next = i + 1 < players ? Wizards[i + 1] : Wizards[0];
                wizard.Previous = i - 1 > -1 ? Wizards[i - 1] : Wizards[players - 1];
            }
        }

        private static void SetupSpells(int players)
        {
            spellLibrary = new SpellLibrary(players);
        }
    }
}