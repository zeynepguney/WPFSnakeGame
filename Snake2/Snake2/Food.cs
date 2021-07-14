using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake2
{
    class Food : GameEntity
    {
        public Food(int size)
        {
            Rectangle rectangle = new Rectangle() {
                Width = size,
                Height = size,
                Fill = Brushes.CornflowerBlue,
                RadiusX = 10,
                RadiusY = 10
            }; 
            UIElement = rectangle;
        }
        public override bool Equals(object obj)
        {
            Food food = obj as Food;
            if(food != null)
            {
                return X == food.X && Y == food.Y;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
    }
}
