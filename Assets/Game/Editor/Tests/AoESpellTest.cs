using Game.Characters;
using Game.Spells;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class AoESpellTest
    {
        [Test]
        public void AoESpellCheckDamage()
        {
            var stats = new Stats() { Health = 50, Damage = 10, SpellDamage = 20 };
            var mage = new Mage(stats);
            var spell = new AoEBurstDamageSpell(mage);

            Assert.AreEqual(mage.Stats.SpellDamage * spell.Factor, spell.Damage);
        }

        [Test]
        public void Hit_2_Warriors_1_Out_Of_Area()
        {
            var dummyHealth = 200;

            var warrior1 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var warrior2 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var warrior3 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var caster = new Mage(new Stats() { Health = 50, Damage = 10, SpellDamage = 50 });

            var spell = new AoEBurstDamageSpell(caster);
            var healthLeft = dummyHealth - spell.Damage;

            //Prepare
            spell.TargetPoint = new Vector2(4, 4);
            spell.CharactersOnTheField = new List<Character>() { warrior1, warrior2, warrior3 };

            //Place
            warrior1.View.transform.position = new Vector3(3, 0, 5);
            warrior2.View.transform.position = new Vector3(6, 0, 2);
            warrior3.View.transform.position = new Vector3(4, 0, 8);

            spell.Target();
            spell.Cast();

            Assert.AreEqual(warrior1.Stats.Health, healthLeft);
            Assert.AreEqual(warrior2.Stats.Health, healthLeft);
            Assert.AreEqual(warrior3.Stats.Health, dummyHealth);
        }
    }
}
