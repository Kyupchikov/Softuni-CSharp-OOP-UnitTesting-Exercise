namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System.Linq;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private const int MIN_ATTACK_HP = 30;

        [TestCase("Pesho", 50, 50)]
        public void ConstructorShouldInitializeTheObject(string name, int damage, int hp)
        {
            Warrior warrior = new Warrior(name, damage, hp);
            Assert.IsNotNull(warrior);
        }

        [TestCase("Pesho", 50, 50)]
        public void GettersCheck(string name, int damage, int hp)
        {
            Warrior warrior = new Warrior(name, damage, hp);
            Assert.AreEqual(name, warrior.Name);
            Assert.AreEqual(damage, warrior.Damage);
            Assert.AreEqual(hp, warrior.HP);
        }

        [TestCase("Pesho", 50, -1)]
        [TestCase("Pesho", 50, -10)]
        public void IfWarriorHpIsLessThenZeroShouldThrowException(string name, int damage, int hp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, hp), "HP should not be negative!");
        }

        [TestCase("Pesho", 0, 50)]
        [TestCase("Pesho", -1, 50)]
        [TestCase("Pesho", -50, 50)]
        public void IfWarriorDamageIsLessOrEqualToZeroShouldThrowException(string name, int damage, int hp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, hp), "HP should not be negative!");
        }

        [TestCase(" ", 50, 50)]
        [TestCase(null, 50, 50)]
        public void IfWarriorNameIsNullOrWhiteSpaceShouldThrowException(string name, int damage, int hp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, hp), "HP should not be negative!");
        }

        [TestCase("Pesho", 50, 29, "Ivan", 50, 50)]
        [TestCase("Pesho", 50, 30, "Ivan", 50, 50)]
        public void CheckFirstWarriorHpInAttack(string firstWarriorName, int firstWarriorDamage, int firstWarriorHp
            , string secondWarriorName, int secondWarriorDamage, int secondWarriorHp)
        {
            Warrior warrior1 = new Warrior(firstWarriorName, firstWarriorDamage, firstWarriorHp);
            Warrior warrior2 = new Warrior(secondWarriorName, secondWarriorDamage, secondWarriorHp);
            Assert.Throws<InvalidOperationException>(() => warrior1.Attack(warrior2));
        }

        [TestCase("Pesho", 50, 50, "Ivan", 50, 29)]
        [TestCase("Pesho", 50, 50, "Ivan", 50, 30)]
        public void CheckSecondWarriorHpInAttack(string firstWarriorName, int firstWarriorDamage, int firstWarriorHp
            , string secondWarriorName, int secondWarriorDamage, int secondWarriorHp)
        {
            Warrior warrior1 = new Warrior(firstWarriorName, firstWarriorDamage, firstWarriorHp);
            Warrior warrior2 = new Warrior(secondWarriorName, secondWarriorDamage, secondWarriorHp);
            Assert.Throws<InvalidOperationException>(() => warrior1.Attack(warrior2));
        }

        [TestCase("Pesho", 50, 45, "Ivan", 50, 31)]
        [TestCase("Pesho", 50, 49, "Ivan", 50, 31)]
        public void CheckFirstWarriorHpInDefence(string firstWarriorName, int firstWarriorDamage, int firstWarriorHp
           , string secondWarriorName, int secondWarriorDamage, int secondWarriorHp)
        {
            Warrior warrior1 = new Warrior(firstWarriorName, firstWarriorDamage, firstWarriorHp);
            Warrior warrior2 = new Warrior(secondWarriorName, secondWarriorDamage, secondWarriorHp);
            Assert.Throws<InvalidOperationException>(() => warrior1.Attack(warrior2));
        }

        [TestCase("Pesho", 51, 100, "Ivan", 50, 31)]
        public void CheckSecondWarriorHpInDefence(string firstWarriorName, int firstWarriorDamage, int firstWarriorHp
           , string secondWarriorName, int secondWarriorDamage, int secondWarriorHp)
        {
            Warrior warrior1 = new Warrior(firstWarriorName, firstWarriorDamage, firstWarriorHp);
            Warrior warrior2 = new Warrior(secondWarriorName, secondWarriorDamage, secondWarriorHp);
            warrior1.Attack(warrior2);
            Assert.AreEqual(0, warrior2.HP);
        }

        [TestCase("Pesho", 20, 80, "Ivan", 99, 80)]
        public void CheckFirstWarriorDefence(string firstWarriorName, int firstWarriorDamage, int firstWarriorHp
           , string secondWarriorName, int secondWarriorDamage, int secondWarriorHp)
        {
            Warrior warrior1 = new Warrior(firstWarriorName, firstWarriorDamage, firstWarriorHp);
            Warrior warrior2 = new Warrior(secondWarriorName, secondWarriorDamage, secondWarriorHp);
            warrior2.Attack(warrior1);
            Assert.AreEqual(0, warrior1.HP);
        }

        [TestCase("Pesho", 20, 80, "Ivan", 49, 80)]
        public void CheckSomethingDefence(string firstWarriorName, int firstWarriorDamage, int firstWarriorHp
           , string secondWarriorName, int secondWarriorDamage, int secondWarriorHp)
        {
            Warrior warrior1 = new Warrior(firstWarriorName, firstWarriorDamage, firstWarriorHp);
            Warrior warrior2 = new Warrior(secondWarriorName, secondWarriorDamage, secondWarriorHp);
            warrior1.Attack(warrior2);
            Assert.AreEqual(60, warrior2.HP);
        }

        [TestCase("Pesho", 20, 80, "Ivan", 99, 80)]
        public void CheckArenaWarriorDefence(string firstWarriorName, int firstWarriorDamage, int firstWarriorHp
          , string secondWarriorName, int secondWarriorDamage, int secondWarriorHp)
        {
            Warrior warrior1 = new Warrior(firstWarriorName, firstWarriorDamage, firstWarriorHp);
            Warrior warrior2 = new Warrior(secondWarriorName, secondWarriorDamage, secondWarriorHp);
            Arena arena = new Arena();
            arena.Enroll(warrior1);
            arena.Enroll(warrior2);
            arena.Fight( secondWarriorName, firstWarriorName);
            Assert.AreEqual(0, warrior1.HP);
        }
    }
}
