using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using wt_lab2_2oop_glebko.Models;
using wt_lab2_2oop_glebko.Interfaces;
using wt_lab2_2oop_glebko.Services;

namespace wt_lab2_2oop_glebko
{
    class Program
    {
        // ВАЖНО: Main стал async Task, чтобы можно было использовать await
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Лабораторная работа №2. Вариант №8: Авиакомпания");
            Console.WriteLine("Разработчик: Глебко Д.И., группа УИР-1");

            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ОПЕРАЦИЙ ---");
                Console.WriteLine("1. Выполнить Блок 1 (Классы и Объекты)");
                Console.WriteLine("2. Выполнить Блок 2 (Коллекции и LINQ)");
                Console.WriteLine("3. Выполнить Блок 3 (Асинхронность)");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                if (choice == "1") RunBlock1();
                else if (choice == "2") RunBlock2();
                else if (choice == "3") await RunBlock3();   // await!
                else if (choice == "0") break;
                else Console.WriteLine("Неверный ввод, попробуйте еще раз.");
            }
        }

        // ================= БЛОК 1 =================
        static void RunBlock1()
        {
            Console.WriteLine("\n--- ВЫПОЛНЕНИЕ БЛОКА 1 ---");

            Console.WriteLine("\n[Уровень 1] Создание через конструктор:");
            Flight flight1 = new Flight(1, "B2-871", "Минск - Москва", 250.00m);
            Flight flight2 = new Flight(2, "B2-900", "Минск - Дубай", 1200.00m);
            flight1.PrintInfo();
            flight2.PrintInfo();

            Console.WriteLine("\n[Уровень 1] Создание через инициализатор объектов:");
            var pf = new PassengerFlight(20, "B2-999", "Минск - Прага", 700m, 150)
            {
                SeatsCount = 200
            };
            pf.PrintInfo();

            Console.WriteLine("\n[Уровень 2] Инкапсуляция + валидация:");
            var f5 = new Flight(5, "B2-303", "Минск - Казань", 4800m);
            try
            {
                f5.UpdatePrice(-100);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка валидации: " + ex.Message);
            }
            f5.UpdatePrice(5000);
            f5.PrintInfo();

            Console.WriteLine("\n[Уровень 2] Полиморфизм через массив базового типа:");
            BaseEntity[] flights =
            {
                new PassengerFlight(10, "B2-300", "Минск - Казань", 3800m, 180),
                new CargoFlight(11, "B2-400", "Минск - Новосибирск", 9000m, 20.5),
                new CharterFlight(12, "B2-500", "Минск - Сочи", 12000m, "ООО Ромашка")
            };

            foreach (var f in flights)
                f.PrintInfo();

            Console.WriteLine("\n[Уровень 3] Сервис через интерфейсную ссылку:");

            IItemService<Flight> service = new FlightService();

            service.Add(new PassengerFlight(30, "B2-700", "Минск - Ереван", 600m, 150));
            service.Add(new CargoFlight(31, "B2-800", "Минск - Алматы", 8000m, 15.0));
            service.Add(new CharterFlight(32, "B2-900", "Минск - Анталия", 12000m, "ООО Ромашка"));

            Console.WriteLine("\nСодержимое сервиса через GetAll():");
            foreach (var f in service.GetAll())
                f.PrintInfo();

            Console.WriteLine("\nПоиск по Id = 31:");
            var found = service.GetById(31);
            if (found != null)
                found.PrintInfo();
            else
                Console.WriteLine("Не найдено");

            Console.WriteLine("\nПерегрузка методов PrintAll:");
            var fs = (FlightService)service;
            fs.PrintAll();
            fs.PrintAll("Все рейсы сервиса");
        }

        // ================= БЛОК 2 =================
        static void RunBlock2()
        {
            Console.WriteLine("\n--- ВЫПОЛНЕНИЕ БЛОКА 2 ---");

            Console.WriteLine("\n[Уровень 1] Коллекция List<Flight>:");

            List<Flight> flights = new List<Flight>();

            flights.Add(new PassengerFlight(1, "B2-101", "Минск - Москва", 250m, 180));
            flights.Add(new PassengerFlight(2, "B2-102", "Минск - СПб", 320m, 160));
            flights.Add(new CargoFlight(3, "B2-201", "Минск - Казань", 7800m, 20));
            flights.Add(new PassengerFlight(4, "B2-103", "Минск - Сочи", 630m, 200));
            flights.Add(new CargoFlight(5, "B2-202", "Минск - Омск", 8100m, 25));
            flights.Add(new CharterFlight(6, "B2-301", "Минск - Анапа", 15000m, "Турфирма"));
            flights.Add(new PassengerFlight(7, "B2-104", "Минск - Мурманск", 710m, 140));

            Console.WriteLine($"Всего в коллекции: {flights.Count} рейсов");
            Console.WriteLine();

            foreach (var f in flights)
                f.PrintInfo();

            Console.WriteLine("\n[Уровень 2] Словарь Dictionary<int, Flight>:");

            Dictionary<int, Flight> flightDict = new Dictionary<int, Flight>();
            foreach (var f in flights)
                flightDict[f.Id] = f;

            Console.WriteLine($"Словарь создан. Ключей в словаре: {flightDict.Count}");

            Console.WriteLine("\nПоиск Id = 3 (существует):");
            if (flightDict.TryGetValue(3, out Flight found))
                found.PrintInfo();
            else
                Console.WriteLine("Рейс с Id=3 не найден");

            Console.WriteLine("\nПоиск Id = 99 (не существует):");
            if (flightDict.TryGetValue(99, out Flight notFound))
                notFound.PrintInfo();
            else
                Console.WriteLine("Рейс с Id=99 не найден");

            Console.WriteLine("\n[Уровень 3] LINQ (Method Syntax):");

            Console.WriteLine("\n--- Where: рейсы дешевле 1000 ---");
            var cheap = flights.Where(f => f.Price < 1000).ToList();
            foreach (var f in cheap)
                f.PrintInfo();

            Console.WriteLine("\n--- OrderByDescending: сортировка по цене ---");
            var sorted = flights.OrderByDescending(f => f.Price).ToList();
            foreach (var f in sorted)
                f.PrintInfo();

            Console.WriteLine("\n--- Select: только номер рейса и цена ---");
            var projection = flights.Select(f => new { f.FlightNumber, f.Price });
            foreach (var p in projection)
                Console.WriteLine($"{p.FlightNumber} — {p.Price:C}");

            Console.WriteLine("\n--- Агрегатные операции ---");
            Console.WriteLine($"Count:   всего рейсов = {flights.Count}");
            Console.WriteLine($"Sum:     сумма цен = {flights.Sum(f => f.Price):C}");
            Console.WriteLine($"Average: средняя цена = {flights.Average(f => f.Price):C}");
            Console.WriteLine($"Min:     минимальная цена = {flights.Min(f => f.Price):C}");
            Console.WriteLine($"Max:     максимальная цена = {flights.Max(f => f.Price):C}");

            Console.WriteLine("\n--- GroupBy: группировка по типу рейса ---");
            var grouped = flights.GroupBy(f => f.GetType().Name);
            foreach (var g in grouped)
            {
                Console.WriteLine($"{g.Key}: {g.Count()} рейсов, " +
                                  $"средняя цена = {g.Average(f => f.Price):C}, " +
                                  $"сумма = {g.Sum(f => f.Price):C}");
            }
        }

        // ================= БЛОК 3 =================
        static async Task RunBlock3()
        {
            Console.WriteLine("\n--- ВЫПОЛНЕНИЕ БЛОКА 3 ---");

            // === УРОВЕНЬ 1: простой async Task ===
            Console.WriteLine("\n[Уровень 1] Простой async Task с await Task.Delay:");
            await ProcessFlightsAsync();

            // === УРОВЕНЬ 2: async Task<List<T>> + try-catch-finally ===
            Console.WriteLine("\n[Уровень 2] Загрузка списка рейсов (async Task<List<Flight>>):");
            var loaded = await LoadFlightsAsync();
            foreach (var f in loaded)
                f.PrintInfo();

            // === УРОВЕНЬ 3: Task.WhenAll + CancellationToken ===
            Console.WriteLine("\n[Уровень 3] Параллельные задачи через Task.WhenAll:");
            await RunParallelTasksAsync();

            Console.WriteLine("\n[Уровень 3] Отмена задачи через CancellationToken:");
            await RunCancellationDemoAsync();
        }

        // ---------- УРОВЕНЬ 1 ----------
        static async Task ProcessFlightsAsync()
        {
            Console.WriteLine("  Начало обработки рейсов...");
            await Task.Delay(1000);   // имитация долгой работы
            Console.WriteLine("  Обработка завершена (прошло 1 сек)");
        }

        // ---------- УРОВЕНЬ 2 ----------
        static async Task<List<Flight>> LoadFlightsAsync()
        {
            try
            {
                Console.WriteLine("  Загрузка рейсов из внешнего API...");
                await Task.Delay(1500);   // имитация запроса к API

                return new List<Flight>
                {
                    new PassengerFlight(101, "B2-1001", "Минск - Москва", 5500m, 180),
                    new CargoFlight(102, "B2-1002", "Минск - СПб", 8000m, 15),
                    new CharterFlight(103, "B2-1003", "Минск - Сочи", 12000m, "ООО Ромашка")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Ошибка: {ex.Message}");
                return new List<Flight>();
            }
            finally
            {
                Console.WriteLine("  finally: операция загрузки завершена");
            }
        }

        // ---------- УРОВЕНЬ 3: Task.WhenAll ----------
        static async Task RunParallelTasksAsync()
        {
            Console.WriteLine("  Запуск 3 задач параллельно...");

            Task<List<Flight>> task1 = LoadFlightsAsync();
            Task<int> task2 = LoadPassengersCountAsync();
            Task<int> task3 = LoadTicketsCountAsync();

            await Task.WhenAll(task1, task2, task3);

            Console.WriteLine($"  Загружено рейсов: {task1.Result.Count}");
            Console.WriteLine($"  Загружено пассажиров: {task2.Result}");
            Console.WriteLine($"  Загружено билетов: {task3.Result}");
        }

        static async Task<int> LoadPassengersCountAsync()
        {
            await Task.Delay(1200);
            return 42;
        }

        static async Task<int> LoadTicketsCountAsync()
        {
            await Task.Delay(1800);
            return 100;
        }

        // ---------- УРОВЕНЬ 3: CancellationToken ----------
        static async Task RunCancellationDemoAsync()
        {
            using var cts = new CancellationTokenSource();

            // Автоматически отменить задачу через 500 мс
            cts.CancelAfter(500);

            try
            {
                Console.WriteLine("  Запуск долгой операции (5 сек), отмена через 0.5 сек...");
                await LongOperationAsync(cts.Token);
                Console.WriteLine("  Операция успела завершиться");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("  Операция была отменена!");
            }
        }

        static async Task LongOperationAsync(CancellationToken token)
        {
            await Task.Delay(5000, token);
        }
    }
}