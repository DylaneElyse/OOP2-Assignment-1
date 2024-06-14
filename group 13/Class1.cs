using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProblemDomain
{
    // appliance class
    public abstract class Appliance
    {
        // class properties
        public string ItemNumber { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int Wattage { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        //dictionaries to display the proper outputs when required
        public Dictionary<string, string> soundRatings = new Dictionary<string, string>
        {
            { "Qt", "Quietest" },
            { "Qr", "Quieter" },
            { "Qu", "Quiet" },
            { "M", "Moderate" }
        };

        public Dictionary<string, string> roomTypes = new Dictionary<string, string>
        {
            { "K", "Kitchen" },
            { "W", "Work Site" }
        };

        // default constructor and parameterized constructor
        protected Appliance() { }
        protected Appliance(string itemNumber, string brand, int quantity, int wattage, string color, decimal price)
        {
            ItemNumber = itemNumber;
            Brand = brand;
            Quantity = quantity;
            Wattage = wattage;
            Color = color;
            Price = price;
        }

        // parent ToString method
        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price:C}";
        }
    }

    // Refrigerator class derived from Appliance class
    public class Refrigerator : Appliance
    {
        // Refrigerator properties
        public int NumberOfDoors { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        // default and parameterized constructors
        public Refrigerator() { }
        public Refrigerator(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, int numberOfDoors, int height, int width)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            NumberOfDoors = numberOfDoors;
            Height = height;
            Width = width;
        }

        // ToString method for Refrigerator
        public override string ToString()
        {
            string NumberOfDoors_String = $"{NumberOfDoors}"; // Default
            if (NumberOfDoors == 2) {
                NumberOfDoors_String = "Double doors";
            } else if (NumberOfDoors == 3) {
                NumberOfDoors_String = "Three doors";
            } else if (NumberOfDoors == 4) {
                NumberOfDoors_String = "Four doors";
            }

            return base.ToString() + $"\nNumber of Doors: {NumberOfDoors_String}\nHeight: {Height}\nWidth: {Width}";
        }
    }

    // Vacuum class derived from Appliance class
    public class Vacuum : Appliance
    {
        // Vacuum properties
        public string Grade { get; set; }
        public int BatteryVoltage { get; set; }

        // default and parameterized constructors
        public Vacuum() { }
        public Vacuum(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, string grade, int batteryVoltage)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Grade = grade;
            BatteryVoltage = batteryVoltage;
        }

        // ToString method
        public override string ToString()
        {
            string BatteryVoltageLH = $"{BatteryVoltage}V"; // backup output
            if (BatteryVoltage == 18) {
                BatteryVoltageLH = "Low";
            } else if (BatteryVoltage == 24) {
                BatteryVoltageLH = "High";
            }

            return base.ToString() + $"\nGrade: {Grade}\nBattery Voltage: {BatteryVoltageLH}";
        }
    }

    // Microwave class derived from Appliance class
    public class Microwave : Appliance
    {
        // properties for Microwave
        public double Capacity { get; set; }
        public string RoomType { get; set; }

        // default and parameterized constructors
        public Microwave() { }
        public Microwave(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, double capacity, string roomType)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Capacity = capacity;
            RoomType = roomType;
        }

        // ToString method
        public override string ToString()
        {
            base.roomTypes.TryGetValue(RoomType, out string roomTypeFull); // Changes short name (K) to long name (Kitchen)
            return base.ToString() + $"\nCapacity: {Capacity}\nRoom Type: {roomTypeFull}";
        }
    }

    // Dishwasher class derived from Appliance class
    public class Dishwasher : Appliance
    {
        // properties for Dishwasher
        public string Feature { get; set; }
        public string SoundRating { get; set; }

        // default and parameterized constructors
        public Dishwasher() { }
        public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, string feature, string soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Feature = feature;
            SoundRating = soundRating;
        }

        // ToString method
        public override string ToString()
        {
            base.soundRatings.TryGetValue(SoundRating, out string soundRatingFull); // Changes short name (Qt) to long name (Quietest)
            return base.ToString() + $"\nFeature: {Feature}\nSound Rating: {soundRatingFull}";
        }
    }

    // ApplianceManager class to handle functions of the program
    public class ApplianceManager
    {
        // creating empty list for Appliances
        public List<Appliance> Appliances { get; private set; }

        // creates new appliance and adds it to the list
        public ApplianceManager()
        {
            Appliances = new List<Appliance>();
        }

        // code section that reads a txt file, parses the information, creates new objects, and adds them to the list
        public void LoadAppliances(string filePath)
        {
            // loop to iterate through each line of the file
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(';');
                var itemNumber = parts[0];
                var firstDigit = itemNumber[0];

                // switch case that determines based on the firstDigit which appliance the information needs to be added to
                switch (firstDigit)
                {
                    case '1':
                        Appliances.Add(new Refrigerator(itemNumber, parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], decimal.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[7]), int.Parse(parts[8])));
                        break;
                    case '2':
                        Appliances.Add(new Vacuum(itemNumber, parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], decimal.Parse(parts[5]), parts[6], int.Parse(parts[7])));
                        break;
                    case '3':
                        Appliances.Add(new Microwave(itemNumber, parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], decimal.Parse(parts[5]), double.Parse(parts[6]), parts[7]));
                        break;
                    case '4':
                        Appliances.Add(new Dishwasher(itemNumber, parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], decimal.Parse(parts[5]), parts[6], parts[7]));
                        break;
                    case '5':
                        Appliances.Add(new Dishwasher(itemNumber, parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], decimal.Parse(parts[5]), parts[6], parts[7]));
                        break;
                }
            }
        }

        // appliance purchasing/check out method
        public void PurchaseAppliance(string itemNumber)
        {
            // finds the proper appliance from the list based on the item number inputted by the user
            var appliance = Appliances.Find(a => a.ItemNumber == itemNumber);

            // conditional statements that check the quality of the chosen item return a message based on the outcome
            if (appliance == null)
            {
                Console.WriteLine("No appliances found with that item number.");
            }
            else if (appliance.Quantity <= 0)
            {
                Console.WriteLine("The appliance is not available to be checked out.");
            }
            else
            {
                appliance.Quantity--;
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
            }
        }

        // method to search for appliances by a specific brand
        public void SearchByBrand(string brand)
        {
            // code that checks the inputted brand name against each appliance on the Appliances list and adds them to a separate list if the brand names match
            var matchedAppliances = Appliances.FindAll(a => a.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));

            // conditional statement to print a message based on if any matching appliances were found
            if (matchedAppliances.Count == 0)
            {
                Console.WriteLine("No appliances found for the specified brand.");
            }
            else
            {
                Console.WriteLine("Matching Appliances:");
                foreach (var appliance in matchedAppliances)
                {
                    Console.WriteLine(appliance + "\n");
                }
            }
        }

        // method to display select appliances based on an inputted type
        public void DisplayAppliancesByType(int type)
        {
            // creates new empty list to hold the found appliances
            List<Appliance> matchedAppliances = new List<Appliance>();

            // switch case to search for appliances based on the inputted selection by the user
            switch (type)
            {
                case 1:
                    Console.Write("Enter number of doors: 2 (double door), 3 (three doors) or 4 (four doors): ");
                    int numberOfDoors = int.Parse(Console.ReadLine());
                    matchedAppliances = Appliances.OfType<Refrigerator>().Where(r => r.NumberOfDoors == numberOfDoors).Cast<Appliance>().ToList();
                    break;
                case 2:
                    Console.Write("Enter battery voltage value. 18 V (low) or 24 V (high): ");
                    int batteryVoltage = int.Parse(Console.ReadLine());
                    matchedAppliances = Appliances.OfType<Vacuum>().Where(v => v.BatteryVoltage == batteryVoltage).Cast<Appliance>().ToList();
                    break;
                case 3:
                    Console.Write("Enter room type: K (kitchen) or W (work site): ");
                    string roomType = Console.ReadLine();
                    matchedAppliances = Appliances.OfType<Microwave>().Where(m => m.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase)).Cast<Appliance>().ToList();
                    break;
                case 4:
                    Console.Write("Enter sound rating: Qt (Quietest), Qr (Quieter), Qu (Quiet), M (Moderate): ");
                    string soundRating = Console.ReadLine();
                    matchedAppliances = Appliances.OfType<Dishwasher>().Where(d => d.SoundRating.Equals(soundRating, StringComparison.OrdinalIgnoreCase)).Cast<Appliance>().ToList();
                    break;
                default:
                    Console.WriteLine("Invalid appliance type.");
                    return;
            }

            // conditional statement to return a message or appliance information based on if any appliances were found
            if (matchedAppliances.Count == 0)
            {
                Console.WriteLine("No matching appliances found.");
            }
            else
            {
                Console.WriteLine("Matching appliances:");
                foreach (var appliance in matchedAppliances)
                {
                    Console.WriteLine(appliance + "\n");
                }
            }
        }

        // method to display a random set of appliances
        public void DisplayRandomAppliances(int count)
        {
            var random = new Random();
            var randomAppliances = Appliances.OrderBy(a => random.Next()).Take(count).ToList();

            Console.WriteLine("Random appliances:");
            foreach (var appliance in randomAppliances)
            {
                Console.WriteLine(appliance + "\n");
            }
        }

        // method to save the appliances from the Appliances list into a txt file
        public void SaveAppliances(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var appliance in Appliances)
                {
                    if (appliance is Refrigerator refrigerator)
                    {
                        writer.WriteLine($"{refrigerator.ItemNumber};{refrigerator.Brand};{refrigerator.Quantity};{refrigerator.Wattage};{refrigerator.Color};{refrigerator.Price};{refrigerator.NumberOfDoors};{refrigerator.Height};{refrigerator.Width}");
                    }
                    else if (appliance is Vacuum vacuum)
                    {
                        writer.WriteLine($"{vacuum.ItemNumber};{vacuum.Brand};{vacuum.Quantity};{vacuum.Wattage};{vacuum.Color};{vacuum.Price};{vacuum.Grade};{vacuum.BatteryVoltage}");
                    }
                    else if (appliance is Microwave microwave)
                    {
                        writer.WriteLine($"{microwave.ItemNumber};{microwave.Brand};{microwave.Quantity};{microwave.Wattage};{microwave.Color};{microwave.Price};{microwave.Capacity};{microwave.RoomType}");
                    }
                    else if (appliance is Dishwasher dishwasher)
                    {
                        writer.WriteLine($"{dishwasher.ItemNumber};{dishwasher.Brand};{dishwasher.Quantity};{dishwasher.Wattage};{dishwasher.Color};{dishwasher.Price};{dishwasher.Feature};{dishwasher.SoundRating}");
                    }
                }
            }
        }
    }
}
