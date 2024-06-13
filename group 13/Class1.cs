﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProblemDomain
{
    public abstract class Appliance
    {
        public string ItemNumber { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int Wattage { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        protected Appliance(string itemNumber, string brand, int quantity, int wattage, string color, decimal price)
        {
            ItemNumber = itemNumber;
            Brand = brand;
            Quantity = quantity;
            Wattage = wattage;
            Color = color;
            Price = price;
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price:C}";
        }
    }

    public class Refrigerator : Appliance
    {
        public int NumberOfDoors { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Refrigerator(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, int numberOfDoors, int height, int width)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            NumberOfDoors = numberOfDoors;
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nNumber of Doors: {NumberOfDoors}\nHeight: {Height}\nWidth: {Width}";
        }
    }

    public class Vacuum : Appliance
    {
        public string Grade { get; set; }
        public int BatteryVoltage { get; set; }

        public Vacuum(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, string grade, int batteryVoltage)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Grade = grade;
            BatteryVoltage = batteryVoltage;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nGrade: {Grade}\nBattery Voltage: {BatteryVoltage}V";
        }
    }

    public class Microwave : Appliance
    {
        public double Capacity { get; set; }
        public string RoomType { get; set; }

        public Microwave(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, double capacity, string roomType)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Capacity = capacity;
            RoomType = roomType;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nCapacity: {Capacity}\nRoom Type: {RoomType}";
        }
    }

    public class Dishwasher : Appliance
    {
        public string Feature { get; set; }
        public string SoundRating { get; set; }

        public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, string feature, string soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Feature = feature;
            SoundRating = soundRating;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nFeature: {Feature}\nSound Rating: {SoundRating}";
        }
    }

    public class ApplianceManager
    {
        public List<Appliance> Appliances { get; private set; }

        public ApplianceManager()
        {
            Appliances = new List<Appliance>();
        }

        public void LoadAppliances(string filePath)
        {
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(';');
                var itemNumber = parts[0];
                var firstDigit = itemNumber[0];

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
                    case '5':
                        Appliances.Add(new Dishwasher(itemNumber, parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], decimal.Parse(parts[5]), parts[6], parts[7]));
                        break;
                }
            }
        }

        public void PurchaseAppliance(string itemNumber)
        {
            var appliance = Appliances.Find(a => a.ItemNumber == itemNumber);
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

        public void SearchByBrand(string brand)
        {
            var matchedAppliances = Appliances.FindAll(a => a.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));
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

        public void DisplayAppliancesByType(int type)
        {
            List<Appliance> matchedAppliances = new List<Appliance>();

            switch (type)
            {
                case 1:
                    Console.WriteLine("Enter number of doors: 2 (double door), 3 (three doors) or 4 (four doors):");
                    int numberOfDoors = int.Parse(Console.ReadLine());
                    matchedAppliances = Appliances.OfType<Refrigerator>().Where(r => r.NumberOfDoors == numberOfDoors).Cast<Appliance>().ToList();
                    break;
                case 2:
                    Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high)");
                    int batteryVoltage = int.Parse(Console.ReadLine());
                    matchedAppliances = Appliances.OfType<Vacuum>().Where(v => v.BatteryVoltage == batteryVoltage).Cast<Appliance>().ToList();
                    break;
                case 3:
                    Console.WriteLine("Enter room type: K (kitchen) or W (work site)");
                    string roomType = Console.ReadLine();
                    matchedAppliances = Appliances.OfType<Microwave>().Where(m => m.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase)).Cast<Appliance>().ToList();
                    break;
                case 4:
                    Console.WriteLine("Enter sound rating: Qt (Quietest), Qr (Quieter), Qu (Quiet), M (Moderate)");
                    string soundRating = Console.ReadLine();
                    matchedAppliances = Appliances.OfType<Dishwasher>().Where(d => d.SoundRating.Equals(soundRating, StringComparison.OrdinalIgnoreCase)).Cast<Appliance>().ToList();
                    break;
                default:
                    Console.WriteLine("Invalid appliance type.");
                    return;
            }

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