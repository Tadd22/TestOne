using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        // Step 1: Get user's name
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        
        string birthdateInput;
        DateTime birthdate;

        // Step 2: Regex for validating birthdate format
        Regex dateRegex = new Regex(@"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$");

        // Step 3: Validating the birthdate format and parsing the date
        do
        {
            Console.Write("Enter your birthdate (mm/dd/yyyy): ");
            birthdateInput = Console.ReadLine();
            if (!dateRegex.IsMatch(birthdateInput) || 
                !DateTime.TryParseExact(birthdateInput, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate))
            {
                Console.WriteLine("Invalid date format! Please enter in mm/dd/yyyy format.");
            }
        } while (!dateRegex.IsMatch(birthdateInput) || 
                 !DateTime.TryParseExact(birthdateInput, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate));

        // Step 4: Calculate the user's age
        int age = DateTime.Now.Year - birthdate.Year;
        if (DateTime.Now.DayOfYear < birthdate.DayOfYear)
        {
            age--;
        }

        // Step 5: Save user information to a file
        string filePath = "user_info.txt";
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine($"Name: {name}");
            sw.WriteLine($"Age: {age}");
        }
        Console.WriteLine("\nUser information saved to 'user_info.txt'.");

        // Step 6: Read and display the contents of the "user_info.txt" file
        Console.WriteLine("\nReading contents of 'user_info.txt':");
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }

        // Step 7: Prompt the user to enter a directory path and list all files
        Console.Write("\nEnter a directory path: ");
        string directoryPath = Console.ReadLine();
        if (Directory.Exists(directoryPath))
        {
            Console.WriteLine("\nFiles in the directory:");
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }

        // Step 8: Prompt the user to input a string and format it to title case
        Console.Write("\nEnter a string to format to title case: ");
        string inputString = Console.ReadLine();
        
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        string titleCaseString = textInfo.ToTitleCase(inputString.ToLower());

        Console.WriteLine($"\nFormatted string in title case: {titleCaseString}");

        // Step 9: Explicitly triggering garbage collection
        Console.WriteLine("\nTriggering garbage collection...");
        GC.Collect(); // Force the garbage collector to run
        GC.WaitForPendingFinalizers(); 

        // Ensures that all finalizers have completed before proceeding
        Console.WriteLine("Garbage collection completed.");
    }
}
//C:\Users\Elitebook 1040 G3\Desktop