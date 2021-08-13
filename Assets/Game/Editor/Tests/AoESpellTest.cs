using Game.Characters;
using Game.Spells;
using Game.Spells.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using Tests.Helpers;
using UnityEngine;

namespace Tests
{
    public class AoESpellTest
    {
        [Test]
        public void One_Spell_Created_With_Damage_50_Second_Spell_Damage_Set_To_100_Values_Must_Be_Same()
        {
            var targetingService = new AreaTargetingService(new CircleSpellArea(2), new List<Character>());
            var spell1 = new AoEBurstDamageSpell(targetingService);
            var spell2 = new AoEBurstDamageSpell(targetingService, 50);

            spell1.SetDamage(100);

            Assert.AreEqual(spell1.Damage, 100);
            Assert.AreEqual(spell2.Damage, 50);
        }

        [Test]
        public void Create_Rect_Area_Spell_Two_Characters_Inside_One_Outside_Only_Inside_Ones_Should_Receive_Damage()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(3);
            var targetingService = new AreaTargetingService(new RectSpellArea(2), characters);
            var spell = new AoEBurstDamageSpell(targetingService, 50);
            var healthFull = characters[0].Health.Value;
            var healthLeft = healthFull - spell.Damage;

            //Act
            targetingService.SetCenterPoint(new Vector3(4, 0, 4));

            characters[0].View.transform.position = new Vector3(3, 0, 5);
            characters[1].View.transform.position = new Vector3(6, 0, 2);
            characters[2].View.transform.position = new Vector3(4, 0, 8);

            spell.Cast();

            //Assert
            Assert.AreEqual(healthLeft, characters[0].Health.Value);
            Assert.AreEqual(healthLeft, characters[1].Health.Value);
            Assert.AreEqual(healthFull, characters[2].Health.Value);
        }

        [Test]
        public void Create_Circle_Area_Spell_Tree_Characters_Inside_Two_Outside_Only_Inside_Ones_Should_Receive_Damage()
        {
            //Arrange
            var characters = new TestCharactersCreator().CreateCharacters(5);
            var targetingService = new AreaTargetingService(new CircleSpellArea(2), characters);
            var spell = new AoEBurstDamageSpell(targetingService, 50);
            var healthFull = characters[0].Health.Value;
            var healthLeft = healthFull - spell.Damage;

            //Act
            targetingService.SetCenterPoint(new Vector3(4, 0, 4));
            var pointOnRadius45Degree = 4 + Mathf.Sqrt(2) / 2 * spell.Area.Size;

            characters[0].View.transform.position = new Vector3(3, 0, 5);
            characters[1].View.transform.position = new Vector3(4, 0, 6);
            characters[2].View.transform.position = new Vector3(6, 0, 2);
            characters[3].View.transform.position = new Vector3(pointOnRadius45Degree, 0, pointOnRadius45Degree);
            characters[4].View.transform.position = new Vector3(pointOnRadius45Degree + 0.1f, 0, pointOnRadius45Degree + 0.1f);

            spell.Cast();

            //Assert
            Assert.AreEqual(healthLeft, characters[0].Health.Value);
            Assert.AreEqual(healthLeft, characters[1].Health.Value);
            Assert.AreEqual(healthFull, characters[2].Health.Value);
            Assert.AreEqual(healthLeft, characters[3].Health.Value);
            Assert.AreEqual(healthFull, characters[4].Health.Value);
        }
    }
}
