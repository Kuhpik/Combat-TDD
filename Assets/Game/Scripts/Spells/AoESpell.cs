using Game.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spells
{
    public abstract class AoESpell : ISpell
    {
        public Vector2 TargetPoint { get; set; }
        public abstract float Area { get; }
        public IEnumerable<Character> CharactersOnTheField;
        protected IList<Character> _targets;

        public virtual void Cast()
        {
            _targets = GetTargets();
        }

        protected virtual IList<Character> GetTargets()
        {
            var targets = new List<Character>();
            var point = TargetPoint;

            foreach (var character in CharactersOnTheField)
            {
                var characterPos = character.View.transform.position;

                if (characterPos.x >= point.x - Area && characterPos.x <= point.x + Area &&
                    characterPos.z >= point.y - Area && characterPos.z <= point.y + Area)
                {
                    targets.Add(character);
                }
            }

            return targets;
        }

        public virtual void Target()
        {
            if (CharactersOnTheField == null)
            {
                Debug.LogError("Must provide characters to calculate");
            }
        }
    }
}