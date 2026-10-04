namespace wt_lab2_2oop_glebko.Models
{
    public class CharterFlight : Flight
    {
        public string Customer { get; set; }

        public CharterFlight(int id, string number, string dest, decimal price, string customer)
            : base(id, number, dest, price)
        {
            Customer = customer;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"    Чартер для: {Customer}");
        }
    }
}