using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Szakdolgozat.ViewModel.Controls
{
    public class StablePairPanel
    {
        public int FirstId { get; set; }

        public int SecondId { get; set; }

        public string WrittenPair { get; set; }

        public Brush Color { get; set; }

        public string Statistics { get; set; }
    }
}
