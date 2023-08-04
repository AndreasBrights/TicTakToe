using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTakToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        private MarkType[] mResults;
        // Hold current cell result

        // true if player ones turn and false if player 2 turn.
        private bool mPlayerOneTurn;

        private bool mGameOver;
        #endregion


        #region Constuctor

        public MainWindow()
        {
            InitializeComponent();

            NewGame();

        }
        #endregion

        #region NewGame
        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = new MarkType(); // Array of "free"

            mPlayerOneTurn = true; // Sett playerOne

            Container.Children.Cast<Button>()
                .ToList()
                .ForEach(b =>
                {
                    b.Content = string.Empty;
                    b.Background = Brushes.White;
                    b.Foreground = Brushes.Blue;
                });

            mGameOver = false;

        }
        #endregion
        private void Button_Click(object sender, RoutedEventArgs e) // Sender is button / e is event
        {
            if (mGameOver)
            {
                NewGame();
                return;
            }
            // cast the sender to a button
            var button = (Button)sender;

            // Find the buttons in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);


            var index = column + (row * 3);

            if (mResults[index] != MarkType.Free)
                return;

            // sett the cell value based on which players turn it is
            mResults[index] = mPlayerOneTurn ? MarkType.Nought : MarkType.Cross;

            button.Content = mPlayerOneTurn ? "O" : "X";

            if (!mPlayerOneTurn)
                button.Foreground = Brushes.Red;

            // Invert operator "^=" switches between true and false
            mPlayerOneTurn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            // & Only keeps the 0-1-2 if all are the same as 0; Check to see if all are the same value.
            #region Horizontal Wins
            var same = (mResults[0] & mResults[1] & mResults[2]) == mResults[0];
            //horizontial
            if (mResults[0] != MarkType.Free && same)
            {
                mGameOver = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            same = (mResults[3] & mResults[4] & mResults[5]) == mResults[3];
            if (mResults[3] != MarkType.Free && same)
            {
                mGameOver = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            same = (mResults[6] & mResults[7] & mResults[8]) == mResults[6];
            if (mResults[6] != MarkType.Free && same)
            {
                mGameOver = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion
            #region Vertical Wins
            //vertical Win
            same = (mResults[0] & mResults[3] & mResults[6]) == mResults[0];
            if (mResults[0] != MarkType.Free && same)
            {
                mGameOver = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            same = (mResults[1] & mResults[4] & mResults[7]) == mResults[1];
            if (mResults[1] != MarkType.Free && same)
            {
                mGameOver = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            same = (mResults[2] & mResults[5] & mResults[8]) == mResults[2];
            if (mResults[2] != MarkType.Free && same)
            {
                mGameOver = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion
            #region Diagonal Wins
            //Diagonal WIn

            same = (mResults[0] & mResults[4] & mResults[8]) == mResults[0];
            if (mResults[0] != MarkType.Free && same)
            {
                mGameOver = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            same = (mResults[2] & mResults[4] & mResults[6]) == mResults[2];
            if (mResults[2] != MarkType.Free && same)
            {
                mGameOver = true;
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
            }
            #endregion

            if (!mResults.Any(r => r == MarkType.Free))
            {
                mGameOver = true;
                Container.Children.Cast<Button>()
                    .ToList()
                    .ForEach(b =>
                    {
                        b.Foreground = Brushes.Orange;
                    });

                NewGame();

            }
        }
    }
}
