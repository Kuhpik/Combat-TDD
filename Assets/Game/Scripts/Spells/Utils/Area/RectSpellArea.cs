using Game.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spells.Utils
{
    public class RectSpellArea : SpellArea
    {
        public RectSpellArea(float size) : base(size) { }

        public override IEnumerable<Character> GetTargets(Vector3 point, IEnumerable<Character> CharactersOnTheField)
        {
            var targets = new List<Character>();

            foreach (var character in CharactersOnTheField)
            {
                var characterPos = character.View.transform.position;

                if (characterPos.x >= point.x - Size && characterPos.x <= point.x + Size &&
                    characterPos.z >= point.z - Size && characterPos.z <= point.z + Size)
                {
                    targets.Add(character);
                }
            }

            return targets;
        }
    }
}