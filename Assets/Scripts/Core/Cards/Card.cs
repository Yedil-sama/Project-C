using System;
using UnityEngine;

namespace Core.Cards
{
    public abstract class Card : ScriptableObject, ICard, IDraggable
    {
        [SerializeField] private string cardName;
        public string CardName => cardName;

        [SerializeField] private int manaCost;
        public int ManaCost
        {
            get => manaCost;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                manaCost = value;

                ManaCostChanged?.Invoke(manaCost);
            }
        }

        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;

        public event Action<int> ManaCostChanged;

        public abstract void Play(Player owner, Vector3 position);

        public virtual void OnDragStart()
        {
            //Debug.Log($"{CardName} dragging started.");
        }

        public virtual void OnDrag(Vector3 position)
        {
            //Debug.Log($"{CardName} dragging at position: {position}");
        }

        public virtual void OnDragEnd(Vector3 position)
        {
            //Debug.Log($"{CardName} dragging ended at position: {position}");
        }

        public virtual void OnHighlight()
        {

        }

        public virtual void OnUnhighlight()
        {

        }
    }
}
