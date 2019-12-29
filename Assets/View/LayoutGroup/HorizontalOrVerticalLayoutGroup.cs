using UnityEngine;

namespace RockPaperSpell.View
{
    [RequireComponent(typeof(UnityEngine.UI.HorizontalOrVerticalLayoutGroup))]
    public abstract class HorizontalOrVerticalLayoutGroup<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T prefab;
        [SerializeField] protected int maximumChilds = 0;
        protected UnityEngine.UI.HorizontalOrVerticalLayoutGroup layoutGroup;
        private float childSize;

        public void SetSpacingAndPadding(int amount)
        {
            float width = (transform as RectTransform).sizeDelta.x;
            childSize = width / maximumChilds;
            float remaining = width - (childSize * amount);
            int padding = (int)(remaining / (amount + 1));
            float spacing = (remaining - padding * 2) / (amount - 1);
            layoutGroup.spacing = spacing;
            SetPadding(padding);
        }

        public T AddObject()
        {
            T newObject = Instantiate(prefab, transform);
            RectTransform rect = newObject.transform as RectTransform;
            Vector2 size = rect.sizeDelta;
            rect.sizeDelta = new Vector2(childSize, size.y);
            return newObject;
        }

        protected abstract void SetPadding(int padding);

        private void OnValidate()
        {
            layoutGroup = GetComponent<UnityEngine.UI.HorizontalOrVerticalLayoutGroup>();
        }
    }
}