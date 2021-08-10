using Game.Characters;
using Game.Characters.Stats;
using Game.Characters.Stats.Factories;
using Game.Characters.Teams;
using NSubstitute;
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
            var statsFactory = Substitute.For<IStatCollectionFactory>();

            var character1 = new Mage(statsFactory);
            var character2 = new Mage(statsFactory);
            var character3 = new Mage(statsFactory);

            var team = new Team(1);

            team.AddMembers(character1, character2);
            team.RemoveMembers(character3);

            Assert.AreEqual(team.Members.Count, 2);
        }
    }
}
