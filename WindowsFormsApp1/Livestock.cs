using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    abstract class Livestock
    {
        protected internal decimal TotalCost
        { get; set; }

        protected internal decimal TotalWeight
        { get; set; }
        protected internal string Name
        { get; set; }

        protected internal decimal Age
        { get; set; }

        protected internal decimal Cost
        { get; set; }

        protected internal decimal Weight
        { get; set; }

        protected internal string Color
        { get; set; }

        protected internal string Notes
        { get; set; }
    }


}


