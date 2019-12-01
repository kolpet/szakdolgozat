using System;
using Szakdolgozat.Model.Events;

namespace Szakdolgozat.Model
{
    public class ModelBase
    {
        //Singleton context
        private static Lazy<ModelContext> _context = new Lazy<ModelContext>(
            () => new ModelContext());

        protected ModelContext Context { get => _context.Value; }

        public IModelContext GetContext { get => _context.Value; }

        public static event EventHandler<ModelErrorEventArgs> ModelError;

        protected void OnModelError(string message)
        {
            ModelError?.Invoke(this, new ModelErrorEventArgs(message));
            throw new Exception();
        }
    }
}
