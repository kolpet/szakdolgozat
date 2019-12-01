using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public class AlgorithmOptionPanel : ViewModelBase
    {
        private int _index;

        public IAlgorithmOptionElement AlgorithmOption { get; set; }

        public DelegateCommand UpdateCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        public int Index
        {
            get => _index;
            set
            {
                if(_index != value)
                {
                    _index = value;
                    OnPropertyChanged("Index");
                }
            }
        }

        public AlgorithmOptionPanel(IAlgorithmOptionElement algorithmOption)
        {
            AlgorithmOption = algorithmOption;
        }
    }
}
