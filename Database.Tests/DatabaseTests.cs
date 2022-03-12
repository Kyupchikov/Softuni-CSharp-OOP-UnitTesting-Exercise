namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [SetUp]
        public void InitializeCollection()
        {
            this.database = new Database();
        }

        [Test]
        public void ConstructorShouldInitializeCollection()
        {
            Assert.IsNotNull(database);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(16)]
        public void DataShouldAddNumbersCorrectlyWhileBelowOfSixteenCount(int num)
        {
            for (int i = 0; i < num; i++)
            {
                database.Add(i);
            }
            Assert.AreEqual(database.Count, num);
        }

        [Test]
        public void VerifyingCounterIncreasing()
        {
            int result;

            for (int j = 0; j < 30; j++)
            {
                Random random = new Random();
                result = random.Next(0, 17);

                for (int i = 0; i < result; i++)
                {
                    database.Add(i);
                }

                Assert.AreEqual(result, database.Count);
                database = new Database();
            }
        }
        
        [Test]
        public void CheckTheBorderOfTheCollection()
        {
            for (int i = 0; i < 16; i++)
            {
                database.Add(i);
            }
            Assert.Throws<InvalidOperationException>(() => database.Add(1), "Array's capacity must be exactly 16 integers!") ;
        }

        [Test]
        public void CollectionShouldThrowAnExceptionIfContainsZeroElementsAndTryToRemoveMore()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove(), "The collection is empty!");
        }

        [Test]
        public void VerifyingCounterDecreasing()
        {
            int result;

            for (int j = 0; j < 30; j++)
            {
                Random random = new Random();
                result = random.Next(0, 17);

                for (int i = 0; i < result; i++)
                {
                    database.Add(i);
                }

                for (int i = result - 1; i >= 0; i--)
                {
                    database.Remove();
                }

                Assert.AreEqual(0, database.Count);
                database = new Database();
            }
        }

        [Test]
        public void FetchShouldReturnSameCollection()
        {
            for (int i = 0; i < 10; i++)
            {
                database.Add(i);
            }

            int[] temp = new int[database.Count];
            temp = database.Fetch();

            database.Remove();
            int[] temp2 = database.Fetch();

            CollectionAssert.AreNotEqual(temp,temp2 );
        }
    }
}
