namespace RockPaperSpell.View
{
    public class SpellBook : HorizontalLayoutGroup<Spell>
    {
        private Spell[] spells;

        public void AddSpellPrefabs(int amount)
        {
            amount = amount > maximumChilds ? maximumChilds : amount;
            SetSpacingAndPadding(amount);
            spells = new Spell[amount];
            for (int i = 0; i < amount; i++)
            {
                spells[i] = AddObject();
            }
        }

        public void SetSpellBookModel(Model.SpellBook spellBook)
        {
            spellBook.AddListenerNewSpell(AddSpell);
        }

        private void AddSpell(Model.Spell spell)
        {
            for (int i = 0; i < spells.Length - 1; i++)
            {
                spells[i].CopySpell(spells[i + 1]);
            }
            spells[spells.Length - 1].SetSpell(spell);
        }
    }
}