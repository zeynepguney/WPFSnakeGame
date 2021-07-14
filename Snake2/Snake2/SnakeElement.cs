using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Snake2
{
    class SnakeElement : GameEntity
    {
        public SnakeElement(int size)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = size;
            rectangle.Height = size;
            rectangle.Fill = Brushes.BlueViolet;
            UIElement = rectangle;
        }
        public bool Head { get; set; }
    }
}
