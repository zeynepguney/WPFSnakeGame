using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;

namespace Snake2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        private GameArea gameArea;
        int foods, score, level;
        protected override void OnContentRendered(EventArgs e)
        {
            gameArea = new GameArea(this);
            InitializeScore();
            base.OnContentRendered(e);
        }
        private void InitializeScore()
        {
            foods = 0;
            score = level = 1;
        }
        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(gameArea != null)
            {
                switch(e.Key)
                {
                    case Key.W:
                        gameArea.HareketYonunuGuncelle(GidisYonu.Up);
                        break;
                    case Key.A:
                        gameArea.HareketYonunuGuncelle(GidisYonu.Left);
                        break;
                    case Key.S:
                        gameArea.HareketYonunuGuncelle(GidisYonu.Down);
                        break;
                    case Key.D:
                        gameArea.HareketYonunuGuncelle(GidisYonu.Right);
                        break;
                    case Key.Space:
                        PauseGame();
                        break;
                }
            }
        }
        internal void GameOver()
        {
            gameArea.StopGame();
            MessageBox.Show($"{level}. seviyeye ulaştınız. Skorunuz : {score}.");
        }
        private void YenidenDeneClick(object sender, RoutedEventArgs e)
        {
            gameArea.StopGame();
            gameArea = new GameArea(this);
            GameArea.Children.Clear();
            if(!gameArea.CalismaDurumu)
            {
                BaslatButonu.IsEnabled = false;
            }
        }
        private void PauseGame()
        {
            gameArea.PauseGame();
            MessageBox.Show("Oyun duraklatıldı!");
            gameArea.ContinueGame();
        }
        private void BaslatClick(object sender, RoutedEventArgs e)
        {
            if(!gameArea.CalismaDurumu)
            {
                BaslatButonu.IsEnabled = false;
            }
        }
        internal void SkoreArtıs()
        {
            foods += 1;
            if (foods % 3 == 0)
                level += 1;
            SkorGuncelle();
        }

        private void SkorGuncelle()
        {
            ScoreLbl.Content = $"Score:{score}";
        }            
    }
}
