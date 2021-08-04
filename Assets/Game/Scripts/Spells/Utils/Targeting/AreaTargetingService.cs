using Game.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spells.Utils
{
    public sealed class AreaTargetingService
    {
        Vector3 _centerPoint;
        public readonly SpellArea Area;
        readonly IReadOnlyCollection<Character> _characterInBattle;

        //TODO: Create some battle class where we can get all characters in battle.
        public AreaTargetingService(SpellArea area, List<Character> charactersInBattle)
        {
            Area = area;
            _characterInBattle = charactersInBattle.AsReadOnly();
        }

        public void SetCenterPoint(Vector3 point)
        {
            _centerPoint = point;
        }

        public IEnumerable<Character> GetTargets()
        {
            return Area.GetTargets(_centerPoint, _characterInBattle);
        }
    }
}