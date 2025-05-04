using Core.Cards.Passives;
using UnityEngine;

namespace Core.Cards
{
    [CreateAssetMenu(menuName = "SO/Races/Undead", fileName = "Undead Race")]
    public class UndeadRace : ScriptableObject, IRace
    {
        public string RaceName => "Undead";
        [SerializeField] private ReviveOnDiePassive revivePassive;

        public virtual void ApplyRaceModifier(Unit unit)
        {
            var passiveInstance = Instantiate(revivePassive);
            unit.AddPassive(passiveInstance);
        }
    }

}
