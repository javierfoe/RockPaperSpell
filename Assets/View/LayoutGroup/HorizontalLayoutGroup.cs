using UnityEngine;

namespace RockPaperSpell.View
{
    public abstract class HorizontalLayoutGroup<T> : HorizontalOrVerticalLayoutGroup<T> where T : MonoBehaviour
    {
        protected override void SetPadding(int padding)
        {
            layoutGroup.padding.left = padding;
            layoutGroup.padding.right = padding;
        }
    }
}