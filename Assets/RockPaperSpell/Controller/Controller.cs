using UnityEngine;

namespace RockPaperSpell.Controller
{
    public abstract class Controller<T> : MonoBehaviour where T : class
    {
        [SerializeField] private GameObject viewGo = null;
        protected T view;

        protected void GetDependencies()
        {
            if (view != null || viewGo == null) return;
            view = viewGo.GetComponent<T>();
        }

        private void Awake()
        {
            GetDependencies();
        }

        private void OnValidate()
        {
            GetDependencies();
        }
    }
}
