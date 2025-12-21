using EduTrackerNew.Data;

namespace EduTrackerNew
{
    internal class HelperMethods
    {
        public static int ChoosePosition()
        {
            using var context = new EduTrackerDbContext();

            var positions = context.Positions.ToList();

            Console.WriteLine("Choose a position:");

            for (int i = 0; i < positions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {positions[i].PositionName}");
            }

            int choice;
            while (true)
            {
                Console.Write("Enter number: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out choice) &&
                    choice >= 1 &&
                    choice <= positions.Count)
                {
                    break;
                }

                Console.WriteLine("Invalid choice, try again.");
            }

            return positions[choice - 1].PositionId;
        }

    }
}
