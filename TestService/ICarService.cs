using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TestDAL;

namespace TestService
{
    [ServiceContract]
    public interface ICarService
    {
        [OperationContract]
        Car GetCar(int id);

        [OperationContract]
        List<Car> GetAllCars();

        [OperationContract]
        void AddCar( string modelName, decimal price, int mileage, string vinCode);

        [OperationContract]
        void UpdateCar(Car car);

        [OperationContract]
        void DeleteCar(int id);
    }
}
