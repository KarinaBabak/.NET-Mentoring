using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public interface IFileSystemElement
    {
        string Path { get; }

        IEnumerable<string> Accept(IVisitor visitor);
    }
}
