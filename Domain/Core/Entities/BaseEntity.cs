using Domain.Core.EventSourcing;
using Flunt.Notifications;
using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public abstract class BaseEntity : Entity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            entityNotifcations = new EntityNotifcations();
        }
        public virtual bool IsTransient() => Id == default;

        #region Notifiable Contract
        public IReadOnlyCollection<Notification> Notifications => entityNotifcations.Notifications;

        private readonly EntityNotifcations entityNotifcations;
        public bool Invalid => entityNotifcations.Invalid;
        public bool Valid => entityNotifcations.Valid;

        public void AddNotifications(BaseEntity entity)
        {
            AddNotifications(entity.entityNotifcations);
        }
        public void AddNotifications(Notifiable item)
        {
            entityNotifcations.AddNotifications(item?.Notifications);
        }

        public void AddNotification(string property, string message)
        {
            entityNotifcations.AddNotification(new Notification(property, message));
        }
        #endregion
    }


}
