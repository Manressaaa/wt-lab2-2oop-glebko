using System;

namespace wt_lab2_2oop_glebko.Models
{
    public class Flight : BaseEntity
    {
        public string FlightNumber { get; private set; }
        public string Destination { get; private set; }

        // Инкапсуляция: снаружи читать можно, менять — только через UpdatePrice
        public decimal Price { get; private set; }

        // Конструктор с параметрами — как в уровне 1, но теперь с base(id)
        public Flight(int id, string flightNumber, string destination, decimal price)
            : base(id)
        {
            FlightNumber = flightNumber;
            Destination = destination;
            Price = price;
            LogCreation();   // демонстрация protected-метода
        }

        // Пустой конструктор — нужен для инициализатора объектов
        public Flight() : base(0)
        {
            FlightNumber = "N/A";
            Destination = "N/A";
            Price = 0;
        }

        // Метод с валидацией — инкапсуляция
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Цена должна быть положительной");
            Price = newPrice;
        }

        // override — реализуем абстрактный метод родителя
        public override void PrintInfo()
        {
            Console.WriteLine($"[{GetType().Name}] #{Id}: {FlightNumber} → {Destination}, цена: {Price:C}");
        }
    }
}