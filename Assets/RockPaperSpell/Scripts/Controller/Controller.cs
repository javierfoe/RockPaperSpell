namespace RockPaperSpell.Controller
{
    public abstract class Controller<T> where T : class
    {
        protected T view;

        public void SetView(T view)
        {
            this.view = view;
        }
    }
}
