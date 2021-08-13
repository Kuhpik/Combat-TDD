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
        public void Damage_Stat_Created_With_Value_10_Check_Value_Must_Return_10()
        {
            var damageStat = new Stat(new StatCalculator(), EStat.Damage, 10f);
            Assert.AreEqual(10, damageStat.Value);
        }

        [Test]
        public void Damage_Stat_Created_With_Value_10_But_Max_Value_5_Check_Value_Must_Return_5()
        {
            //Arrange
            var stats = new StatCollection
            (
                new Stat(new StatCalculator(), EStat.Damage, 10, 5)
            );

            //Assert
            Assert.AreEqual(5, stats.GetValue(EStat.Damage));
        }

        [Test]
        public void Damage_Stat_Created_With_Max_Value_100_Flat_Modifier_Applied_With_Value_200_Check_Value_Must_Return_100()
        {
            //Arrange
            var stats = new StatCollection
            (
                new Stat(new StatCalculator(), EStat.Damage, 10, 100)
            );

            var flatDamageModifier = new StatModifier(EBonusType.Flat, 200);
            var modifiersDictionary = new Dictionary<EStat, IReadOnlyCollection<StatModifier>>();
            var modifierProvider = Substitute.For<IStatModifierProvider>();

            //Act
            modifiersDictionary.Add(EStat.Damage, new List<StatModifier>() { flatDamageModifier });
            modifierProvider.GetModifiers().Returns(modifiersDictionary);
            stats.ApplyModifiers(modifierProvider);

            //Assert
            Assert.AreEqual(100, stats.GetValue(EStat.Damage));
        }

        [Test]
        public void Use_Stat_As_A_Value_Provider_Apply_Modifiers_To_New_Stats_First_One_Must_Not_Be_Affected_By_Modifier()
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
        public void Apply_Two_Sources_Remove_One_Only_One_Sourse_Should_Affect_Stats()
        {
            //Arrange
            var stats = new StatCollection(new Stat(new StatCalculator(), EStat.AbilityPower, 10, 1000));

            var modifierProvider1 = Substitute.For<IStatModifierProvider>();
            var modifierProvider2 = Substitute.For<IStatModifierProvider>();

            //Act
            stats.ApplyModifiers(modifierProvider1, modifierProvider2);
            stats.RemoveModifiers(modifierProvider1);

            //Assert
            Assert.AreEqual(1, stats.Sources.Count);
            Assert.AreEqual(true, stats.IsAffectedBy(modifierProvider2));
            Assert.AreEqual(false, stats.IsAffectedBy(modifierProvider1));
        }

        [Test]
        public void Apply_Zero_Sources_None_Sources_Should_Affect_Stats()
        {
            //Arrange
            var modifierProvider = Substitute.For<IStatModifierProvider>();
            var stats = new StatCollection();

            //Act
            stats.ApplyModifiers();

            //Assert
            Assert.AreEqual(0, stats.Sources.Count);
            Assert.AreEqual(false, stats.IsAffectedBy(modifierProvider));
        }

        [Test]
        public void Apply_Same_Source_Several_Times_Only_One_Souce_Should_Affect_Stats()
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
        public void Remove_Same_Source_Several_Time_Zero_Sources_Should_Affect_Stats()
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
        public void Change_Stat_Base_Values_Several_Times_Stat_Values_Should_Be_Equal_To_Last_Ones_Set()
        {
            //Arrange
            var stats = new StatCollection(new Stat(new StatCalculator(), EStat.AbilityPower, 10, 1000));

            //Act
            stats.SetValues(new Stat(new StatCalculator(), EStat.AbilityPower, 200, 2000));
            stats.SetValues(new Stat(new StatCalculator(), EStat.AbilityPower, 50, 2000));
            stats.SetValues(new Stat(new StatCalculator(), EStat.AbilityPower, 500, 8000));

            //Assert
            Assert.AreEqual(500, stats.GetStat(EStat.AbilityPower).BaseValue);
            Assert.AreEqual(8000, stats.GetStat(EStat.AbilityPower).MaxValue);
        }
    }
}
