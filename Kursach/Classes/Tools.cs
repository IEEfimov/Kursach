using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Classes
{
    public class Tools
    {
        public  const int NULL = 0;
        public  const int PEN = 1;
        public  const int TEXT = 2;
        public  const int LINE = 3;
        public  const int ELLIPSE = 4;
        public  const int ZALIVKA = 5;
        public const int MASS = 6;


        private int current = 0;
        
        public int Value
        {
            get { return current; }
            set
            {
                current = value;
                if (current > 6 || current < 0) current = 0;
            }
        }
    }
}
