using Game.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Spells
{
    public abstract class ReboundSpell : ISpell
    {
        protected abstract int Rebounds { get; }
        readonly protected Character _caster;
        protected Character _target;

        public ReboundSpell(Character caster)
        {
            _caster = caster;
        }

        public virtual void Cast()
        {
            if (_target == null)
            {
                Debug.LogError("Target was not set");
            }

            if (_target.Team.Members.Count == 1)
            {
                Affect(_target);
            }

            else
            {
                for (int i = 0; i < Rebounds + 1; i++) //Affect one time + Reboun count
                {
                    Affect(_target);
                    _target = GetClosest(_target.Team.Members);
                }
            }
        }

        public virtual void SelectTarget(Character target)
        {
            _target = target;
        }

        public abstract void Affect(Character target);

        protected Character GetClosest(IEnumerable<Character> characters)
        {
            return characters.Except(new List<Character>() { _target }).OrderBy(x => Vector3.Distance(_target.View.transform.position, x.View.transform.position)).First();
        }
    }
}
