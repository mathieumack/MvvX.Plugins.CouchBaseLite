using System;
using System.Collections.Generic;
using System.Text;

namespace MvvX.Plugins.CouchBaseLite
{
    [AttributeUsage(AttributeTargets.Class)]
    class PreserveAttribute : Attribute
    {
        public PreserveAttribute() { }
        public bool AllMembers { get; set; }
        public bool Conditional { get; set; }
    }
}
