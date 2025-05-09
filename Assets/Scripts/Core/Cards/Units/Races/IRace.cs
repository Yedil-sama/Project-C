namespace Core.Cards.Units.Races
{
    public interface IRace
    {
        string RaceName { get; }

        void ApplyRaceModifier(Unit unit); 
    }
}
