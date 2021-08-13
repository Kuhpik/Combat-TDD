using Game.Characters.Teams;
using NUnit.Framework;
using Tests.Helpers;

namespace Tests
{
    public class TeamTest
    {
        [Test]
        public void One_Team_Created_With_Name_Name_Shouldbe_Same()
        {
            //Arrange
            var team = new Team(1, "Sure");

            //Assert
            Assert.AreEqual("Sure", team.Name);
        }

        [Test]
        public void Team_Name_Changed_Two_Times_Name_Must_Be_Last_One_Set()
        {
            //Arrange
            var team = new Team(1);

            //Act
            team.SetName("Yes");
            team.SetName("No");

            //Assert
            Assert.AreEqual("No", team.Name);
        }

        [Test]
        public void Three_Characters_Added_To_The_Team_One_Character_Removed_Count_Of_Team_Members_Must_Be_Two()
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
