using Game.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spells.Utils
{
    public sealed class CircleSpellArea : SpellArea
    {
        float _radiusSquared;

        public CircleSpellArea(float size) : base(size) 
        {
            _radiusSquared = Mathf.Pow(size, 2);
        }

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
            var summ = Mathf.Pow(pointX, 2) + Mathf.Pow(pointZ, 2);
            var rounded = Mathf.Round(summ * 1000) / 1000; //Rounding to 3 digit

            return rounded <= _radiusSquared;
        }
    }
}