using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace Snake2
{
    class GameEntity
    {
        public UIElement UIElement { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
