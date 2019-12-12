using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Structures;
using Szakdolgozat.ViewModel.Events;

namespace Szakdolgozat.ViewModel.Windows
{
    public class ResultViewModel : ViewModelBase
    {
        private ResultModel _model;

        private IContext _context;

        public List<StablePairPanel> StablePairPanels { get; private set; }

        public string Name { get => _model.Name; }

        public ResultViewModel(NewResultWindowEventArgs e)
        {
            _model = e.Model;
            _context = e.Context;

            int result = e.Result;

            StablePairPanels = new List<StablePairPanel>();
            Solution solution = _model.Solution;
            foreach(Tuple<int, int> pair in solution)
            {
                string firstName = _context.GetParticipants[pair.Item1].Name;
                string secondName = _context.GetParticipants[pair.Item2].Name;
                StablePairPanel panel = new StablePairPanel
                {
                    FirstId = pair.Item1,
                    SecondId = pair.Item2,
                    WrittenPair = (firstName != "" && secondName != "") ? firstName + " - " + secondName : "",
                    Color = new SolidColorBrush(_model.IsStablePair(pair) ? Colors.LightGreen : Colors.OrangeRed),
                    Statistics = "Boldogság: " + _model.GetGroupHappiness(pair) + ", Egyenlő boldogság: " + _model.GetEgalitarianGroupHappiness(pair)

                };
                StablePairPanels.Add(panel);
            }
            StablePairPanels.OrderBy(x => x.FirstId);

            OnPropertyChanged("Name");
            OnPropertyChanged("StablePairPanels");
        }
    }
}
