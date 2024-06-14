using System;
using ProblemDomain;

class Program
{
    static void Main()
    {
        // code to read txt file, create objects and add them to a list.
        var applianceManager = new ApplianceManager();
        string filePath = "appliances.txt";
        applianceManager.LoadAppliances(filePath);

        while (true)
        {
            // displays menu options and collects user input
            Console.WriteLine("Welcome to Modern Appliances!");
            Console.WriteLine("How may we assist you?");
            Console.WriteLine("1 – Check out appliance");
            Console.WriteLine("2 – Find appliances by brand");
            Console.WriteLine("3 – Display appliances by type");
            Console.WriteLine("4 – Produce random appliance list");
            Console.WriteLine("5 – Save & exit");
            Console.Write("Enter option: ");
            int option = int.Parse(Console.ReadLine());

            // switch case to validate which menu option has been selected and calls the proper method
            switch (option)
            {
                case 1:
                    Console.Write("Enter item number of an appliance: ");
                    string itemNumber = Console.ReadLine();
                    applianceManager.PurchaseAppliance(itemNumber);
                    break;
                case 2:
                    Console.Write("Enter brand to search for: ");
                    string brand = Console.ReadLine();
                    applianceManager.SearchByBrand(brand);
                    break;
                case 3:
                    Console.WriteLine("Appliance Types");
                    Console.WriteLine("1 – Refrigerators");
                    Console.WriteLine("2 – Vacuums");
                    Console.WriteLine("3 – Microwaves");
                    Console.WriteLine("4 – Dishwashers");
                    Console.Write("Enter type of appliance: ");
                    int type = int.Parse(Console.ReadLine());
                    applianceManager.DisplayAppliancesByType(type);
                    break;
                case 4:
                    Console.Write("Enter number of random appliances to display: ");
                    int count = int.Parse(Console.ReadLine());
                    applianceManager.DisplayRandomAppliances(count);
                    break;
                case 5:
                    applianceManager.SaveAppliances(filePath);
                    Console.WriteLine("Appliances have been saved. Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine(); // Adds space before menu is printed again
        }
    }
}
