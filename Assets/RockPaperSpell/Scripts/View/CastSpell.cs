using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    [RequireComponent(typeof(Spell))]
    public class CastSpell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private static RectTransform canvas;

        private Spell spell;
        private GameObject ghostCard;
        private WizardToken currentTarget;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!RockPaperSpell.CanCast) return;
            RockPaperSpell.CastingSpell = true;
            CreateGhostCard(eventData);
        }

        private void CreateGhostCard(PointerEventData eventData)
        {
            ghostCard = Instantiate(gameObject, canvas.transform);

            RectTransform rt = ghostCard.transform as RectTransform;
            rt.sizeDelta = (transform as RectTransform).sizeDelta;

            MaskableGraphic[] graphics = ghostCard.GetComponentsInChildren<MaskableGraphic>();
            foreach (MaskableGraphic mg in graphics) mg.raycastTarget = false;

            ghostCard.transform.SetAsLastSibling();

            SetDraggedPosition(eventData);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            var rt = ghostCard.GetComponent<RectTransform>();
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = canvas.rotation;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!RockPaperSpell.CanCast || ghostCard == null)
            {
                DestroyGhost();
                return;
            }
            WizardToken drop = null;
            List<GameObject> hovered = eventData.hovered;
            GameObject hover;
            for (int i = 0; i < hovered.Count && drop == null; i++)
            {
                hover = hovered[i];
                drop = hover.GetComponent<WizardToken>();
            }

            if (currentTarget != null && drop != currentTarget)
            {
                currentTarget.Highlight(false);
            }

            currentTarget = null;
            if (drop != null && (Interface.WizardView)drop != RockPaperSpell.LocalPlayer)
            {
                currentTarget = drop;
                drop.Highlight(true);
            }

            SetDraggedPosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DestroyGhost();
            if (!RockPaperSpell.CanCast)
            {
                return;
            }
            if (currentTarget != null)
            {
                RockPaperSpell.SetSpellTarget(currentTarget, spell);
            }
        }

        private void DestroyGhost()
        {
            Destroy(ghostCard);
            RockPaperSpell.CastingSpell = false;
        }

        private void Awake()
        {
            if (!canvas) canvas = FindObjectOfType<Canvas>().transform as RectTransform;
            spell = GetComponent<Spell>();
        }
    }
}