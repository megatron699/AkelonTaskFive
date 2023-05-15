using AkelonTaskFive.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkelonTaskFive.Models
{
	internal class EmployeeVacation
    {
        internal string EmployeeFullName { get; private set; }
        internal List<Vacation> Vacations { get; private set; }
		public int VacationCount { get; private set; }

        public EmployeeVacation(string employeeFullName)
        {
            EmployeeFullName = employeeFullName;
            Vacations = new List<Vacation>();
			VacationCount = Consts.VacationCount;
		}

		public int GetAvailableDaysCount(int vacationStep)
		{
			VacationCount -= vacationStep;
			return VacationCount;
		}

		public int GetVacationStep()
		{
			var gen = new Random();

			var vacationSteps = Consts.VacationSteps;
			int vacIndex = gen.Next(vacationSteps.Length);

			int vacationStep;
			if (VacationCount <= 7)
			{
				vacationStep = VacationCount;
			}
			else
			{
				vacationStep = Consts.VacationSteps[vacIndex];
			}

			return vacationStep;
		} 

        public void AddVacation(Vacation vacation)
        {
			if(VacationCount <= 0)
			{
				return;
			}
            Vacations.Add(vacation);
			VacationCount -= vacation.EndDate.DayNumber - vacation.StartDate.DayNumber;
		}

		public Vacation CreatePossibleVacation(DateOnly startDate)
		{
			int difference = GetVacationStep();
			var endDate = startDate.AddDays(difference);
			return new Vacation()
			{
				StartDate = startDate,
				EndDate = endDate
			};
		}
    }
}
