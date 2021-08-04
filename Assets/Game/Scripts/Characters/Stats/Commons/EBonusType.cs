namespace Game.Characters.CharacterStats.Commons
{
    public enum EBonusType
    {
        Flat = 0, //Plus 
        UnscaledFlat, //Plus in the end of formula
        Multiply, //Multiply everything
        MultiplyBase //Multiply only base value then continue calculating
    }
}