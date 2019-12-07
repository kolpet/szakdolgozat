﻿using System;
using Szakdolgozat.Common;

namespace Szakdolgozat.Persistence.Structures
{
    [Serializable]
    public class UnitSave
    {
        public int Id;

        public string Name;

        public MarriageGroup Group;
    }
}
