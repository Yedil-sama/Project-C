namespace Core.Cards.Units.BehaviorStrategy
{
    public interface IUnitBehavior
    {
        void OnEnter(UnitBrain brain, Unit unit);
        void Tick(UnitBrain brain, Unit unit);
        void OnExit(UnitBrain brain, Unit unit);
    }
}
