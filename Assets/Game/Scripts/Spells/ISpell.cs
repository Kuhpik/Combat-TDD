namespace Game.Spells
{
    public interface ISpell
    {
        /// <summary>
        /// Cast spell when everything is ok
        /// </summary>
        void Cast();

        /// <summary>
        /// Call targeting scripts here
        /// </summary>
        void Target();
    }
}