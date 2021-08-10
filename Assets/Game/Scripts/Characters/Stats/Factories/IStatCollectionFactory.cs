namespace Game.Characters.Stats.Factories
{
    public interface IStatCollectionFactory
    {
        StatCollection Create(Character character);        
    }
}