using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempGameClasses
{
    public interface ICombat
    {
        /// <summary>
        /// attacks an Actor
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        bool Attack(Actor act);


    }
}
