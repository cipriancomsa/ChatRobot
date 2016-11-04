namespace ChatRobot.TextCleaners
{
    public interface ITextCleaner
    {
        string GetCleaned(string text);
    }
}
