namespace wt_lab2_2oop_glebko.Models
{
    public class PassengerFlight : Flight
    {
        public int SeatsCount { get; set; }

        public PassengerFlight(int id, string number, string dest, decimal price, int seats)
            : base(id, number, dest, price)
        {
            SeatsCount = seats;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();   // вызов родительской версии
            Console.WriteLine($"    Пассажирский, мест: {SeatsCount}");
        }
    }
}