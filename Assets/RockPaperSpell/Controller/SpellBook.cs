namespace RockPaperSpell.Controller
{
    public class SpellBook : Controller<Interface.SpellBook>
    {
        private Model.SpellBook spellBook;

        public void SetSpellBook(Model.SpellBook spellBook)
        {
            this.spellBook = spellBook;
            spellBook.AddListenerNewSpell(view.AddSpell);
        }

        public void InitialState()
        {
            spellBook.InvokeEvents();
        }
    }
}