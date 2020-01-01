﻿namespace RockPaperSpell.Controller
{
    public class SpellBook : Controller<Interfaces.SpellBook>
    {
        private Model.SpellBook spellBook;

        public void SetSpellBook(Model.SpellBook spellBook)
        {
            this.spellBook = spellBook;
            GetDependencies();
            spellBook.AddListenerNewSpell(view.AddSpell);
        }

        public void InitialState()
        {
            spellBook.InvokeEvents();
        }
    }
}