using UnityEngine;

namespace RockPaperSpell.Controller
{
    public abstract class Controller<T> : MonoBehaviour where T : class
    {
        protected T view;

        public void SetView(T view)
        {
            this.view = view;
        }
    }
}
