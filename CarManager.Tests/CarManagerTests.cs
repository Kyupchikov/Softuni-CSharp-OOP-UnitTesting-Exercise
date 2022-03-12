namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private static string make = "Honda";
        private static string model = "CR-V";
        private static double fuelConsumption = 13;
        private static double fuelCapacity = 70;

        Car car = new Car("Honda", "CR-V", 13, 70);

        [Test]
        public void InitializeObjectAndGetters()
        {
            Assert.AreEqual(make,car.Make);
            Assert.AreEqual(model,car.Model);
            Assert.AreEqual(fuelConsumption,car.FuelConsumption);
            Assert.AreEqual(fuelCapacity,car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void MakeSetterCheck(string make)
        {
            Car car;
            Assert.Throws<ArgumentException>(() => car = new Car(make, model, fuelConsumption, fuelCapacity), "Make cannot be null or empty!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ModelSetterCheck(string model)
        {
            Car car;
            Assert.Throws<ArgumentException>(() => car = new Car(make, model, fuelConsumption, fuelCapacity), "Model cannot be null or empty!");
        }

        [TestCase("Honda", "CR-V", 0,70)]
        [TestCase("Honda","CR-V",-1,70)]
        [TestCase("Honda","CR-V",-10,70)]
        public void FuelConsumptionSetterCheck(string make, string model, double fuelConsumption, double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity), "Fuel consumption cannot be zero or negative!");
        }

        [TestCase("Honda", "CR-V", 13, 0)]
        [TestCase("Honda", "CR-V", 13, -1)]
        [TestCase("Honda", "CR-V", 13, -50)]
        public void FuelCapacitySetterCheck(string make, string model, double fuelConsumption, double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity), "Fuel capacity cannot be zero or negative!");
        }

        //[TestCase("Honda", "CR-V", 13,70)]
        //[TestCase("Honda", "CR-V", 13, 70)]
        //[TestCase("Honda", "CR-V", 13, 70)]
        //public void FuelAmountSetterCheck(string make, string model, double fuelConsumption, double fuelCapacity)
        //{
        //    Car car = new Car(make, model, fuelConsumption, fuelCapacity);
        //    car.FuelAmount =
        //    Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity), "Fuel capacity cannot be zero or negative!");
        //}

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void RefuelShouldReturnExceptionIfFuelToRefuelIsZeroOrLess(int fuelToRefill)
        {
            Assert.Throws<ArgumentException>(() => car.Refuel(fuelToRefill), "Fuel amount cannot be zero or negative!");
        }

        [TestCase(71)]
        [TestCase(717)]
        public void IfFuellToRefillPlusFuellAmountIsOverThenFuellCapacityThenFuellAmountShouldBeEqualToFuellCapacity(int fuellToRefill)
        {
            Car car = new Car("Honda", "CR-V", 13, 70);
            car.Refuel(fuellToRefill);
            Assert.AreEqual(car.FuelCapacity, car.FuelAmount);
        }

        [TestCase(501)]
        [TestCase(500.1)]
        [TestCase(1000)]
        public void IfNeedFuelToDriveIsMoreThanFuelAmountShouldThrowException(double distance)
        {
            Car newCar = new Car("Honda", "CR-V", 1, 50);
            newCar.Refuel(5);
           
            Assert.Throws<InvalidOperationException>(() => newCar.Drive(distance));
            
        }

        [TestCase(499)]
        [TestCase(250)]
        [TestCase(0)]
        public void IfDriveTheCarFuelShouldDecrease(double distance)
        {
            Car newCar = new Car("Honda", "CR-V", 1, 50);
            newCar.Refuel(5);

            double fuelNeeded = (distance / 100) * newCar.FuelConsumption;
            double fuelLeft = newCar.FuelAmount - fuelNeeded;

            newCar.Drive(distance);

            Assert.AreEqual(fuelLeft, newCar.FuelAmount);
        }

        //[Test]
        //public void FuelAmount_ThrowsException_WhenNegativeValueIspassed()
        //{
        //    this.car.Refuel(this.car.FuelCapacity);
        //    double beforeDrive = this.car.FuelAmount;
        //    this.car.Drive(100);
        //    double afterDrive = this.car.FuelAmount;
        //    Assert.That(afterDrive, Is.LessThan(beforeDrive));
        //}
    }
}