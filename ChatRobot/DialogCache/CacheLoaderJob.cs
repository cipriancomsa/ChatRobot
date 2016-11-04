using ApplicationBoot.Annotations;
using ApplicationBoot.Contracts;
using ChatRobot.DialogCache;

namespace ChatRobot
{
    [Service(typeof(IModule))]
    public class CacheLoaderJob : IModule
    {
        private readonly IDialogCacheLoader dialogCacheLoaderService;
        public CacheLoaderJob(IDialogCacheLoader dialogCacheLoaderService)
        {
            this.dialogCacheLoaderService = dialogCacheLoaderService;
        }

        public void Init()
        {
            dialogCacheLoaderService.Populate();
        }
    }
}
