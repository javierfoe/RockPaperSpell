using UnityEngine;
using UnityEngine.EventSystems;

namespace RockPaperSpell.View
{
    [RequireComponent(typeof(WizardToken))]
    public class OnPointerHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private WizardToken wizard;

        public void OnPointerExit(PointerEventData eventData)
        {
            if (RockPaperSpell.CastingSpell) return;
            wizard.Highlight(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (RockPaperSpell.CastingSpell) return;
            wizard.Highlight(true);
        }

        private void Awake()
        {
            wizard = GetComponent<WizardToken>();
        }
    }
}