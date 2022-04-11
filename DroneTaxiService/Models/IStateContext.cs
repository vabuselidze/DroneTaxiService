using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Models
{
    interface IStateContext
    {
        public List<string> GetStateInfo();

        // To write logs in .txt file or DB;  It's not required
        public void MakeLogs();
                
    }
}
