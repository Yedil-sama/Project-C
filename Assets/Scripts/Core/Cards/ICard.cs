using UnityEngine;

namespace Core.Cards
{
    public interface ICard
    {
        string CardName { get; }
        int ManaCost { get; }
        void Play(Vector3 targetPosition);
    }

}
