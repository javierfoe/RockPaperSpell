namespace RockPaperSpell.View
{
    public class SpellBook : HorizontalLayoutGroup<Spell>, Interface.SpellBook
    {
        public void AddSpell(Structs.Spell spell)
        {
            for (int i = 0; i < children.Length - 1; i++)
            {
                children[i].CopySpell(children[i + 1]);
            }
            children[children.Length - 1].SetSpell(spell);
        }
    }
}