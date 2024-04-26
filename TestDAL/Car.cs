namespace TestDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Car
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        public string VinCode { get; set; }

        public string Model { get; set; }
    }
}
