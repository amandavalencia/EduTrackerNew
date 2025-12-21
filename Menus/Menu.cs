using Spectre.Console;

namespace EduTrackerNew.Menus
{
    internal class Menu
    {
        public static void MainMenu()
        {
            Console.Clear();
            var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Welcome to EduTracker! What do you want to do?")
            .AddChoices(new[] {
            "Get all students",
            "Get all students from specific class",
            "Add new student",
            "Add new staff",
            "Get staff"
            }));

            switch (menu)
            {
                case "Get all students":
                    GetStudentVisual();
                    break;
                case "Get all students from specific class":
                    GetStudentsByClassVisual();
                    break;
                case "Add new student":
                    AddStudentVisual();
                    break;
                case "Add new staff":
                    AddStaffVisual();
                    break;
                case "Get staff":
                    GetStaffVisual();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }


        public static void GetStudentVisual()
        {
            int sortField;
            int sortOrder;

            while (true)
            {
                Console.WriteLine("Sort students by:");
                Console.WriteLine("1. First name");
                Console.WriteLine("2. Last name");

                int.TryParse(Console.ReadLine(), out sortField);

                if (sortField == 1 || sortField == 2)
                    break;

                Console.WriteLine("Invalid choice. Try again.\n");
            }

            while (true)
            {
                Console.WriteLine("Sort by:");
                Console.WriteLine("1. Ascending");
                Console.WriteLine("2. Descending");

                int.TryParse(Console.ReadLine(), out sortOrder);

                if (sortOrder == 1 || sortOrder == 2)
                    break;

                Console.WriteLine("Invalid choice. Try again.\n");
            }
            Console.Clear();

            Service.GetStudents(sortField, sortOrder);
            Console.ReadKey();
            Console.WriteLine("Press enter to return to menu");
            MainMenu();
        }

        public static void GetStudentsByClassVisual()
        {
            Console.WriteLine("All classes:");
            Service.ShowAllClasses();
            Console.WriteLine("Choose by writing the name of the class");
            string className = Console.ReadLine()?.ToUpper() ?? "";

            Service.GetStudnetsFromClass(className);
            Console.WriteLine("Press enter to return to menu");
            Console.ReadKey();
            MainMenu();
        }

        public static void AddStudentVisual()
        {
            Console.Write("First name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last name: ");
            var lastName = Console.ReadLine();

            Console.WriteLine("All classes:");
            Service.ShowAllClasses();
            Console.Write("Choose class for new student: ");
            var className = Console.ReadLine()?.ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(className))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            Service.AddStudent(firstName, lastName, className);
            Console.WriteLine("Student added successfully!");
            Console.WriteLine("Press enter to return to menu");
            Console.ReadKey();
            MainMenu();
        }

        public static void AddStaffVisual()
        {
            Console.Write("First name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last name: ");
            var lastName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) ||
               string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            int positionId = HelperMethods.ChoosePosition();

            Service.AddStaff(firstName, lastName, positionId);

            Console.WriteLine("Staff added successfully!");
            Console.WriteLine("Press enter to return to menu");
            Console.ReadKey();
            MainMenu();
        }

        public static void GetStaffVisual()
        {
            while (true)
            {

                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Show all staff");
                Console.WriteLine("2. Show staff by position");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var allStaff = Service.GetAllStaff();
                        foreach (var s in allStaff)
                        {
                            Console.WriteLine($"{s.FirstName} {s.LastName}");
                        }
                        break;

                    case "2":
                        int positionId = HelperMethods.ChoosePosition();
                        var staffList = Service.GetStaffById(positionId);
                        foreach (var s in staffList)
                        {
                            Console.WriteLine($"{s.FirstName} {s.LastName}");
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                break;
            }
            Console.WriteLine("Press enter to return to menu");
            Console.ReadKey();
            MainMenu();

        }
    }
}
