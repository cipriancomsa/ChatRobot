using ApplicationBoot.Annotations;
using System.Collections.Generic;

namespace ChatRobot.DialogCache
{
    [Service(typeof(IDialogCacheLoader))]
    public class DialogCacheLoader : IDialogCacheLoader
    {
        private static Dictionary<string, string> dialogBase = new Dictionary<string, string>();
        private readonly IDialogBaseDb dialogBaseService;
        private readonly IDialogCacheService dialogCacheService;
        public DialogCacheLoader(IDialogBaseDb dialogBaseService, IDialogCacheService dialogCacheService)
        {
            this.dialogBaseService = dialogBaseService;
            this.dialogCacheService = dialogCacheService;
        }

        public void Populate()
        {
            var dialogBaseEntities = this.dialogBaseService.GetAll();
            dialogCacheService.Populate(dialogBaseEntities);
        }
    }
}
