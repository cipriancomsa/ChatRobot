using ChatRobot.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRobot
{
    public static class KnowledgeBase
    {
        private static Dictionary<string, string> responses = new Dictionary<string, string>();

        private static void LoadResponses()
        {
            var fileContent = FileUtils.Read();
            string[] lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach(var line in lines)
            {
                string[] questionAndAnswer = line.Split(';');
                string question;
                if (!responses.TryGetValue(questionAndAnswer[0], out question))
                {
                    responses.Add(questionAndAnswer[0], questionAndAnswer[1]);
                }
            }
        }

        public static Dictionary<string, string> GetQuestionsAndAnswers()
        {
            LoadResponses();
            return responses;
        }

        public static void RegisterConversation(string question, string response)
        {
            FileUtils.Add(question + ";" + response);
        }
    }
}
