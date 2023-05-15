using AkelonTaskFive.Models;
using AkelonTaskFive.Services;

namespace AkelonTaskFive
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DoTask();
		}

		static void DoTask()
		{
			var VacationDictionary = new Dictionary<string, List<DateTime>>()
			{
				["Иванов Иван Иванович"] = new List<DateTime>(),
				["Петров Петр Петрович"] = new List<DateTime>(),
				["Юлина Юлия Юлиановна"] = new List<DateTime>(),
				["Сидоров Сидор Сидорович"] = new List<DateTime>(),
				["Павлов Павел Павлович"] = new List<DateTime>(),
				["Георгиев Георг Георгиевич"] = new List<DateTime>()
			};

			var vacationService = new VacationService();
			
			vacationService.CreateVacationsForEmployee("Иванов Иван Иванович");
			vacationService.CreateVacationsForEmployee("Павлов Павел Павлович");
			vacationService.CreateVacationsForEmployee("Петров Петр Петрович");
			vacationService.CreateVacationsForEmployee("Юлина Юлия Юлиановна");
			vacationService.CreateVacationsForEmployee("Сидоров Сидор Сидорович");
			vacationService.CreateVacationsForEmployee("Георгиев Георг Георгиевич");
			

			var employeeVacations = vacationService.GetEmployeeVacations();
			foreach(var employeeVacation in employeeVacations)
			{
				Console.WriteLine(employeeVacation.EmployeeFullName);

				foreach(var vacation in employeeVacation.Vacations)
				{
					Console.WriteLine(vacation.StartDate + " " + vacation.EndDate);
				}
				Console.WriteLine();
			}
		}
	}
}