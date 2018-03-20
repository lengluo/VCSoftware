using System;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Util
{
    public class Log
    {
        [Fact]
        public void Trace()
        {
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
            VCUtil.Logger.Trace("woca");
        }
    }
}
