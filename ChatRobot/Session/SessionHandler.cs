using ApplicationBoot.Annotations;
using ChatRobot.Dialog;
using ChatRobot.DialogCache;
using System.Collections.Generic;

namespace ChatRobot.Session
{
    [Service(typeof(ISessionHandler), LifeTimeScope.Application)]
    public class SessionHandler : ISessionHandler
    {
        public static List<Conversation> dialog = new List<Conversation>();
        private readonly IDialogBaseDb dialogBaseDbService;
        private readonly IDialogCacheLoader dialogCacheLoaderService;

        public SessionHandler(IDialogBaseDb dialogBaseDbService, IDialogCacheLoader dialogCacheLoaderService)
        {
            this.dialogBaseDbService = dialogBaseDbService;
            this.dialogCacheLoaderService = dialogCacheLoaderService;
        }

        public void AddConversationToHistory(Conversation conversation)
        {
            dialog.Add(conversation);
            if (conversation.Save)
            {
                SaveToDb(conversation);
            }
        }
     
        public bool TryGetPreviousConversation(out Conversation previousConversation)
        {
            previousConversation = null;
            if (dialog.Count > 0)
            {
                previousConversation = dialog[dialog.Count - 1];
                return true;
            }
            return false;
        }

        private void SaveToDb(Conversation conversation)
        {
            dialogBaseDbService.Add(conversation.Question, conversation.Response, false);
            dialogCacheLoaderService.Populate();
        }
    }
}
