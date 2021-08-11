using Game.Characters.Stats;
using Game.Characters.Stats.Commons;
using Game.Characters.Stats.Utils;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class CharacterStatsTest
    {
        [Test]
        public void All_Stats_Created_1_HasValue()
        {
            var damageStat = new Stat(new StatCalculator(), EStat.Damage, 10f);
            Assert.AreEqual(10, damageStat.Value);
        }

        [Test]
        public void Test_Stat_With_Max_Value()
        {
            //Arrange
            var stats = new StatCollection
            (
                new Stat(new StatCalculator(), EStat.Damage, 10, 5),
                new Stat(new StatCalculator(), EStat.AbilityPower, 10, 100)
            );

            var flatAPModifier = new StatModifier(EBonusType.Flat, 200);
            var modifiersDictionary = new Dictionary<EStat, IReadOnlyCollection<StatModifier>>();
            var modifierProvider = Substitute.For<IStatModifierProvider>();

            //Act
            modifiersDictionary.Add(EStat.AbilityPower, new List<StatModifier>() { flatAPModifier });
            modifierProvider.GetModifiers().Returns(modifiersDictionary);
            stats.ApplyModifiers(modifierProvider);

            //Assert
            Assert.AreEqual(5, stats.GetValue(EStat.Damage));
            Assert.AreEqual(100, stats.GetValue(EStat.AbilityPower));
        }

        [Test]
        public void Base_Stat_Isnt_Affected_By_Modifiers()
        {
            //Arrange
            var baseAP = new Stat(new StatCalculator(), EStat.AbilityPower, 10, 500);
            var stats = new StatCollection(new Stat(new StatCalculator(), EStat.AbilityPower, 10, 500));
            var flatAPModifier = new StatModifier(EBonusType.Flat, 290);
            var modifiersDictionary = new Dictionary<EStat, IReadOnlyCollection<StatModifier>>();
            var modifierProvider = Substitute.For<IStatModifierProvider>();

            //Act
            modifiersDictionary.Add(EStat.AbilityPower, new List<StatModifier>() { flatAPModifier });
            modifierProvider.GetModifiers().Returns(modifiersDictionary);
            stats.ApplyModifiers(modifierProvider);

            //Assert
            Assert.AreEqual(10, baseAP.Value); //Base stat not affected by modifier
            Assert.AreEqual(300, stats.GetValue(EStat.AbilityPower)); //AP inside stats class should be changed
        }

        [Test]
        public void Equip_Two_Sources_Unequip_One()
        {
            //Arrange
            var stats = new StatCollection(new Stat(new StatCalculator(), EStat.AbilityPower, 10, 1000));

            var modifierProvider1 = Substitute.For<IStatModifierProvider>();
            var modifierProvider2 = Substitute.For<IStatModifierProvider>();

            //Act
            stats.ApplyModifiers(modifierProvider1, modifierProvider2);
            stats.RemoveModifiers(modifierProvider1);

            //Assert
            Assert.AreEqual(true, stats.IsAffectedBy(modifierProvider2));
            Assert.AreEqual(false, stats.IsAffectedBy(modifierProvider1));
        }

        [Test]
        public void Equip_None_Sources()
        {
            //Arrange
            var modifierProvider = Substitute.For<IStatModifierProvider>();
            var stats = new StatCollection();

            //Act
            stats.ApplyModifiers();

            //Assert
            Assert.AreEqual(false, stats.IsAffectedBy(modifierProvider));
        }

        [Test]
        public void Equp_Same_Source_Several_Times()
        {
            //Arrange
            var modifierProvider = Substitute.For<IStatModifierProvider>();
            var stats = new StatCollection();

            //Act
            stats.ApplyModifiers(modifierProvider, modifierProvider, modifierProvider);
            stats.ApplyModifiers(modifierProvider, modifierProvider, modifierProvider);
            stats.ApplyModifiers(modifierProvider, modifierProvider, modifierProvider);

            //Assert
            Assert.AreEqual(1, stats.Sources.Count);
        }

        [Test]
        public void Unequip_Same_Source_Several_Times()
        {
            //Arrange
            var modifierProvider = Substitute.For<IStatModifierProvider>();
            var stats = new StatCollection();

            //Act
            stats.ApplyModifiers(modifierProvider, modifierProvider, modifierProvider);
            stats.RemoveModifiers(modifierProvider, modifierProvider, modifierProvider);
            stats.RemoveModifiers(modifierProvider, modifierProvider, modifierProvider);

            //Assert
            Assert.AreEqual(0, stats.Sources.Count);
        }

        [Test]
        public void Change_Base_Value()
        {
            //Arrange
            var stats = new StatCollection(new Stat(new StatCalculator(), EStat.AbilityPower, 10, 1000));

            //Act
            stats.SetValues(new Stat(new StatCalculator(), EStat.AbilityPower, 200, 2000));
            stats.SetValues(new Stat(new StatCalculator(), EStat.AbilityPower, 50, 2000));
            stats.SetValues(new Stat(new StatCalculator(), EStat.AbilityPower, 500, 2000));

            //Assert
            Assert.AreEqual(500, stats.GetValue(EStat.AbilityPower));
        }
    }
}
