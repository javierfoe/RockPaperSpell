using UnityEngine;

namespace RockPaperSpell.View
{
    public abstract class HorizontalLayoutGroup<T> : HorizontalOrVerticalLayoutGroup<T> where T : IndexBehaviour
    {
        protected override float MaximumSize()
        {
            return (transform as RectTransform).sizeDelta.x;
        }

        protected override void SetPadding(int padding)
        {
            layoutGroup.padding.left = padding;
            layoutGroup.padding.right = padding;
        }
    }
}