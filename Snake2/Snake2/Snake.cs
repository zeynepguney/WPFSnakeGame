using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake2
{
    class Snake
    {
        private readonly int elementsize;
        public Snake(int size)
        {
            Elements = new List<SnakeElement>();
            elementsize = size;
        }
        public SnakeElement TailBackUp { get; set; }
        public List<SnakeElement> Elements { get; set; }
        public GidisYonu GidisYonu { get; set; }
        public SnakeElement Head => Elements.Any() ? Elements[0] : null;
        internal void HareketYonunuGuncelle(GidisYonu up)
        {
            switch (up)
            {
                case GidisYonu.Up:
                    if (GidisYonu != GidisYonu.Down)
                        GidisYonu = GidisYonu.Up;
                    break;
                case GidisYonu.Left:
                    if (GidisYonu != GidisYonu.Right)
                        GidisYonu = GidisYonu.Left;
                    break;
                case GidisYonu.Down:
                    if (GidisYonu != GidisYonu.Up)
                        GidisYonu = GidisYonu.Down;
                    break;
                case GidisYonu.Right:
                    if (GidisYonu != GidisYonu.Left)
                        GidisYonu = GidisYonu.Right;
                    break;
            }
        }
        internal void Grow()
        {
            Elements.Add(new SnakeElement(elementsize) { X = TailBackUp.X, Y = TailBackUp.Y });
        }
        public bool KuyrugunaCarpma()
        {
            SnakeElement snakeHead = Head;
            if (snakeHead != null)
            {
                foreach (var snakeElement in Elements)
                {
                    if (!snakeElement.Head)
                    {
                        if (snakeElement.X == snakeHead.X && snakeElement.Y == snakeHead.Y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        internal void PositionFirstElement(int sutun, int satır, GidisYonu baslangicYonu)
        {
            Elements.Add(new SnakeElement(elementsize)
            {

                X = (sutun / 2) * elementsize,
                Y = (satır / 2) * elementsize,
                Head = true
            });
            GidisYonu = baslangicYonu;
        }
        internal void MoveSnake()
        {
            SnakeElement bas = Elements[0];
            SnakeElement kuyruk = Elements[Elements.Count - 1];
            TailBackUp = new SnakeElement(elementsize)
            {
                X = kuyruk.X,
                Y = kuyruk.Y
            };
            bas.Head = false;
            bas.Head = true;
            kuyruk.X = bas.X;
            kuyruk.Y = bas.Y;
            switch (GidisYonu)
            {
                case GidisYonu.Right:
                    kuyruk.X += elementsize;
                    break;
                case GidisYonu.Left:
                    kuyruk.X -= elementsize;
                    break;
                case GidisYonu.Up:
                    kuyruk.Y -= elementsize;
                    break;
                case GidisYonu.Down:
                    kuyruk.Y += elementsize;
                    break;
                default:
                    break;
            }
            Elements.RemoveAt(Elements.Count - 1);
            Elements.Insert(0, kuyruk);
        }
    }
    enum GidisYonu
    {
        Right,
        Left,
        Up,
        Down
    }
}
