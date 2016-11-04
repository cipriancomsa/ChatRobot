using System;

namespace ChatRobot.Dialog
{
    public class Conversation
    {
        public string SessionId { set; get; }
        public DateTime LastConversation { set; get; }
        public string Question { set; get; }
        public string Response { set; get; }
        public bool LearningMode { set; get; }
        public bool Save { set; get; }
    }
}
