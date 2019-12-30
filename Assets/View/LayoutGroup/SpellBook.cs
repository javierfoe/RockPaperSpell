namespace RockPaperSpell.View
{
    public class SpellBook : HorizontalLayoutGroup<Spell>
    {
        public void AddSpellPrefabs(int amount)
        {
            amount = amount > maximumChilds ? maximumChilds : amount;
            SetSpacingAndPadding(amount);
        }

        public void SetSpellBookModel(Model.SpellBook spellBook)
        {
            spellBook.AddListenerNewSpell(AddSpell);
        }

        private void AddSpell(Model.Spell spell)
        {
            for (int i = 0; i < children.Length - 1; i++)
            {
                children[i].CopySpell(children[i + 1]);
            }
            children[children.Length - 1].SetSpell(spell);
        }
    }
}