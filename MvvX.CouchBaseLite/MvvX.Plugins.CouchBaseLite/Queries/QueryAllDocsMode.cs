using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public enum QueryAllDocsMode
    {
        AllDocs = 0,
        IncludeDeleted = 1,
        ShowConflicts = 2,
        OnlyConflicts = 3,
        BySequence = 4
    }
}
