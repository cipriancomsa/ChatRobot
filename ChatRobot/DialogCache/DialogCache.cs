using ApplicationBoot.Annotations;
using System.Collections.Generic;

namespace ChatRobot.DialogCache
{
    [Service(typeof(IDialogCacheService), LifeTimeScope.Application)]
    public class DialogCache : IDialogCacheService
    {
        private static object syncronizer = new object();
        private static Dictionary<string, string> dialogBase = new Dictionary<string, string>();

        public Dictionary<string, string> Get()
        {
            lock (syncronizer)
            {
                return dialogBase;
            }
        }

        public void Populate(Dictionary<string, string> dialog)
        {
            lock(syncronizer)
            {
                dialogBase = dialog;
            }
        }

        public bool TryGet(string subject, out string response)
        {
            response = string.Empty;
            if (dialogBase.TryGetValue(subject, out response))
            {
                return true;
            }
            return false;
        }
    }
}
