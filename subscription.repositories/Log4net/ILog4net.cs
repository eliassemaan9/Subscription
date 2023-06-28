using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace subscription.repositories.Log4net
{
   public interface ILog4net
    {
        void Warn(object message);
        void Info(object message);
        void Debug(object message);
        void Error(object message);
    }
}
