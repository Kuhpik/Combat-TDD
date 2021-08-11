using Game.Characters.Teams;
using Game.Spells;
using NSubstitute;
using NUnit.Framework;
using Tests.Helpers;
using UnityEngine;

namespace Tests
{
    public class ReboundHealSpellTest
    {
        [Test]
        public void Test_Heal_Enemy()
        {
            //Arrange
            var team1 = new Team(1);
            var team2 = new Team(2);
            var characters = new TestCharactersCreator().CreateCharacters(2);
            var healSpell = new HealReboundSpell(characters[0]);
            var valueToReduce = 100;
            var valueNotRestored = characters[1].Health.Value - valueToReduce;

            //Act
            team1.AddMembers(characters[0]);
            team2.AddMembers(characters[1]);
            team1.AddEnemies(team2);

            characters[1].Health.Reduce(valueToReduce);

            healSpell.SelectTarget(characters[1]);
            healSpell.Cast();

            //Assert
            Assert.AreEqual(valueNotRestored, characters[1].Health.Value);
        }

        [Test]
        public void Test_Heal_2_People()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(3);
            var healSpell = new HealReboundSpell(characters[0]);
            var valueToReduce = 200;
            var finalHealth = (characters[1].Health.Value - valueToReduce) + healSpell.HealAmount * 2; //Will affect each warrior 2 times.
            var team = new Team(0);

            //Act
            team.AddMembers(characters[0], characters[1], characters[2]);
            characters[1].View.transform.position = new Vector3(5, 0, 0);
            characters[2].View.transform.position = new Vector3(6, 0, 0);
            characters[1].Health.Reduce(valueToReduce);
            characters[2].Health.Reduce(valueToReduce);

            healSpell.SelectTarget(characters[1]);
            healSpell.Cast();

            //Assert
            Assert.AreEqual(finalHealth, characters[1].Health.Value);
            Assert.AreEqual(finalHealth, characters[2].Health.Value);
        }

        [Test]
        public void Test_Heal_5_People()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(6);
            var caster = characters[0];
            var team = new Team(0);
            var healSpell = new HealReboundSpell(caster);
            var valueToReduce = 100;
            var valueNotRestored = characters[1].Health.Value - valueToReduce;
            var finalHealth = valueNotRestored + healSpell.HealAmount;

            //Act
            team.AddMembers(caster);

            for (int i = 0; i < 5; i++)
            {
                var warrior = characters[i+1];
                team.AddMembers(warrior);

                warrior.Health.Reduce(valueToReduce);
                warrior.View.transform.position = Vector3.right * Fact(5 - i);
            }

            healSpell.SelectTarget(characters[1]);
            healSpell.Cast();

            //Assert
            Assert.AreEqual(finalHealth, characters[1].Health.Value);
            Assert.AreEqual(finalHealth, characters[2].Health.Value);
            Assert.AreEqual(finalHealth, characters[3].Health.Value);
            Assert.AreEqual(finalHealth, characters[4].Health.Value);
            Assert.AreEqual(valueNotRestored, characters[5].Health.Value);
        }

        [Test]
        public void Test_Heal_5_People_With_Caster_Inthe_Middle()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(6);
            var caster = characters[0];
            var team = new Team(0);
            var healSpell = new HealReboundSpell(caster);
            var valueToReduce = 100;
            var valueNotRestored = characters[1].Health.Value - valueToReduce;
            var finalHealth = valueNotRestored + healSpell.HealAmount;

            //Act
            team.AddMembers(caster);

            for (int i = 0; i < 5; i++)
            {
                var warrior = characters[i+1];
                team.AddMembers(warrior);

                warrior.Health.Reduce(valueToReduce);
                warrior.View.transform.position = Vector3.right * Fact(5 - i);
            }

            caster.Health.Reduce(valueToReduce);
            caster.View.transform.position = Vector3.right * 50;

            healSpell.SelectTarget(characters[1]);
            healSpell.Cast();

            //Assert
            Assert.AreEqual(finalHealth, characters[1].Health.Value);
            Assert.AreEqual(finalHealth, characters[2].Health.Value);
            Assert.AreEqual(finalHealth, characters[3].Health.Value);
            Assert.AreEqual(valueNotRestored, characters[4].Health.Value);
            Assert.AreEqual(valueNotRestored, characters[5].Health.Value);
            Assert.AreEqual(finalHealth, caster.Health.Value);
        }

        [Test]
        public void Test_Heal_6_People_With_Enemies_Inthe_Middle()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(7);
            var caster = characters[0];
            var team = new Team(0);
            var enemyTeam = new Team(1);
            var healSpell = new HealReboundSpell(caster);
            var valueToReduce = 100;
            var valueNotRestored = characters[1].Health.Value - valueToReduce;
            var finalHealth = valueNotRestored + healSpell.HealAmount;

            //Act
            team.AddMembers(caster);
            team.AddEnemies(enemyTeam);

            for (int i = 0; i < 6; i++)
            {
                var warrior = characters[i+1];

                if (i % 2 == 0) team.AddMembers(warrior);
                else enemyTeam.AddMembers(warrior);

                warrior.Health.Reduce(valueToReduce);
                warrior.View.transform.position = Vector3.right * Fact(6 - i);
            }

            caster.View.transform.position = Vector3.left * 100;
            healSpell.SelectTarget(characters[1]);
            healSpell.Cast();

            //Assert
            Assert.AreEqual(finalHealth, characters[1].Health.Value);
            Assert.AreEqual(valueNotRestored, characters[2].Health.Value);
            Assert.AreEqual(finalHealth + healSpell.HealAmount, characters[3].Health.Value);
            Assert.AreEqual(valueNotRestored, characters[4].Health.Value);
            Assert.AreEqual(finalHealth, characters[5].Health.Value);
            Assert.AreEqual(valueNotRestored, characters[6].Health.Value);
        }

        int Fact(int number)
        {
            return number == 0 ? 1 : number * Fact(number - 1);
        }
    }
}
