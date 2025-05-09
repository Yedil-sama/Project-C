using UnityEngine;

namespace Core.Cards
{
    public interface ICard
    {
        string CardName { get; }
        int ManaCost { get; }
        Sprite Icon { get; }
        void Play(Player owner, Vector3 position);
    }
}
