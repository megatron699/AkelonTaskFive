using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkelonTaskFive.Configs;
using AkelonTaskFive.Models;

namespace AkelonTaskFive.Services
{
    internal class VacationService
    {
        internal List<EmployeeVacation> EmployeeVacations { get; set; }

		internal VacationService()
		{
			EmployeeVacations = new List<EmployeeVacation>();
		}

		internal List<EmployeeVacation> GetEmployeeVacations()
		{
			return EmployeeVacations;
		}

        internal void CreateVacationsForEmployee(string employeeFullName)
        {
			var employeeVacation = new EmployeeVacation(employeeFullName);
			EmployeeVacations.Add(employeeVacation);
			while (employeeVacation.VacationCount > 0)
			{
				var availableWorkingDaysOfWeekWithoutWeekends = Consts.AvailableWorkingDaysOfWeekWithoutWeekends;
				var gen = new Random();
				var currentYear = DateOnly.FromDateTime(DateTime.Now).Year;
				var start = new DateOnly(currentYear, 1, 1);
				var end = new DateOnly(currentYear, 12, 31);
				int range = end.DayNumber - start.DayNumber;
				var startDate = start.AddDays(gen.Next(range));

				if (!availableWorkingDaysOfWeekWithoutWeekends.Contains(startDate.DayOfWeek.ToString()))
				{
					continue;
				}

				var possibleVacation = employeeVacation.CreatePossibleVacation(startDate);

				bool CanCreateVacation = false;
				bool existStart = false;
				bool existEnd = false;

				var allVacations = GetVacationList();

				if (!allVacations.Any(element => element.StartDate >= startDate && element.EndDate <= possibleVacation.EndDate))
				{
					if (!allVacations.Any(element => element.StartDate.AddDays(3) >= startDate && element.StartDate.AddDays(3) <= possibleVacation.EndDate))
					{
						existStart = employeeVacation.Vacations.Any(element => element.StartDate.AddMonths(1) >= startDate && element.EndDate.AddMonths(1) >= possibleVacation.EndDate); //TODO Fix infinite loop
						existEnd = employeeVacation.Vacations.Any(element => element.StartDate.AddMonths(-1) <= startDate && element.EndDate.AddMonths(-1) <= possibleVacation.EndDate);

						if (!existStart || !existEnd)
						{
							CanCreateVacation = true;
						}
					}
				}

				if (!CanCreateVacation)
				{
					continue;
				}
				employeeVacation.AddVacation(possibleVacation);
			}
		}

		private List<Vacation> GetVacationList()
		{
			return EmployeeVacations.SelectMany(element => element.Vacations).ToList();
		}
    }
}
