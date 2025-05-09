using UnityEngine;

namespace Core.Cards.Spells
{
    public interface ISpell
    {
        void Cast(Player owner, Vector3 targetPosition);
    }
}
