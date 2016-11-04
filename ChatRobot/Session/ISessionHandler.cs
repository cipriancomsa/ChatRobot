using ChatRobot.Dialog;

namespace ChatRobot.Session
{
    public interface ISessionHandler
    {
        bool TryGetPreviousConversation(out Conversation previousConversation);
        void AddConversationToHistory(Conversation conversation);
    }
}