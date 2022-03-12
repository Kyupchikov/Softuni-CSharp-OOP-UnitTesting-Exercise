namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private static long id = 4002201001;
        private static string userName = "Pesho";
        private const int maxCount = 16;
        Person person = new Person(id, userName);

        private Database database;

        [SetUp]
        public void InitializeCollection()
        {
            this.database = new Database();
        }

        [Test]
        public void IsIdCorrect()
        {
            Assert.AreEqual(person.Id, id);
        }

        [Test]
        public void IsUserNameCorrect()
        {
            Assert.AreEqual(person.UserName, userName);
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
        public void DataShouldAddPersonCorrectlyWhileBelowOfMaximumCount(int newCollectionCount)
        {
            int databaseStartingCount = database.Count;

            for (int j = 0; j < newCollectionCount; j++)
            {
                database.Add(new Person(id + j + 1, "person #" + (j + 1)));
            }

            Assert.LessOrEqual(newCollectionCount + databaseStartingCount, maxCount);
            Assert.AreEqual(database.Count, newCollectionCount);
        }


        [TestCase(17)]
        [TestCase(50)]
        public void DataShouldThrowExceptionIfAddingPersonOverOfMaximumCount(int newCollectionCount)
        {
            for (int j = 0; j < newCollectionCount; j++)
            {
                if (j == maxCount)
                {
                    Assert.Throws<InvalidOperationException>(() => database.Add(new Person(id + j + 1, "person #" + (j + 1)))
                    , "Array's capacity must be exactly 16 integers!");
                    break;
                }
                else
                {
                    database.Add(new Person(id + j + 1, "person #" + (j + 1)));
                }
            }
        }

        [Test]
        public void CheckTheBorderOfTheCollection()
        {
            for (int j = 1; j < 17; j++)
            {
                database.Add(new Person(id + j + 1, "person #" + (j + 1)));
            }

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(id + 20 + 1, "person #" + (20 + 1)))
            , "Provided data length should be in range [0..16]!");
        }

        [Test]
        public void EachPersonShouldBeWithUniqueName()
        {
            for (int j = 1; j < 10; j++)
            {
                database.Add(new Person(id + j + 1, "person #" + (j + 1)));
            }

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(id + 20 + 1, "person #" + 5))
            , "There is already user with this username!");
        }

        [Test]
        public void EachPersonShouldBeWithUniqueId()
        {
            for (int j = 1; j < 10; j++)
            {
                database.Add(new Person(id + j + 1, "person #" + (j + 1)));
            }

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(id + 5, "person #" + (20 + 1)))
            , "There is already user with this Id!");
        }

        [Test]
        public void RemovePersonFromEmptyCollection()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove(), "Collection is empty!");
        }

        [Test]
        public void RemovePersonFromCollectionProperly()
        {
            database.Add(person);
            database.Remove();

            Assert.AreEqual(0, database.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void DataShouldReturnExceptionForInvalidGivenName(string name)
        {
            database.Add(person);

            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(name), "Username parameter is null!");
        }

        [TestCase("Tosho")]
        [TestCase("Gosho")]
        [TestCase("Ivan")]
        public void DataShouldReturnExceptionForNonExistingGivenName(string name)
        {
            database.Add(person);

            Assert.Throws<InvalidOperationException>(() => database.FindByUsername(name), "No user is present by this username!");
        }

        [Test]
        public void DataShouldReturnPersonByGivenNameIfHeExist()
        {
            database.Add(person);
            var result = database.FindByUsername("Pesho");

            Assert.AreEqual(person, result);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void DataShouldReturnExceptionForInvalidGivenId(int id)
        {
            database.Add(person);

            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(id), "Id should be a positive number!");
        }

        [TestCase(0)]
        [TestCase(100_000)]
        [TestCase(100_000_000)]
        public void DataShouldReturnExceptionForNonExistingGivenId(int id)
        {
            database.Add(person);

            Assert.Throws<InvalidOperationException>(() => database.FindById(id), "No user is present by this ID!");
        }

        [Test]
        public void DataShouldReturnPersonByGivenIdIfHeExist()
        {
            database.Add(person);
            var result = database.FindById(id);

            Assert.AreEqual(person, result);
        }

        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        public void CounterCheck(int count)
        {
            for (int j = 0; j < count; j++)
            {
                database.Add(new Person(id + j + 1, "person #" + (j + 1)));
            }

            Assert.AreEqual(count, database.Count);
        }

        [TestCase(17)]
        public void ConstructorShouldThrowanExceptionIfAddRangeIsLongerThenMaxLength(int count)
        {
            Person[] people = new Person[count];

            for (int j = 0; j < count; j++)
            {
                if (j == maxCount)
                {
                    Assert.Throws<InvalidOperationException>(() => database.Add(new Person(id + j + 1, "person #" + (j + 1)))
                    , "Provided data length should be in range [0..16]!");
                    break;
                }
                else
                {
                    database.Add(new Person(id + j + 1, "person #" + (j + 1)));
                }
            }
        }

        [Test]
        public void AddOperationThrowsExeptionWhenAddingPersonWithIdThatAlreadyIsAssignToExistingPersonInDatabase()
        {
            Person[] people;
            people = new Person[]
            { new Person(0,"Pesho"),
                      new Person (1,"Misho"),
                      new Person (2,"Gosho"),
                      new Person (3,"Mimi"),
                      new Person (4,"Rosana"),
                      new Person(5,"Peshito"),
                      new Person (6,"Mishto"),
                      new Person (7,"Goshko"),
                      new Person (8,"Mimito"),
                      new Person (9, "Roxana"),
                      new Person(10,"Pepi"),
                      new Person (11,"Mishko"),
                      new Person (12,"Gosheto"),
                      new Person (13,"Mitko"),
                      new Person (14, "Roximira"),
                      new Person (15, "Nikolina"),
            };

            database = new Database(people);

            Person newPerson = new Person(10, "Miteto");
            database.Remove();

            Assert.That(() => database.Add(newPerson), Throws.InvalidOperationException);
        }

        [Test]
        public void ConstructorThrowsExeptionIfPeopleAreNotExactly16()
        {
            Person[] people;
            people = new Person[]
           { new Person(12478,"Pesho"),
                      new Person (32092,"Misho"),
                      new Person (43589,"Gosho"),
                      new Person (49109,"Mimi"),
                      new Person (9820989,"Rosana"),
                      new Person(12345,"Peshito"),
                      new Person (32098,"Mishto"),
                      new Person (43356,"Goshko"),
                      new Person (492098,"Mimito"),
                      new Person (9836749, "Roxana"),
                      new Person(123490,"Pepi"),
                      new Person (32078,"Mishko"),
                      new Person (433590,"Gosheto"),
                      new Person (492678,"Mitko"),
                      new Person (9836745, "Roximira"),
                      new Person (8963790, "Nikolina"),
                      new Person (432516, "Maxi")
           };

            Assert.That(() => new Database(people), Throws.ArgumentException);
        }
    }
}
