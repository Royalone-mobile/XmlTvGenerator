﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlTvGenerator.Core
{
    public abstract class GrabberBase
    {
        public abstract List<Show> Grab(string xmlParameters);
    }
}
