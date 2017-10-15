using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public class Directory : IFileSystemElement
    {
        private string path;

        public string Path
        {
            get
            {
                return this.path;
            }
        }

        #region ctors
        public Directory()
        {
            this.path = "C:/";
        }

        public Directory(string path)
        {
            this.path = path;
        }
        #endregion

        /// <summary>
        /// call visitor to start file searching
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public IEnumerable<string> Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
