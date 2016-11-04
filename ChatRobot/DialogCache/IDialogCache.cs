using System.Collections.Generic;

namespace ChatRobot.DialogCache
{
    public interface IDialogCacheService
    {
        void Populate(Dictionary<string, string> dialogBase);
        Dictionary<string, string> Get();
        bool TryGet(string subject, out string response);
    }
}
