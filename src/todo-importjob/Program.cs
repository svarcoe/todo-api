using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todoist.Net;
using Todoist.Net.Models;

namespace Todo.ImportJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TodoistClient client = new TodoistClient("10fe48577af27c2273c83143457ffb1b4d92f8eb");
            IEnumerable<Item> items = await client.Items.GetAsync();
            foreach (Item item in items)
            {
                Console.WriteLine($"{item.Id}:{item.Content}");
            }

            Console.ReadLine();
        }
    }
}
