namespace RockPaperSpell.Model
{
    public class SpellLibrary
    {
        private SpellStack stack;
        private SpellBook book;

        public SpellLibrary(int players)
        {
            stack = new SpellStack();
            book = new SpellBook(players);
            InitialSpellBook(players);
        }

        public Spell WildSurge()
        {
            return stack.WildSurge();
        }

        public int SpellsBefore(Spell spell)
        {
            return book.SpellsBefore(spell);
        }

        public void RotateSpellBook()
        {
            Spell newSpell = stack.RandomSpell(book.ValidSpell());
            Spell discardedSpell = book.AddSpell(newSpell);
            if(discardedSpell != null)
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