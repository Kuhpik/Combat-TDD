using Game.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spells.Utils
{
    public sealed class CircleSpellArea : SpellArea
    {
        public CircleSpellArea(float size) : base(size) { }

        public override IEnumerable<Character> GetTargets(Vector3 point, IEnumerable<Character> CharactersOnTheField)
        {
            var targets = new List<Character>();

            foreach (var character in CharactersOnTheField)
            {
                var characterPos = character.View.transform.position;

                if (IsInsideCircle(point, characterPos))
                {
                    targets.Add(character);
                }
            }

            return targets;
        }

        bool IsInsideCircle(Vector3 point, Vector3 pos)
        {
            var pointX = pos.x - point.x;
            var pointZ = pos.z - point.z;
            var radius = Size;

            return Mathf.Pow(pointX, 2) + Mathf.Pow(pointZ, 2) <= Mathf.Pow(radius, 2);
        }
    }
}