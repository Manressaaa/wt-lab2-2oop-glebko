using System;

namespace wt_lab2_2oop_glebko.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected BaseEntity(int id)
        {
            Id = id;
            CreatedAt = DateTime.Now;
        }

        // protected-метод — доступен только наследникам
        protected void LogCreation()
        {
            Console.WriteLine($"[LOG] Сущность #{Id} создана в {CreatedAt:HH:mm:ss}");
        }

        // Контракт: каждый наследник ОБЯЗАН реализовать
        public abstract void PrintInfo();
    }
}