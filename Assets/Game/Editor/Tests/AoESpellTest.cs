using Game.Characters;
using Game.Characters.Stats;
using Game.Characters.Stats.Commons;
using Game.Characters.Stats.Utils;
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
            var healthStat = new Stat(new StatCalculator(), EStat.Health, dummyHealth, 1000);

            var warrior1 = new Warrior(new Stats(healthStat));
            var warrior2 = new Warrior(new Stats(healthStat));
            var warrior3 = new Warrior(new Stats(healthStat));

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

            Assert.AreEqual(healthLeft, warrior1.Health.Value);
            Assert.AreEqual(healthLeft, warrior2.Health.Value);
            Assert.AreEqual(dummyHealth, warrior3.Health.Value);
        }

        [Test]
        public void Circle_Spell_Hit_3_Warriors_2_Out_Of_Area()
        {
            var dummyHealth = 200;
            var healthStat = new Stat(new StatCalculator(), EStat.Health, dummyHealth, 1000);

            var warrior1 = new Warrior(new Stats(healthStat));
            var warrior2 = new Warrior(new Stats(healthStat));
            var warrior3 = new Warrior(new Stats(healthStat));
            var warrior4 = new Warrior(new Stats(healthStat));
            var warrior5 = new Warrior(new Stats(healthStat));

            var charactersOnTheField = new List<Character>() { warrior1, warrior2, warrior3, warrior4, warrior5 };

            var targetingService = new AreaTargetingService(new CircleSpellArea(2), charactersOnTheField);
            var spell = new AoEBurstDamageSpell(targetingService, 50);
            var healthLeft = dummyHealth - spell.Damage;

            targetingService.SetCenterPoint(new Vector3(4, 0, 4));

            //Place
            warrior1.View.transform.position = new Vector3(3, 0, 5);
            warrior2.View.transform.position = new Vector3(4, 0, 6);
            warrior3.View.transform.position = new Vector3(6, 0, 2);

            var pointOnRadius45Degree = 4 + Mathf.Sqrt(2) / 2 * spell.Area.Size;
            warrior4.View.transform.position = new Vector3(pointOnRadius45Degree, 0, pointOnRadius45Degree);
            warrior5.View.transform.position = new Vector3(pointOnRadius45Degree + 0.1f, 0, pointOnRadius45Degree + 0.1f);

            spell.Cast();

            Assert.AreEqual(healthLeft, warrior1.Health.Value);
            Assert.AreEqual(healthLeft, warrior2.Health.Value);
            Assert.AreEqual(dummyHealth, warrior3.Health.Value);
            Assert.AreEqual(healthLeft, warrior4.Health.Value);
            Assert.AreEqual(dummyHealth, warrior5.Health.Value);
        }
    }
}
