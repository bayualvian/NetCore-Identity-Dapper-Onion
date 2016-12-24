using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Core.Entities.ConfigManager
{
    public class ConfigEntity
    {
        public ConnectionStringsEntity ConnectionStrings { get; set; }
        public LoggingEntity Logging { get; set; }
    }
}
