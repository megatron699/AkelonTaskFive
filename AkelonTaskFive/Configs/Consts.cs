using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkelonTaskFive.Configs
{
    internal class Consts
    {
        internal static readonly List<string> AvailableWorkingDaysOfWeekWithoutWeekends = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        internal static readonly int VacationCount = 28;
        internal static readonly int[] VacationSteps = { 7, 14 };
    }
}
