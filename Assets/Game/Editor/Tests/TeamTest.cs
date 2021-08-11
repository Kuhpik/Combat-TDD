using Game.Characters;
using Game.Characters.Stats;
using Game.Characters.Stats.Factories;
using Game.Characters.Stats.Utils;
using Game.Characters.Teams;
using NSubstitute;
using NUnit.Framework;
using Tests.Helpers;

namespace Tests
{
    public class TeamTest
    {
        [Test]
        public void Test_Naming()
        {
            //Arrange
            var team1 = new Team(1);
            var team2 = new Team(2, "Sure");

            //Act
            team1.SetName("Yes");
            team1.SetName("No");

            //Assert
            Assert.AreEqual("No", team1.Name);
            Assert.AreEqual("Sure", team2.Name);
        }

        [Test]
        public void Test_Characters()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(3);
            var team = new Team(1);

            //Act
            team.AddMembers(characters[0], characters[1]);
            team.RemoveMembers(characters[2]);

            //Assert
            Assert.AreEqual(2, team.Members.Count);
        }
    }
}
