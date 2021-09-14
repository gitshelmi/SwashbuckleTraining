using System;

namespace SwaggerTraining.Helpers.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var age = DateTime.UtcNow.Year - dob.Year;

            if (DateTime.UtcNow.DayOfYear < dob.DayOfYear)
            {
                age--;
            }

            return age;
        }
    }
}
