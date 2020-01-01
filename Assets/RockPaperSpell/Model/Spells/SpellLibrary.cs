namespace RockPaperSpell.Model
{
    public class SpellLibrary
    {
        public SpellBook SpellBook { get; private set; }

        private SpellStack stack;

        public SpellLibrary(int players)
        {
            stack = new SpellStack();
            SpellBook = new SpellBook(players);
            InitialSpellBook(players);
        }

        public Spell WildSurge()
        {
            return stack.WildSurge();
        }

        public int SpellsBefore(Spell spell)
        {
            return SpellBook.SpellsBefore(spell);
        }

        public void RotateSpellBook()
        {
            Spell newSpell = stack.RandomSpell(SpellBook.ValidSpell());
            Spell discardedSpell = SpellBook.AddSpell(newSpell);
            if (discardedSpell != null)
            {
                stack.AddSpell(discardedSpell);
            }
        }

        private void InitialSpellBook(int players)
        {
            if (players > 5) players = 5;
            for (int i = 0; i < players; i++)
            {
                RotateSpellBook();
            }
        }
    }
}