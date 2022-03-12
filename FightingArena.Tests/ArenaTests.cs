namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System.Linq;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void ArenaShouldInitializeWarriorsCollection()
        {
            Arena arena = new Arena();

            Assert.IsNotNull(arena);
        }

        [Test]
        public void CollectionLenghtCheck()
        {
            Arena arena = new Arena();
            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void ArenaShouldThrowExceptionIfTryToAddWarriorWithSameName()
        {
            Arena arena = new Arena();

            string name = string.Empty;
            for (int j = 0; j < 10; j++)
            {
                name = "person #" + (j + 1);
                arena.Enroll(new Warrior(name, 50, 50));
            }

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior(name, 50, 50)));
        }

        [TestCase("Pesho", "Tosho")]
        public void arenaShouldThrowExceptionIfTheNameOfAttackWarriorMissing(string attackerName, string defenderName)
        {
            Arena arena = new Arena();

            string name = string.Empty;
            for (int j = 0; j < 10; j++)
            {
                name = "person #" + (j + 1);
                arena.Enroll(new Warrior(name, 50, 50));
            }

            //Warrior attacker = arena.Warriors
            //    .FirstOrDefault(w => w.Name == attackerName);
            Warrior defender = new Warrior(defenderName, 50, 50);
            arena.Enroll(defender);
            Assert.Throws<InvalidOperationException>(() => arena.Fight(attackerName, defenderName));
        }

        [TestCase("Pesho", "Tosho")]
        public void ArenaShouldThrowExceptionIfTheNameOfDefenderWarriorMissing(string attackerName, string defenderName)
        {
            Arena arena = new Arena();

            string name = string.Empty;
            for (int j = 0; j < 10; j++)
            {
                name = "person #" + (j + 1);
                arena.Enroll(new Warrior(name, 50, 50));
            }

            Warrior defender = arena.Warriors
                .FirstOrDefault(w => w.Name == attackerName);
            Warrior attacker = new Warrior(defenderName, 50, 50);
            arena.Enroll(attacker);
            Assert.Throws<InvalidOperationException>(() => arena.Fight(attackerName, defenderName));
        }

        [Test]
        [TestCase("Attacker", "Attacker", "Defender")]
        [TestCase("Defender", "Attacker", "Defender")]
        [TestCase("Warrior", "Attacker", "Defender")]
        public void FightShouldThrowExceptionIfWarriorNamesAreNotValid(string warriorName, string attackerName, string defenderName)
        {
            Arena arena = new Arena();

            Warrior warrior = new Warrior(warriorName, 40, 40);
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() => arena.Fight(attackerName, defenderName));
        }
    }
}
