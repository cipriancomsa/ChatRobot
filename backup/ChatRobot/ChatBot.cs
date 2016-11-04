using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatRobot
{
    public static class ChatBot
    {
        public static Dictionary<string, List<string>> context = new Dictionary<string, List<string>>()
        {
            { "ThankYou", new List<string>() {" Thank you" }},
            { "HelpMe", new List<string>() {" Please tell me the answer" }},
        };

        private static bool IsLearningMode = false;
        private static bool Safe = false;
        public static string GetResponse(string input)
        {
            var response = new Responder(input).GetResponse();

            if (response == string.Empty)
            {
                Conversation previousConversation = null;
                if (SessionHandler.TryGetPreviousConversation(out previousConversation))
                {
                    if (previousConversation.LearningMode)
                    {
                        Safe = true;
                        IsLearningMode = false;
                        SessionHandler.AddConversationToHistory(BuildConversation(previousConversation.Question, input, IsLearningMode, Safe));
                        return context["ThankYou"][0];
                    }
                }
                Safe = false;
                response = context["HelpMe"][0];
                IsLearningMode = true;
            }
            SessionHandler.AddConversationToHistory(BuildConversation(input, response, IsLearningMode, false));
            return response;
        }

        private static Conversation BuildConversation(string question, string response, bool IsLearningMode, bool save)
        {
            return new Conversation() { LastConversation = DateTime.Now, Question = question, Response = response, LearningMode = IsLearningMode, Save = save };
        }

    }
}
