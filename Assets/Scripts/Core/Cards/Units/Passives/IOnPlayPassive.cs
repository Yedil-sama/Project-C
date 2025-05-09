using UnityEngine;

namespace Core.Cards.Units.Passives
{
    public interface IOnPlayPassive : IPassive
    {
        void OnPlay(Vector3 position);
    }
}
