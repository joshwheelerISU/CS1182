using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempGameClasses
{
    public interface IRepeatable<T>
    {
        /// <summary>
        /// copies an item of type T
        /// </summary>
        /// <returns></returns>
        T CreateCopy();

    }
}
