using System;
using System.Collections.Generic;
using System.Linq;
using wt_lab2_2oop_glebko.Interfaces;
using wt_lab2_2oop_glebko.Models;

namespace wt_lab2_2oop_glebko.Services
{
    public class FlightService : IItemService<Flight>
    {
        private readonly List<Flight> _flights = new();

        // Реализация методов интерфейса
        public void Add(Flight item) => _flights.Add(item);

        public Flight GetById(int id) =>
            _flights.FirstOrDefault(f => f.Id == id);

        public IEnumerable<Flight> GetAll() => _flights;

        // === ПЕРЕГРУЗКА МЕТОДОВ (method overloading) ===
        // Два метода с одним именем, но разными параметрами
        public void PrintAll()
        {
            foreach (var f in _flights)
                f.PrintInfo();
        }

        public void PrintAll(string header)
        {
            Console.WriteLine($"--- {header} ---");
            PrintAll();
        }
    }
}