namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly Dob){

        var today = DateOnly.FromDateTime(DateTime.Now);
         
        var age = today.Year - Dob.Year;
         
        if(Dob>today.AddYears(-age)) age--;
         
        return age;

    }

}
