using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Common
{
    public interface IPriorities
    {
        Dictionary<int, List<int>> GetDictionary();
    }
}
