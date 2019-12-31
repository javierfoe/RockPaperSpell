using UnityEngine;

namespace RockPaperSpell.Controller
{
    public abstract class Controller<T> : MonoBehaviour where T : class
    {
        [SerializeField] private Component component = null;
        protected T view;

        private void GetDependencies()
        {
            if (component != null && (view = component as T) != null) return;
            view = GetComponent<T>();
            component = view as Component;
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
