using System.Collections;
using UnityEngine;

namespace RockPaperSpell.View
{
    public class WizardRow : Wizard
    {
        private RectTransform wizard;

        public IEnumerator MoveTo(int position)
        {
            Transform newPosition = transform.GetChild(position);
            if (newPosition.childCount < 1)
            {
                wizard.SetParent(newPosition);
                yield return Controller.RockPaperSpell.Lerp(wizard);
            }
        }

        public void SetPosition(int position)
        {
            wizard.SetParent(transform.GetChild(position));
            wizard.offsetMax = Vector2.zero;
            wizard.offsetMin = Vector2.zero;
        }

        private void Awake()
        {
            wizard = wizardImage.rectTransform;
        }
    }
}