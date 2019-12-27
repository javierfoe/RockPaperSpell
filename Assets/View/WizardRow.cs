using System.Collections;
using UnityEngine;

namespace RockPaperSpell.View
{
    public class WizardRow : Wizard
    {
        private RectTransform wizard;

        public IEnumerator MoveTo(int position, float time)
        {
            yield return new WaitForSeconds(1);
            Transform newPosition = transform.GetChild(position);
            if (newPosition.childCount < 1)
            {
                wizard.SetParent(newPosition);
                yield return Lerp(time);
            }
        }

        private void Start()
        {
            wizard = wizardImage.rectTransform;
        }

        private IEnumerator Lerp(float timeToMove)
        {
            yield return null;
            Vector2 left = wizard.offsetMin;
            Vector2 right = wizard.offsetMax;
            float time = 0f;
            while (time < 1)
            {
                time += Time.deltaTime / timeToMove;
                wizard.offsetMin = Vector3.Lerp(left, Vector2.zero, time);
                wizard.offsetMax = Vector3.Lerp(right, Vector2.zero, time);
                yield return null;
            }
        }
    }
}