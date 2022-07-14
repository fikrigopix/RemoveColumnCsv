using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveColumnCsv.Model
{
    public class UserInput
    {
        public string PathCsvInput { get; set; }
        public string PathCsvOutput { get; set; }
        public int IndexWantToBeRemoved { get; set; }
    }
}
