using System;
using Szakdolgozat.Common;
using Szakdolgozat.Model.Events;

namespace Szakdolgozat.Model
{
    public abstract class ModelBase
    {
        protected IModelContext Context { get; private set; }

        public static event EventHandler<ModelErrorEventArgs> ModelError;

        public ModelBase(IModelContext context) { Context = context; }

        protected void OnModelError(string message)
        {
            ModelError?.Invoke(this, new ModelErrorEventArgs(message));
            throw new Exception();
        }
    }
}
