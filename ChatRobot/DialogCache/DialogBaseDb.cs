using ApplicationBoot.Annotations;
using Dal;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatRobot.DialogCache
{
    [Service(typeof(IDialogBaseDb), LifeTimeScope.Application)]
    public class DialogBaseDb : IDialogBaseDb
    {
        private readonly IRepository repository;

        public DialogBaseDb(IRepository repository)
        {
            this.repository = repository;
        }

        public Dictionary<string, string> GetAll()
        {
            var dialogBase = this.repository.GetEntities<DialogBase>().Where(d=>d.active == 1).ToArray();
            return dialogBase.ToDictionary(item => item.subject, item => item.response);
        }

        public void Add(string subject, string response, bool active)
        {
            var dialogBaseEntity = BuildDialogBaseEntity(subject, response, active);
            this.repository.Add(dialogBaseEntity);
            this.repository.SaveChanges();
        }

        private DialogBase BuildDialogBaseEntity(string subject, string response, bool active)
        {
            return new DialogBase
            {
                active = Convert.ToByte(active),
                response = response,
                subject = subject
            };
        }
    }
}
