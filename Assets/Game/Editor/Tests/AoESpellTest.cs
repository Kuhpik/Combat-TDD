using Game.Characters;
using Game.Spells;
using Game.Spells.Utils;
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
            var targetingService = new AreaTargetingService(new CircleSpellArea(2), new List<Character>());
            var spell1 = new AoEBurstDamageSpell(targetingService);
            var spell2 = new AoEBurstDamageSpell(targetingService, 50);

            spell1.SetDamage(100);

            Assert.AreEqual(spell1.Damage, 100);
            Assert.AreEqual(spell2.Damage, 50);
        }

        [Test]
        public void Rect_Spell_Hit_2_Warriors_1_Out_Of_Area()
        {
            var dummyHealth = 200;

            var warrior1 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var warrior2 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var warrior3 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });

            var charactersOnTheField = new List<Character>() { warrior1, warrior2, warrior3 };

            var targetingService = new AreaTargetingService(new RectSpellArea(2), charactersOnTheField);
            var spell = new AoEBurstDamageSpell(targetingService, 50);
            var healthLeft = dummyHealth - spell.Damage;

            targetingService.SetCenterPoint(new Vector3(4, 0, 4));

            //Place
            warrior1.View.transform.position = new Vector3(3, 0, 5);
            warrior2.View.transform.position = new Vector3(6, 0, 2);
            warrior3.View.transform.position = new Vector3(4, 0, 8);

            spell.Cast();

            Assert.AreEqual(warrior1.Stats.Health, healthLeft);
            Assert.AreEqual(warrior2.Stats.Health, healthLeft);
            Assert.AreEqual(warrior3.Stats.Health, dummyHealth);
        }

        [Test]
        public void Circle_Spell_Hit_3_Warriors_1_Out_Of_Area()
        {
            var dummyHealth = 200;

            var warrior1 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var warrior2 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var warrior3 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });
            var warrior4 = new Warrior(new Stats() { Health = dummyHealth, Damage = 0, SpellDamage = 0 });

            var charactersOnTheField = new List<Character>() { warrior1, warrior2, warrior3, warrior4 };

            var targetingService = new AreaTargetingService(new CircleSpellArea(2), charactersOnTheField);
            var spell = new AoEBurstDamageSpell(targetingService, 50);
            var healthLeft = dummyHealth - spell.Damage;

            targetingService.SetCenterPoint(new Vector3(4, 0, 4));

            //Place
            warrior1.View.transform.position = new Vector3(3, 0, 5);
            warrior2.View.transform.position = new Vector3(4, 0, 6);
            warrior3.View.transform.position = new Vector3(6, 0, 2);

            var pointOnRadius45Degree = 4 + Mathf.Sqrt(2) / 2 * (spell.Area.Size / 2);
            warrior4.View.transform.position = new Vector3(pointOnRadius45Degree, 0, pointOnRadius45Degree);

            spell.Cast();

            Assert.AreEqual(warrior1.Stats.Health, healthLeft);
            Assert.AreEqual(warrior2.Stats.Health, healthLeft);
            Assert.AreEqual(warrior3.Stats.Health, dummyHealth);
            Assert.AreEqual(warrior4.Stats.Health, healthLeft);
        }
    }
}
