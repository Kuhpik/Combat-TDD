namespace Game.Characters.CharacterStats.Factories
{
    public interface IStatFactory
    {
        Stats GetStats(Character character);        
    }
}