using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesWatcher
{
    public interface ICustomWatcher
    {
        void SubscribeToChanges();
        void UnSubscribeOnChanges();
    }
}
