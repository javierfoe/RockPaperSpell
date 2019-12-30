using UnityEngine;

namespace RockPaperSpell.View
{
    public class VerticalLayoutGroup<T> : HorizontalOrVerticalLayoutGroup<T> where T : MonoBehaviour
    {
        protected override float MaximumSize()
        {
            return (transform as RectTransform).sizeDelta.y;
        }
        protected override void SetChildrenSize(float childSize) { }
        protected override void SetPadding(int padding)
        {
            layoutGroup.padding.top = padding;
            layoutGroup.padding.bottom = padding;
        }
    }
}