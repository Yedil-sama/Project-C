using UnityEngine;

namespace Core.Cards.Units
{
    public class UnitFactory
    {
        private readonly Transform spawnParent;

        public UnitFactory(Transform spawnParent = null)
        {
            this.spawnParent = spawnParent;
        }

        public Unit CreateUnit(UnitData data, Vector3 position, Quaternion rotation, Player owner)
        {
            GameObject go = Object.Instantiate(data.prefab, position, rotation, spawnParent);
            Unit unit = go.GetComponent<Unit>();

            if (unit == null)
            {
                Debug.LogError($"Prefab {data.prefab.name} does NOT contain a Unit component!");
                Object.Destroy(go);
                return null;
            }

            unit.Initialize(owner);
            unit.SetHealth(data.maxHealth);

            data.ApplyStatsTo(unit);
            data.race?.ApplyRaceModifier(unit);
            data.ApplyPassivesTo(unit);

            return unit;
        }
    }
}
