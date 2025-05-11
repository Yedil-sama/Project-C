using UnityEngine;

namespace Core.Cards
{
    public interface IDraggable
    {
        void OnDragStart();
        void OnDrag(Vector3 position);
        void OnDragEnd(Vector3 position);
    }
}
