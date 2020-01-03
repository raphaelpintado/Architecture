using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Validator.Notification;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Arquitetura.Services.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private IRepository<T> _repository;
        private readonly INotification _notification;

        public BaseService(IRepository<T> repository, INotification notification)
        {
            _repository = repository;
            _notification = notification;
        }       

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public T Get(int id)
        {
            return _repository.Select(id);
        }

        public IList<T> Get()
        {
            return _repository.SelectAll();
        }

        public T Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Insert(obj);
            return obj;
        }

        public T Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Update(obj);
            return obj;
        }

        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");            

            validator.ValidateAndThrow(obj);
        }

        #region notificationPattern

        public void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        public void Notify(string errorMessage)
        {
            _notification.Handle(new Message(errorMessage));
        }

        public bool Validate<TValidation, TEntity>(TValidation validation, TEntity entity)
            where TValidation : AbstractValidator<TEntity> where TEntity : BaseEntity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }

        #endregion
    }
}
