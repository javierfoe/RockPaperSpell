using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class WizardToken : Wizard, IPointerEnterHandler, IPointerExitHandler
    {
        private UnityEvent pointerEnter, pointerExit;
        [SerializeField] private Text gold = null, spell = null;
        [SerializeField] private Image target = null;

        public void SetGold(int gold)
        {
            this.gold.text = gold.ToString();
        }

        public void SetSpell(Model.Spell spell)
        {
            this.spell.text = spell.ToString();
        }

        public void SetTarget(int target)
        {
            this.target.color = RockPaperSpell.WizardColors[target];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerExit.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerEnter.Invoke();
        }

        public void AddListenerPointerEnter(UnityAction action)
        {
            if (pointerEnter == null) pointerEnter = new UnityEvent();
            pointerEnter.AddListener(action);
        }

        public void AddListenerPointerExit(UnityAction action)
        {
            if (pointerExit == null) pointerExit = new UnityEvent();
            pointerExit.AddListener(action);
        }
    }
}