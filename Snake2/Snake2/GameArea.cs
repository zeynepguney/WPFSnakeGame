using System;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
namespace Snake2
{
    class GameArea
    {
        private MainWindow mainWindow;
        private int boyut = 10;
        public int Boyut
        {
            get { return boyut; }
            set { boyut = value; }
        }
        public int SutunSayisi { get; private set; }
        public int SatirSayisi { get; private set; }
        public double GameWidth { get; private set; }
        public double GameHeight { get; private set; }
        Random rnd;
        public Food Food { get;  set; }
        public Snake Snake { get; set; }
        DispatcherTimer gameTimer;
        public bool CalismaDurumu { get; set; }
        public static double ActualWidth { get; private set; }
        public static double ActualHeight { get; private set; }

        public GameArea(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            rnd = new Random(DateTime.Now.Millisecond / DateTime.Now.Second);
        }
        public void InitializeGame(int boyut)
        {
            Boyut = boyut;
            GameWidth = mainWindow.GameArea.ActualWidth;
            GameHeight = mainWindow.GameArea.ActualHeight;
            SutunSayisi = (int)GameWidth / Boyut;
            SatirSayisi = (int)GameHeight / Boyut;

            DrawGameArea();
            InitializeSnake();
            InitializeTimer();
            CalismaDurumu = true;
        }
        private void InitializeTimer()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromSeconds(0.1);
            gameTimer.Tick += MainGameLoop;
            gameTimer.Start();
        }
        private void InitializeSnake()
        {
            Snake = new Snake(Boyut);
            Snake.PositionFirstElement(SutunSayisi, SatirSayisi, GidisYonu.Right);
        }
        private void MainGameLoop(object sender, EventArgs e)
        {
            Snake.MoveSnake();
            CheckCollision();
            CreateFood();
            Draw();
        }
        private void Draw()
        {
            DrawSnake();
            DrawFood();
        }
        private void DrawGameArea()
        {
            int i = 0;
            for (; i < SatirSayisi; i++)
                mainWindow.GameArea.Children.Add(GenerateHorizontalAreaLine(i));
            int j = 0;
            for (; j < SutunSayisi; j++)
                mainWindow.GameArea.Children.Add(GenerateHorizontalAreaLine(j));
            mainWindow.GameArea.Children.Add(GenerateHorizontalAreaLine(j));
            mainWindow.GameArea.Children.Add(GenerateHorizontalAreaLine(i));
        }
        private void DrawSnake()
        {
            foreach (var snakeElement in Snake.Elements)
            {
                if (!mainWindow.GameArea.Children.Contains(snakeElement.UIElement))
                    mainWindow.GameArea.Children.Add(snakeElement.UIElement);

                Canvas.SetLeft(snakeElement.UIElement, snakeElement.X);
                Canvas.SetTop(snakeElement.UIElement, snakeElement.Y);
            }
        }
        private void DrawFood()
        {
            if (!mainWindow.GameArea.Children.Contains(Food.UIElement))
                mainWindow.GameArea.Children.Add(Food.UIElement);
            Canvas.SetLeft(Food.UIElement, Food.X + 2);
            Canvas.SetTop(Food.UIElement, Food.Y + 2);
        }
        private Line GenerateVerticalAreaLine(int j)
        {
            return new Line
            {
                Stroke = Brushes.Black,
                X1 = j * Boyut,
                Y1 = 0,
                X2 = j * Boyut,
                Y2 = Boyut * SatirSayisi
            };
        }
        private Line GenerateHorizontalAreaLine(int i)
        {
            return new Line
            {
                Stroke = Brushes.Black,
                X1 = 0,
                Y1 = i * Boyut,
                X2 = Boyut * SutunSayisi,
                Y2 = i * Boyut
            };
        }
        private void CheckCollision()
        {
            if (CollisionWithFood())
                EatFood();
            if(Snake.KuyrugunaCarpma() || CollisionWithAreaBounds())
            {
                mainWindow.GameOver();
                StopGame();
            }

                
        }
        private bool CollisionWithFood()
        {
            if (Food == null || Snake == null)
            {
                SnakeElement head = Snake.Head;
                return (head.X == Food.X && head.Y == Food.Y);
            }

            return false;
        }
        private void EatFood()
        {
            mainWindow.SkoreArtıs();
            mainWindow.GameArea.Children.Remove(Food.UIElement);
            Food = null;
            Snake.Grow();
            IncreaseGameSpeed();
        }

        private void CreateFood()
        {
            if (Food != null)
                return;
            Food = new Food(Boyut)
            {
                X = rnd.Next(0, SutunSayisi) * Boyut,
                Y = rnd.Next(0, SatirSayisi) * Boyut
            };
        }
        private bool CollisionWithAreaBounds()
        {
            if (Snake == null || Snake.Head == null)
                return false;
            var snakeHead = Snake.Head;
            return (snakeHead.X > GameWidth - Boyut ||
                snakeHead.Y > GameHeight - Boyut ||
                snakeHead.X < 0 || snakeHead.Y < 0);
        }
        public void StopGame()
        {
            gameTimer.Stop();
            gameTimer.Tick -= MainGameLoop;
            CalismaDurumu = false;
        }
        private void IncreaseGameSpeed()
        {
            var hiz = gameTimer.Interval.Ticks / 10;
            gameTimer.Interval = TimeSpan.FromTicks(gameTimer.Interval.Ticks - hiz);
        }
        public void PauseGame()
        {
            gameTimer.Stop();
            CalismaDurumu = false;
        }
        public void ContinueGame()
        {
            gameTimer.Start();
            CalismaDurumu = true;
        }
        internal void HareketYonunuGuncelle(GidisYonu gidisYonu)
        {
            if (Snake != null)
                Snake.HareketYonunuGuncelle(gidisYonu);
        }
    }
    
}
