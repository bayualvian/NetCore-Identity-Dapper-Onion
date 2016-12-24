using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Core.Entities.ConfigManager
{
    public class LoggingEntity
    {
        public bool IncludeScopes { get; set; }
        public LoggingLogLevelEntity LogLevel { get; set; }
    }
}
