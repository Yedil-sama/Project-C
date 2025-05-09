using UnityEngine;

namespace Core.Cards
{
    public abstract class Card : ScriptableObject, ICard
    {
        [SerializeField] private string cardName;
        public string CardName => cardName;

        [SerializeField] private int manaCost;
        public int ManaCost => manaCost;

        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;

        public abstract void Play(Player owner, Vector3 position);

        public virtual void OnDragStart() { }
        public virtual void OnDragEnd() { }
        public virtual void OnHighlight() { }
        public virtual void OnUnhighlight() { }
    }
}
