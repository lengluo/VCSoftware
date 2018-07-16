using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Cache
{
    public class ChangeToken : IChangeToken
    {
        public bool HasChanged => DateTime.Now.Second % 2 == 0;

        public bool ActiveChangeCallbacks => false;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            throw new NotImplementedException();
        }
    }

    public class ChangeTokenCallback : IChangeToken
    {

        public bool HasChanged => true;

        public bool ActiveChangeCallbacks => true;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            callback.Invoke(3);
            return null;
        }
    }
}
