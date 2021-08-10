using Game.Characters;
using Game.Characters.Stats;
using Game.Characters.Stats.Commons;
using Game.Characters.Stats.Factories;
using Game.Characters.Stats.Utils;
using Game.Characters.Teams;
using Game.Spells;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class ReboundHealSpellTest
    {
        [Test]
        public void Test_Heal_Enemy()
        {
            var statsFactory = Substitute.For<IStatCollectionFactory>();
            var caster = new Mage(statsFactory);
            var enemy = new Warrior(statsFactory);

            var team1 = new Team(1);
            var team2 = new Team(2);

            var healSpell = new HealReboundSpell(caster);

            team1.AddMembers(caster);
            team2.AddMembers(enemy);
            team1.AddEnemies(team2);

            healSpell.SelectTarget(enemy);
            healSpell.Cast();

            enemy.Health.DidNotReceive().Restore(default);
        }

        [Test]
        public void Test_Heal_2_People()
        {
            var healthStat = new Stat(new StatCalculator(), EStat.Health, 200, 1000);
            var caster = new Mage(new Stats(healthStat));
            var warrior1 = new Warrior(new Stats(healthStat));
            var warrior2 = new Warrior(new Stats(healthStat));
            var healSpell = new HealReboundSpell(caster);
            var valueToHeal = 100 + healSpell.HealAmount * 2; //Will affect each warrior 2 times.
            var team = new Team(0);

            team.AddMembers(caster, warrior1, warrior2);

            warrior1.View.transform.position = new Vector3(5, 0, 0);
            warrior2.View.transform.position = new Vector3(6, 0, 0);

            warrior1.Health.Reduce(100);
            warrior2.Health.Reduce(100);

            healSpell.SelectTarget(warrior1);
            healSpell.Cast();

            Assert.AreEqual(valueToHeal, warrior1.Health.Value);
            Assert.AreEqual(valueToHeal, warrior2.Health.Value);
        }

        [Test]
        public void Test_Heal_5_People()
        {
            var healthStat = new Stat(new StatCalculator(), EStat.Health, 200, 1000);
            var caster = new Mage(new Stats());
            var team = new Team(0);
            var healSpell = new HealReboundSpell(caster);
            var warriorsList = new List<Character>();

            team.AddMembers(caster);

            for (int i = 0; i < 5; i++)
            {
                var warrior = new Warrior(new Stats(healthStat));
                warriorsList.Add(warrior);
                team.AddMembers(warrior);

                warrior.Health.Reduce(100);
                warrior.View.transform.position = Vector3.right * Fact(5 - i);
            }

            healSpell.SelectTarget(warriorsList[0]);
            healSpell.Cast();

            Assert.AreEqual(150, warriorsList[0].Health.Value);
            Assert.AreEqual(150, warriorsList[1].Health.Value);
            Assert.AreEqual(150, warriorsList[2].Health.Value);
            Assert.AreEqual(150, warriorsList[3].Health.Value);
            Assert.AreEqual(100, warriorsList[4].Health.Value);
        }

        [Test]
        public void Test_Heal_5_People_With_Caster_Inthe_Middle()
        {
            var statsFactory = Substitute.For<IStatCollectionFactory>();
            var healthStat = new Stat(new StatCalculator(), EStat.Health, 200, 1000);
            var caster = new Mage(statsFactory);
            var team = new Team(0);
            var healSpell = new HealReboundSpell(caster);
            var warriorsList = new List<Character>();

            team.AddMembers(caster);

            for (int i = 0; i < 5; i++)
            {
                var warrior = new Warrior(statsFactory);
                warriorsList.Add(warrior);
                team.AddMembers(warrior);

                warrior.Health.Reduce(100);
                warrior.View.transform.position = Vector3.right * Fact(5 - i);
            }

            caster.Health.Reduce(100);
            caster.View.transform.position = Vector3.right * 50;

            healSpell.SelectTarget(warriorsList[0]);
            healSpell.Cast();

            Assert.AreEqual(150, warriorsList[0].Health.Value);
            Assert.AreEqual(150, warriorsList[1].Health.Value);
            Assert.AreEqual(150, warriorsList[2].Health.Value);
            Assert.AreEqual(100, warriorsList[3].Health.Value);
            Assert.AreEqual(100, warriorsList[4].Health.Value);
            Assert.AreEqual(150, caster.Health.Value);
        }

        [Test]
        public void Test_Heal_6_People_With_Enemies_Inthe_Middle()
        {
            var statsFactory = Substitute.For<IStatCollectionFactory>();
            var caster = new Mage(statsFactory);
            var team = new Team(0);
            var enemyTeam = new Team(1);
            var healSpell = new HealReboundSpell(caster);
            var warriorsList = new List<Character>();

            team.AddMembers(caster);
            team.AddEnemies(enemyTeam);

            caster.View.transform.position = Vector3.left * 100;

            for (int i = 0; i < 6; i++)
            {
                var warrior = new Warrior(statsFactory);
                warriorsList.Add(warrior);

                if (i % 2 == 0) team.AddMembers(warrior);
                else enemyTeam.AddMembers(warrior);

                warrior.Health.Reduce(100);
                warrior.View.transform.position = Vector3.right * Fact(6 - i);
            }

            var counter = 0;
            //2nd warrior should be healed 2 times.
            warriorsList[2].Health.When(x => x.Restore(default)).Do(x => counter++);

            healSpell.SelectTarget(warriorsList[0]);
            healSpell.Cast();

            Assert.AreEqual(150, warriorsList[0].Health.Value);
            Assert.AreEqual(100, warriorsList[1].Health.Value);
            Assert.AreEqual(100, warriorsList[3].Health.Value);
            Assert.AreEqual(150, warriorsList[4].Health.Value);
            Assert.AreEqual(100, warriorsList[5].Health.Value);

            Assert.AreEqual(2, counter);
        }

        int Fact(int number)
        {
            return number == 0 ? 1 : number * Fact(number - 1);
        }
    }
}
