using Game.Characters;
using Game.Characters.CharacterStats;
using Game.Characters.Teams;
using NUnit.Framework;

namespace Tests
{
    public class TeamTest
    {
        [Test]
        public void Test_Naming()
        {
            var team = new Team(1);
            team.SetName("Yes");
            team.SetName("No");

            var team2 = new Team(2, "Sure");

            Assert.AreEqual(team.Name, "No");
            Assert.AreEqual(team2.Name, "Sure");
        }

        [Test]
        public void Test_Characters()
        {
            var character1 = new Mage(new Stats());
            var character2 = new Mage(new Stats());
            var character3 = new Mage(new Stats());

            var team = new Team(1);

            team.AddMembers(character1, character2);
            team.RemoveMembers(character3);

            Assert.AreEqual(team.Members.Count, 2);
        }
    }
}
