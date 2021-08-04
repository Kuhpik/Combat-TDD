using Game.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spells.Utils
{
    public abstract class SpellArea
    {
        public float Size { get; private set; }

        //Must never be zero
        public SpellArea(float size) 
        {
            SetSize(size);
        }

        public void SetSize(float size)
        {
            Size = size;
        }

        //Would be much easier with no math. Just collision check. And make it as a playmode tests.
        public abstract IEnumerable<Character> GetTargets(Vector3 point, IEnumerable<Character> CharactersOnTheField); 
    }
}
