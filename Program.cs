namespace FileOrg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== File Organizer ===");
                Console.WriteLine("1. Sort a folder");
                Console.WriteLine("2. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine()?.Trim();

                if (choice == "2")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else if (choice == "1")
                {
                    Console.Write("Enter folder path to organize: ");
                    string sourcePath = Console.ReadLine() ?? "";

                    if (!Directory.Exists(sourcePath))
                    {
                        Console.WriteLine("Invalid path. Press Enter to try again.");
                        Console.ReadLine();
                        continue;
                    }

                    try
                    {
                        FileSorter sorter = new FileSorter(sourcePath);
                        sorter.SortFiles();
                        Console.WriteLine("Sorting completed!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }

                    Console.WriteLine("Press Enter to return to the menu.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Invalid option. Press Enter to try again.");
                    Console.ReadLine();
                }
            }
        }
    }
}
