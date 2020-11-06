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
                yield return Lerp(wizard);
            }
        }

        public void SetPosition(int position)
        {
            wizard.SetParent(transform.GetChild(position));
            wizard.offsetMax = Vector2.zero;
            wizard.offsetMin = Vector2.zero;
        }

        private IEnumerator Lerp(RectTransform wizard)
        {
            Vector2 left = wizard.offsetMin;
            Vector2 right = wizard.offsetMax;
            float time = 0f;
            while (time < 1)
            {
                time += Time.deltaTime / Controller.GameController.WizardMovementTime;
                wizard.offsetMin = Vector3.Lerp(left, Vector2.zero, time);
                wizard.offsetMax = Vector3.Lerp(right, Vector2.zero, time);
                yield return null;
            }
        }

        private void Awake()
        {
            wizard = wizardImage.rectTransform;
        }
    }
}