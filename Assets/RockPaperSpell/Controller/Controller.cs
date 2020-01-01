using UnityEngine;

namespace RockPaperSpell.Controller
{
    public abstract class Controller<T> : MonoBehaviour where T : class
    {
        [SerializeField] private Component interfaceComponent = null;
        protected T view;

        protected void GetDependencies()
        {
            if (view != null) return;
            if (interfaceComponent != null && (view = interfaceComponent as T) != null) return;
            view = GetComponent<T>();
            interfaceComponent = view as Component;
            if (view == null)
                Debug.Log("No proper interface "+ typeof(T) +" found", gameObject);
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
