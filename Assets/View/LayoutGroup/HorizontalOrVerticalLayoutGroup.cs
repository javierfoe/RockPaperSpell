using UnityEngine;

namespace RockPaperSpell.View
{
    [RequireComponent(typeof(UnityEngine.UI.HorizontalOrVerticalLayoutGroup))]
    public abstract class HorizontalOrVerticalLayoutGroup<T> : MonoBehaviour where T : MonoBehaviour
    {

        [SerializeField] protected int maximumChilds = 0;
        protected UnityEngine.UI.HorizontalOrVerticalLayoutGroup layoutGroup;
        protected T[] children;
        private float childSize;

        public T this[int index] => children[index];

        public void SetSpacingAndPadding(int amount)
        {
            float maximumSize = MaximumSize();
            childSize = maximumSize / maximumChilds;
            SetChildrenSize(childSize);
            float remaining = maximumSize - (childSize * amount);
            int padding = (int)(remaining / (amount + 1));
            float spacing = (remaining - padding * 2) / (amount - 1);
            layoutGroup.spacing = spacing;
            SetPadding(padding);
            RemoveRemaining(amount);
        }

        protected abstract float MaximumSize();

        protected abstract void SetPadding(int padding);

        protected virtual void SetChildrenSize(float childSize)
        {
            foreach (T child in children)
            {
                SetSize(child);
            }
        }

        public void RemoveRemaining(int amount)
        {
            for(int i = amount; i < children.Length; i++)
            {
                children[i].gameObject.SetActive(false);
            }
            T[] newArray = new T[amount];
            for(int i = 0; i < amount; i++)
            {
                newArray[i] = children[i];
            }
            children = newArray;
        }

        private void SetSize(T child)
        {
            RectTransform rect = child.transform as RectTransform;
            Vector2 size = rect.sizeDelta;
            rect.sizeDelta = new Vector2(childSize, size.y);
        }

        private void Awake()
        {
            children = GetComponentsInChildren<T>();
        }

        private void OnValidate()
        {
            layoutGroup = GetComponent<UnityEngine.UI.HorizontalOrVerticalLayoutGroup>();
        }
    }
}