using System;
using Assignment4.Entities;


namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new KanbanContextFactory().CreateDbContext();
            
        }
    }
}
