using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class LogData : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MetaData { get; set; }
    }
}
