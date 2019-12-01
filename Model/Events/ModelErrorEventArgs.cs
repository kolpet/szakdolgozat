using System;

namespace Szakdolgozat.Model.Events
{
    public class ModelErrorEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ModelErrorEventArgs(string message)
        {
            Message = message;
        }
    }
}
