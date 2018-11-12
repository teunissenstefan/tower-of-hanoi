using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfHanoi
{
    public class Disc
    {
        public int number;
        public int size;
        public ConsoleColor color;

        public Disc(int _number, int _size, ConsoleColor _color)
        {
            number = _number;
            size = _size;
            color = _color;
        }
    }
}
