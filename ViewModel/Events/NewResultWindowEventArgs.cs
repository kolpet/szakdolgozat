using System;
using Szakdolgozat.Common;
using Szakdolgozat.Model;

namespace Szakdolgozat.ViewModel.Events
{
    public class NewResultWindowEventArgs : EventArgs
    {
        private Action<object, int> openNewResultWindow;

        public ResultModel Model { get; set; }

        public IContext Context { get; set; }

        public int Result { get; set; }

        public NewResultWindowEventArgs(ResultModel model, IContext context, int result)
        {
            Model = model;
            Context = context;
            Result = result;
        }

        public NewResultWindowEventArgs(Action<object, int> openNewResultWindow)
        {
            this.openNewResultWindow = openNewResultWindow;
        }
    }
}
