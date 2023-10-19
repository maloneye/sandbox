using BuinessLogic;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
		try
		{
			var db = new MySQLDatabase();
			var items = (List<Housemate>) db.GetHouseMates();

			items.ForEach(item => Console.WriteLine(item.Name));
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
    }
}