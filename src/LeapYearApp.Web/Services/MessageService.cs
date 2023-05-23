namespace LeapYearApp.Web.Services
{
    public class MessageService : IMessageService
    {
        public string GenerateMessage(int year, string name, bool isFemale, bool isLeapYear)
        {
            string message, verb;

            if (isLeapYear)
            {
                message = "To był rok przestępny.";
            }
            else
            {
                message = "To nie był rok przestępny.";
            }

            if (name == null)
            {
                verb = "";
            }
            else
            {
                if (isFemale)
                {
                    verb = "urodziła";
                }
                else
                {
                    verb = "urodził";
                }
                message = $"{name} {verb} się w {year} roku. {message}";
            }

            return message;
        }
    }
}
