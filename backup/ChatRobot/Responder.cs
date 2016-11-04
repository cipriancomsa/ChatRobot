namespace ChatRobot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Responder
    {
        private Dictionary<string, string> responses;

        
        const int MinDistanceLevel = 5;
        private readonly string question;

        public Responder(string question)
        {
            responses = KnowledgeBase.GetQuestionsAndAnswers();
            this.question = question;
        }

        public string GetResponse()
        {
            return FindMatch();
        }

        public string FindMatch()
        {
            string response;
            var found = TryToFindPerfectMatch(out response);
            if (found)
            {
                return response;
            }
            if (TryToFindBestMatch(out response))
            {
                return response;
            }

            return string.Empty;
        }

        public bool TryToFindPerfectMatch(out string response)
        {
            if (responses.TryGetValue(this.question, out response))
            {
                return true;
            }
            return false;
        }


        private bool TryToFindBestMatch(out string response)
        {
            response = string.Empty;
            var topResponses = new List<Tuple<int, string>>();
            foreach (var resp in responses)
            {
                var rating = LevenshteinDistance.Compute(question, resp.Key);
                topResponses.Add(new Tuple<int, string>(rating, resp.Key));
            }
            string bestSubject = string.Empty;

            if (TryGetBestSubject(topResponses, out bestSubject))
            {
                return true;
            }
            return false;
        }

        private bool TryGetBestSubject(List<Tuple<int, string>> topResponses, out string subject)
        {
            subject = string.Empty;
            var minDistance = topResponses.Min(x => x.Item1);
            if (minDistance < MinDistanceLevel)
            {
                subject = topResponses.Single(s => s.Item1 == minDistance).Item2;
                return true;
            }

            return false;
        }

    }
}
