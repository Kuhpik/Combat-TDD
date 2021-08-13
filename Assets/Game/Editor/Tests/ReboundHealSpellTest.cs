using Game.Characters.Teams;
using Game.Spells;
using NUnit.Framework;
using Tests.Helpers;
using UnityEngine;

namespace Tests
{
    public class ReboundHealSpellTest
    {
        [Test]
        public void Heal_Enemy_Character_His_Health_Must_Not_Be_Changed()
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
        public void One_Character_Heals_Himself_Once_Only_One_Heal_Must_Be_Applied()
        {
            //Arrange
            var character = new TestCharactersCreator().CreateCharacter();
            var healSpell = new HealReboundSpell(character);
            var valueToReduce = 100;
            var valueRestored = character.Health.Value - valueToReduce + healSpell.HealAmount;
            var team = new Team(1);

            //Act
            team.AddMembers(character);

            character.Health.Reduce(valueToReduce);
            healSpell.SelectTarget(character);
            healSpell.Cast();

            //Assert
            Assert.AreEqual(valueRestored, character.Health.Value);
        }

        [Test]
        public void Heal_Two_Characters_They_Should_Get_Two_Heals_Each()
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
        public void Heal_Two_Allies_With_Two_Enemies_Between_Them_Enemies_Should_Not_Be_Healed_Allies_Must_Be_Healed_Twice()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(4);
            var healSpell = new HealReboundSpell(characters[0]);
            var valueToReduce = 200;
            var reducedHealth = characters[1].Health.Value - valueToReduce;
            var finalHealth = reducedHealth + healSpell.HealAmount * 2; //Will affect each warrior 2 times.
            var allyTeam = new Team(0);
            var enemyTeam = new Team(1);

            //Act
            allyTeam.AddEnemies(enemyTeam);
            allyTeam.AddMembers(characters[0], characters[1]);
            enemyTeam.AddMembers(characters[2], characters[3]);

            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].Health.Reduce(valueToReduce);
                characters[i].View.transform.position = Vector3.right *  i;
            }

            characters[1].View.transform.position = Vector3.right * 5;
            healSpell.SelectTarget(characters[1]);
            healSpell.Cast();

            //Assert
            Assert.AreEqual(finalHealth, characters[0].Health.Value);
            Assert.AreEqual(finalHealth, characters[1].Health.Value);
            Assert.AreEqual(reducedHealth, characters[2].Health.Value);
            Assert.AreEqual(reducedHealth, characters[3].Health.Value);
        }

        [Test] //Also will work as test for targeting logic
        public void Five_Characters_In_Game_Cast_Heal_Once_Four_Characters_Must_Be_Healed()
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

        int Fact(int number)
        {
            return number == 0 ? 1 : number * Fact(number - 1);
        }
    }
}
