using Game.Characters.Stats;
using Game.Characters.Stats.Commons;
using Game.Characters.Stats.Complex;
using Game.Characters.Stats.Utils;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class CharacterHealthTest
    {
        [Test]
        //Trying to clarify test's meaning with better naming
        public void Was_100_Of_200_HP_Modifier_Removed_Should_Be_80_Of_80()
        {
            //Arrange
            var healthStat = new Stat(new StatCalculator(), EStat.Health, 80, 200);
            var stats = new Stats(healthStat);
            var characterHealth = new Health(stats);
            var flatAPModifier = new StatModifier(EBonusType.Flat, 300);
            var modifiersDictionary = new Dictionary<EStat, IReadOnlyCollection<StatModifier>>();
            var modifierProvider = Substitute.For<IStatModifierProvider>();

            //Act
            modifiersDictionary.Add(EStat.Health, new List<StatModifier>() { flatAPModifier });
            modifierProvider.GetModifiers().Returns(modifiersDictionary);
            stats.ApplyModifiers(modifierProvider);

            //Assert
            Assert.AreEqual(200, characterHealth.Value);
            Assert.AreEqual(200, characterHealth.MaxValue);

            characterHealth.Reduce(100);

            Assert.AreEqual(100, characterHealth.Value);
            Assert.AreEqual(200, characterHealth.MaxValue);

            stats.RemoveModifiers(modifierProvider);

            Assert.AreEqual(80, characterHealth.Value);
            Assert.AreEqual(80, characterHealth.MaxValue);
        }
    }
}
