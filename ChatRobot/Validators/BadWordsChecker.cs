using ApplicationBoot.Annotations;
using Dal;
using DataModel;
using System.Linq;

namespace ChatRobot.Validators
{
    [Service("BadWordsChecker", typeof(IValidatorService))]
    public class BadWordsChecker : IValidatorService
    {
        private readonly IRepository repository;

        public BadWordsChecker(IRepository repository)
        {
            this.repository = repository;
        }

        public bool IsValid(string subject)
        {
            var badWord = this.repository.GetEntities<BadWord>().FirstOrDefault(s => s.word == subject);
            if (badWord != null)
                return false;
            return true;
        }
    }
}
