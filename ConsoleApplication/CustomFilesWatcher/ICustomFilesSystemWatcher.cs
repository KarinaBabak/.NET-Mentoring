using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFilesWatcher
{
    public interface ICustomWatcher
    {
        void SubscribeToChanges();
        void UnSubscribeOnChanges();
    }
}
