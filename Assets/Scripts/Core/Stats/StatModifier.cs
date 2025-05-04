namespace Core.Stats
{
    public struct StatModifier
    {
        public StatType StatType;
        public int Amount;
        public ModifierType ModifierType;

        public StatModifier(StatType statType, int amount, ModifierType modifierType = ModifierType.Flat)
        {
            StatType = statType;
            Amount = amount;
            ModifierType = modifierType;
        }
    }

}
