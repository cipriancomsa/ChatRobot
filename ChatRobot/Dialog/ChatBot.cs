using ApplicationBoot.Annotations;
using ChatRobot.Session;
using ChatRobot.TextCleaners;
using System;
using System.Collections.Generic;

namespace ChatRobot.Dialog
{
    [Service(typeof(IChatRobot), LifeTimeScope.Application)]
    public class ChatBot : IChatRobot
    {
        public static Dictionary<string, List<string>> context = new Dictionary<string, List<string>>()
        {
            { "ThankYou", new List<string>() {" Thank you" }},
            { "HelpMe", new List<string>() {" Please tell me the answer" }},
        };

        private static bool IsLearningMode = false;
        private static bool Save = false;
        private readonly IResponder responder;
        private readonly ISessionHandler sessionHandlerService;
        private readonly ITextCleaner[] textCleanerTools;
        public ChatBot(IResponder responder, ISessionHandler sessionHandlerService, ITextCleaner[] textCleanerTools)
        {
            this.responder = responder;
            this.sessionHandlerService = sessionHandlerService;
            this.textCleanerTools = textCleanerTools;
        }

        public string GetResponse(string input)
        {
            input = CleanText(input);
            var response = this.responder.GetResponse(input);

            if (string.IsNullOrEmpty(response))
            {
                Conversation previousConversation = null;
                if (this.sessionHandlerService.TryGetPreviousConversation(out previousConversation))
                {
                    if (previousConversation.LearningMode)
                    {
                        Save = true;
                        IsLearningMode = false;
                        this.sessionHandlerService.AddConversationToHistory(BuildConversation(previousConversation.Question, input, IsLearningMode, Save));
                        return context["ThankYou"][0];
                    }
                }
                Save = false;
                response = context["HelpMe"][0];
                IsLearningMode = true;
            }
            this.sessionHandlerService.AddConversationToHistory(BuildConversation(input, response, IsLearningMode, Save));
            return response;
        }

        private static Conversation BuildConversation(string question, string response, bool IsLearningMode, bool save)
        {
            return new Conversation() { LastConversation = DateTime.Now, Question = question, Response = response, LearningMode = IsLearningMode, Save = save };
        }

        private string CleanText(string input)
        {
            foreach (var textCleanerTool in this.textCleanerTools)
            {
                input = textCleanerTool.GetCleaned(input);
            }

            return input;
        }

    }
}
