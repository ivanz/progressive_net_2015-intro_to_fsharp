using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsWebApiExample.Utilities
{
    public class Unit
    {
        private Unit()
        {
        }

        public static Unit Instance = new Unit();
    }

}
