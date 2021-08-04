using Game.Characters;
using Game.Characters.Teams;
using Game.Spells;
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
            var caster = new Mage(new Stats());
            var enemy = new Warrior(new Stats());

            var team1 = new Team(1);
            var team2 = new Team(2);

            var healSpell = new HealReboundSpell(caster);

            team1.AddMembers(caster);
            team2.AddMembers(enemy);
            team1.AddEnemies(team2);

            healSpell.SelectTarget(enemy);
            healSpell.Cast();

            Assert.AreEqual(enemy.Stats.Health, 0);
        }

        [Test]
        public void Test_Heal_2_People()
        {
            var caster = new Mage(new Stats());
            var warrior1 = new Warrior(new Stats());
            var warrior2 = new Warrior(new Stats());
            var healSpell = new HealReboundSpell(caster);
            var valueToHeal = healSpell.HealAmount * 2; //Will affect each warrior 2 times.
            var team = new Team(0);

            team.AddMembers(caster, warrior1, warrior2);

            warrior1.View.transform.position = new Vector3(5, 0, 0);
            warrior2.View.transform.position = new Vector3(6, 0, 0);

            healSpell.SelectTarget(warrior1);
            healSpell.Cast();

            Assert.AreEqual(warrior1.Stats.Health, valueToHeal);
            Assert.AreEqual(warrior2.Stats.Health, valueToHeal);
        }

        [Test]
        public void Test_Heal_5_People()
        {
            var caster = new Mage(new Stats());
            var team = new Team(0);
            var healSpell = new HealReboundSpell(caster);
            var warriorsList = new List<Character>();

            team.AddMembers(caster);

            for (int i = 0; i < 5; i++)
            {
                var warrior = new Warrior(new Stats());
                warriorsList.Add(warrior);
                team.AddMembers(warrior);

                warrior.View.transform.position = Vector3.right * Fact(5 - i);
            }

            healSpell.SelectTarget(warriorsList[0]);
            healSpell.Cast();

            Assert.AreEqual(warriorsList[0].Stats.Health, 50);
            Assert.AreEqual(warriorsList[1].Stats.Health, 50);
            Assert.AreEqual(warriorsList[2].Stats.Health, 50);
            Assert.AreEqual(warriorsList[3].Stats.Health, 50);
            Assert.AreEqual(warriorsList[4].Stats.Health, 0);
        }

        [Test]
        public void Test_Heal_5_People_With_Caster_Inthe_Middle()
        {
            var caster = new Mage(new Stats());
            var team = new Team(0);
            var healSpell = new HealReboundSpell(caster);
            var warriorsList = new List<Character>();

            team.AddMembers(caster);

            for (int i = 0; i < 5; i++)
            {
                var warrior = new Warrior(new Stats());
                warriorsList.Add(warrior);
                team.AddMembers(warrior);

                warrior.View.transform.position = Vector3.right * Fact(5 - i);
            }

            caster.View.transform.position = Vector3.right * 50;

            healSpell.SelectTarget(warriorsList[0]);
            healSpell.Cast();

            Assert.AreEqual(warriorsList[0].Stats.Health, 50);
            Assert.AreEqual(warriorsList[1].Stats.Health, 50);
            Assert.AreEqual(warriorsList[2].Stats.Health, 50);
            Assert.AreEqual(warriorsList[3].Stats.Health, 0);
            Assert.AreEqual(warriorsList[4].Stats.Health, 0);
            Assert.AreEqual(caster.Stats.Health, 50);
        }

        [Test]
        public void Test_Heal_6_People_With_Enemies_Inthe_Middle()
        {
            var caster = new Mage(new Stats());
            var team = new Team(0);
            var enemyTeam = new Team(1);
            var healSpell = new HealReboundSpell(caster);
            var warriorsList = new List<Character>();

            team.AddMembers(caster);
            team.AddEnemies(enemyTeam);

            caster.View.transform.position = Vector3.left * 100;

            for (int i = 0; i < 6; i++)
            {
                var warrior = new Warrior(new Stats());
                warriorsList.Add(warrior);

                if (i % 2 == 0) team.AddMembers(warrior);
                else enemyTeam.AddMembers(warrior);

                warrior.View.transform.position = Vector3.right * Fact(6 - i);
            }

            healSpell.SelectTarget(warriorsList[0]);
            healSpell.Cast();

            Assert.AreEqual(warriorsList[0].Stats.Health, 50);
            Assert.AreEqual(warriorsList[1].Stats.Health, 0);
            Assert.AreEqual(warriorsList[2].Stats.Health, 100);
            Assert.AreEqual(warriorsList[3].Stats.Health, 0);
            Assert.AreEqual(warriorsList[4].Stats.Health, 50);
            Assert.AreEqual(warriorsList[5].Stats.Health, 0);
        }

        int Fact(int number)
        {
            return number == 0 ? 1 : number * Fact(number - 1);
        }
    }
}
