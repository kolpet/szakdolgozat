using System;

namespace Szakdolgozat.ViewModel.Events
{
    public class AlgorithmOptionChangedEventArgs : EventArgs
    {
        public int Index { get; set; }

        public AlgorithmOptionChangedEventArgs(int index)
        {
            Index = index;
        }
    }
}
