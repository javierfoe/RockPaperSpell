using UnityEngine;

namespace RockPaperSpell.View
{
    [RequireComponent(typeof(UnityEngine.UI.HorizontalLayoutGroup))]
    public class HorizontalLayoutGroup : MonoBehaviour
    {
        [SerializeField] private int maximumChilds = 0;
        private float childSize;

        public void SetSpacingAndPadding(int amount)
        {
            float width = (transform as RectTransform).sizeDelta.x;
            childSize = width / maximumChilds;
            UnityEngine.UI.HorizontalLayoutGroup horizontalLayout = GetComponent<UnityEngine.UI.HorizontalLayoutGroup>();
            float remaining = width - (childSize * amount);
            int padding = (int)(remaining / (amount + 1));
            float spacing = (remaining - padding * 2) / (amount - 1);
            horizontalLayout.spacing = spacing;
            horizontalLayout.padding.left = padding;
            horizontalLayout.padding.right = padding;
        }

        public T AddObject<T>(T prefab) where T : MonoBehaviour
        {
            T newObject = Instantiate(prefab, transform);
            RectTransform rect = newObject.transform as RectTransform;
            Vector2 size = rect.sizeDelta;
            rect.sizeDelta = new Vector2(childSize, size.y);
            return newObject;
        }
    }
}