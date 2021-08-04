using Game.Characters;
using Game.Spells.Utils;
using System.Collections.Generic;

namespace Game.Spells
{
    public abstract class AoESpell : ISpell
    {
        public readonly SpellArea Area;
        protected IEnumerable<Character> _targets;
        protected readonly AreaTargetingService _targetingService;

        public AoESpell(AreaTargetingService areaTargeting)
        {
            Area = areaTargeting.Area;
            _targetingService = areaTargeting;
        }

        public virtual void Cast()
        {
            _targets = _targetingService.GetTargets();
        }
    }
}