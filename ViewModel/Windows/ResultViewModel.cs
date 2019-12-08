using System;
using System.Collections.Generic;
using System.Windows.Media;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Windows
{
    public class ResultViewModel : ViewModelBase
    {
        private ResultModel _model;

        public List<StablePairPanel> StablePairPanels { get; private set; }

        public string Name { get => _model.Name; }

        public ResultViewModel(int result)
        {
            _model = new ResultModel(result);

            StablePairPanels = new List<StablePairPanel>();
            Solution solution = _model.Solution;
            foreach(Tuple<int, int> pair in solution)
            {
                string firstName = _model.GetContext.Participants[pair.Item1].Name;
                string secondName = _model.GetContext.Participants[pair.Item2].Name;
                StablePairPanel panel = new StablePairPanel
                {
                    FirstId = pair.Item1,
                    SecondId = pair.Item2,
                    WrittenPair = (firstName != "" && secondName != "") ? firstName + " - " + secondName : "",
                    Color = new SolidColorBrush(_model.IsStablePair(pair) ? Colors.LightGreen : Colors.Orange),
                    Statistics = "Boldogság: " + _model.GetGroupHappiness(pair) + ", Egyenlő boldogság: " + _model.GetEgalitarianGroupHappiness(pair)

                };
                StablePairPanels.Add(panel);
            }
            OnPropertyChanged("Name");
            OnPropertyChanged("StablePairPanels");
        }
    }
}
