using UnityEngine;

namespace RockPaperSpell.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public abstract class Button : MonoBehaviour
    {
        protected abstract void Click();

        // Start is called before the first frame update
        protected virtual void Start()
        {
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Click);
        }
    }
}