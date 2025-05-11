using System;
using UnityEngine;

namespace Core.Cards
{
    public interface ICard
    {
        string CardName { get; }
        int ManaCost { get; }
        Sprite Icon { get; }
        event Action<int> ManaCostChanged;
        void Play(Player owner, Vector3 position);
    }
}
