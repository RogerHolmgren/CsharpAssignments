using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArenaFighter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter.Tests
{
    [TestClass()]
    public class ArenaFighterTests
    {
        [TestMethod()]
        public void CreatePlayer()
        {
            string name = "Hero";
            Character character = Character.GetPlayerCharacter(name);
            Assert.IsTrue(character.name.Equals(name));
            Assert.IsTrue(character.isPlayer);
        }

        [TestMethod()]
        public void CreateNamelessPlayer()
        {
            string name = "";
            Character character = Character.GetPlayerCharacter(name);
            Assert.IsTrue(character.name.Equals("Nameless"));
        }

        [TestMethod()]
        public void CreateEnemy()
        {
            Character character = Character.GetEnemyCharacter(1);
            Assert.IsFalse(character.isPlayer);
        }
        [TestMethod()]
        public void PlayerTakeDamage()
        {
            Character character = Character.GetPlayerCharacter("");
            character.maxHealth = character.currentHealth = 10;
            character.TakeDamage(5);
            Assert.AreEqual(character.currentHealth, 5);
            character.TakeDamage(5);
            Assert.AreEqual(character.currentHealth, 0);
            character.TakeDamage(5);
            Assert.AreEqual(character.currentHealth, 0);
        }


    }
}