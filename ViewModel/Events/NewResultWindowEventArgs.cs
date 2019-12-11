using System;
using Szakdolgozat.Common;

namespace Szakdolgozat.ViewModel.Events
{
    public class NewResultWindowEventArgs : EventArgs
    {
        private Action<object, int> openNewResultWindow;

        public IModelContext Context { get; set; }

        public int Result { get; set; }

        public NewResultWindowEventArgs(IModelContext context, int result)
        {
            Context = context;
            Result = result;
        }

        public NewResultWindowEventArgs(Action<object, int> openNewResultWindow)
        {
            this.openNewResultWindow = openNewResultWindow;
        }
    }
}
