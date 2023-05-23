namespace LeapYearApp.Web.Services
{
    public interface IMessageService
    {
        public string GenerateMessageForIndexPage(int year, string name, bool isFemale, bool isLeapYear);
    }
}
