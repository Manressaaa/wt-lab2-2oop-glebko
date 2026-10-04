namespace wt_lab2_2oop_glebko.Models
{
    public class CargoFlight : Flight
    {
        public double MaxCargoWeight { get; set; }

        public CargoFlight(int id, string number, string dest, decimal price, double weight)
            : base(id, number, dest, price)
        {
            MaxCargoWeight = weight;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"    Грузовой, макс. вес: {MaxCargoWeight} т");
        }
    }
}