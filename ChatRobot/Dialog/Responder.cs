namespace ChatRobot.Dialog
{
    using ApplicationBoot.Annotations;
    using DialogCache;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    [Service(typeof(IResponder))]
    public class Responder : IResponder
    {
        const int MinDistanceLevel = 5;
        private readonly IDialogCacheService dialogCacheService;

        public Responder(IDialogCacheService dialogCacheService)
        {
            this.dialogCacheService = dialogCacheService;
        }

        public string GetResponse(string subject)
        {
            return FindMatch(subject);
        }

        private string FindMatch(string subject)
        {
            string response;
            if (dialogCacheService.TryGet(subject, out response))
            {
                return response;
            }

            if (TryToFindBestMatch(subject, out response))
            {
                return response;
            }

            return string.Empty;
        }

        private bool TryToFindPerfectMatch(string subject, out string response)
        {
            if (dialogCacheService.TryGet(subject, out response))
            {
                return true;
            }
            return false;
        }

        private bool TryToFindBestMatch(string subject, out string response)
        {
            response = string.Empty;
            var topResponses = new List<Tuple<int, string>>();
            foreach (var resp in dialogCacheService.Get())
            {
                var rating = LevenshteinDistance.Compute(subject, resp.Key);
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
            if (!topResponses.Any())
            {
                return false;
            }

            var minDistance = topResponses.Min(x => x.Item1);
            if (minDistance < MinDistanceLevel)
            {
                subject = topResponses.FirstOrDefault(s => s.Item1 == minDistance).Item2;
                return true;
            }

            return false;
        }
    }
}
