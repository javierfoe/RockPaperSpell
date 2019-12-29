using UnityEngine;

namespace RockPaperSpell.View
{
    public class VerticalLayoutGroup<T> : HorizontalOrVerticalLayoutGroup<T> where T : MonoBehaviour
    {
        protected override void SetPadding(int padding)
        {
            layoutGroup.padding.top = padding;
            layoutGroup.padding.bottom = padding;
        }
    }
}