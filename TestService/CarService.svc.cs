using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using TestDAL;

namespace TestService
{
    public class CarService : ICarService
    {
        private readonly Model1 _dbContext;

        public CarService()
        {
            _dbContext = new Model1();
        }

        public Car GetCar(int id)
        {
            try
            {
                return _dbContext.Cars.Find(id);
            }
            catch (Exception ex)
            {
                throw new FaultException("Error occurred while retrieving the car.", new FaultCode("ServerError"));
            }
        }

        public List<Car> GetAllCars()
        {
            
            
                return _dbContext.Cars.ToList();
           
            
        }

        public void AddCar( string modelName, decimal price, int mileage, string vinCode)
        {
           
                Car carToAdd = new Car();
                
                carToAdd.Model = modelName;
                carToAdd.Price = price;
                carToAdd.Mileage = mileage;
                carToAdd.VinCode = vinCode;

                _dbContext.Cars.Add(carToAdd);
                _dbContext.SaveChanges();
          
            
        }


        public void UpdateCar(Car car)
        {
            try
            {
                if (car == null)
                    throw new ArgumentNullException(nameof(car), "Car object is null.");

                var existingCar = _dbContext.Cars.Find(car.Id);
                if (existingCar != null)
                {
                    existingCar.Model = car.Model;
                    existingCar.Price = car.Price;
                    existingCar.Mileage = car.Mileage;
                    existingCar.VinCode = car.VinCode;

                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"Car with ID {car.Id} not found.", nameof(car));
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Error occurred while updating the car.", new FaultCode("ServerError"));
            }
        }

        public void DeleteCar(int id)
        {
            try
            {
                var carToDelete = _dbContext.Cars.Find(id);
                if (carToDelete != null)
                {
                    _dbContext.Cars.Remove(carToDelete);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"Car with ID {id} not found.", nameof(id));
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Error occurred while deleting the car.", new FaultCode("ServerError"));
            }
        }
    }
}
