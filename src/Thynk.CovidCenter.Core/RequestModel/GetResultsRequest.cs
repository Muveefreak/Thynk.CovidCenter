using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class GetResultsRequest
    {
        public Guid LocationId { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
}
