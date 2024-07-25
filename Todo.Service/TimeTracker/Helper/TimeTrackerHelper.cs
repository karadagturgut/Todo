using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.DTO;
using Todo.Core.Entity;

namespace Todo.Service
{
    internal static class TimeTrackerHelper
    {
        internal static UserTimeTracker ConvertToEntity(TimeTrackerDTO dto)
        {
            string dateFormat = "yyyy-MM-ddTHH:mm";

            DateTime startDate = DateTime.ParseExact(dto.StartDate, dateFormat, null);
            DateTime endDate = DateTime.ParseExact(dto.EndDate, dateFormat, null);

            TimeSpan timeSpent = endDate - startDate;
            decimal hoursSpent = (decimal)timeSpent.TotalHours;

            return new UserTimeTracker
            {
                Id = dto.Id,
                UserId = (int)dto.UserId!,
                AssignmentId = dto.AssignmentId,
                Comment = dto.Comment,
                TimeSpent = hoursSpent,
                LoggedDate = startDate 
            };
        }
    }

}
