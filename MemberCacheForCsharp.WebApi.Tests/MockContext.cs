using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.WebApi.Tests
{
   public class MockContext
    {
        private IDisposable disposable;

        public string Address
        {
            get; set;
        }

        public virtual void Init()
        {
            if (Address != null)
                disposable = WebApp.Start<Startup>(Address);

        }

        public virtual void Distory()
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
