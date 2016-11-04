using System.Collections.Generic;

namespace ChatRobot.DialogCache
{
    public interface IDialogBaseDb
    {
        Dictionary<string, string> GetAll();
        void Add(string subject, string response, bool active);
    }
}
