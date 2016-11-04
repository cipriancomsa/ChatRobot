using System;
using System.Collections.Generic;

namespace ChatRobot
{
    public static class SessionHandler
    {
        public static List<Conversation> dialog = new List<Conversation>();
        public static void AddConversationToHistory(Conversation conversation)
        {
            dialog.Add(conversation);
            if (conversation.Save)
            {
                SaveToFile(conversation);
            }
        }
     
        public static void Display()
        {
            Console.WriteLine("START PRINTING");
            foreach(var line in dialog)
            {
                Console.WriteLine(line.Question);
                Console.WriteLine(line.Response);
            }
        }

        public static bool TryGetPreviousConversation(out Conversation previousConversation)
        {
            previousConversation = null;
            if (dialog.Count > 0)
            {
                previousConversation = dialog[dialog.Count - 1];
                return true;
            }
            return false;
        }

        private static void SaveToFile(Conversation conversation)
        {
            var text = conversation.Question + ";" + conversation.Response + Environment.NewLine;
            FileUtils.Add(text);
        }
    }
}
