using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BubbleSortVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static int numberOfElements=13;  // starting number of elements to sort
        ListOfRectangle myList = new ListOfRectangle(numberOfElements); // create list which include all elements to sort

        DispatcherTimer timer1 = new DispatcherTimer(); // for show/hide settingsGrid
        int Start = 0; // 0-no operation, 1- show, 2- hide  (for settingsGrid)


        DispatcherTimer sortTimer = new DispatcherTimer();  // for sorting algorithm ( SortFunction() )


        // for sorting algorithm
        bool sortCompleate = false;
        int numberOfRevers = 0;
        int actualyElements = 0;
        MainRectangle[] reversalArea = new MainRectangle[2];
        int greenElement = 1;


        public MainWindow()
        {
            InitializeComponent();
            

            ArrangeElements();
            #region Settings grid timer
                timer1.Interval = TimeSpan.FromMilliseconds(10);
                timer1.Tick += ShowOrHideSettings;
            #endregion

            #region Sorting timer
                sortTimer.Interval = TimeSpan.FromMilliseconds(1000);
                sortTimer.Tick += SortFunction;
            #endregion



        }



       



        public void Mix()
        {
            int rndValue;

            Random rnd = new Random();
            for (int i = 0; i < numberOfElements; i++)
            {
                rndValue = rnd.Next(10, 400);
                myList.ListaObiektow[i].Height = rndValue;
            }
            
        } // this function set random values for element's height

        public void ArrangeElements()
        {
            DisplayArea.Children.Clear();
            for (int i = 0; i < numberOfElements; i++)
            {
                myList.ListaObiektow[i].Margin = new Thickness((myList.ListaObiektow[i].Width +5) * i, 0, 0, 0);
                DisplayArea.Children.Add(myList.ListaObiektow[i]);
            }
        }    // this funcion set Margin to correct display all elements

        

        public void SortFunctionEvent()
        {
            
            sortTimer.Start();
        }

        public void SortFunction(object sender, EventArgs e)
        {

            // this instruction checking whether sort algorithm doesn't compare green objects
            if (myList.ListaObiektow[actualyElements].Background == Brushes.LightGreen)
            {
                myList.ListaObiektow[actualyElements - 1].Background = Brushes.LightGreen;
                greenElement++;
                sortCompleate = true;
                actualyElements = 0;
            }
            // the end of the checking all objects
            if (actualyElements == numberOfElements - 1)
            {
                if(myList.ListaObiektow[(numberOfElements - (greenElement-1)) -1].Background != Brushes.LightGreen)
                    myList.ListaObiektow[(numberOfElements - (greenElement-1)) -1].Background = Brushes.WhiteSmoke;

                myList.ListaObiektow[numberOfElements-greenElement].Background = Brushes.LightGreen;
                myList.ListaObiektow[(numberOfElements - greenElement)-1].Background = Brushes.WhiteSmoke;
                greenElement++;
                sortCompleate = true;
                actualyElements = 0;
            }

            // if sorting is compleate
            if (numberOfRevers == 0 && sortCompleate == true)
            {
                sortCompleate = false;
                greenElement = 0;

                for (int i = 0; i < numberOfElements; i++)
                {
                    myList.ListaObiektow[i].Background = Brushes.LawnGreen;
                }

                sortTimer.Stop();
            }

            // if sorting isn't compleate
            else if(numberOfRevers != 0 && sortCompleate == true)
            {
                numberOfRevers = 0;
                sortCompleate = false;
            }


            //kolejny krok sortowania
            if (sortCompleate==false)
            {
                //reset color from blue
                if (actualyElements - 1 >= 0 && myList.ListaObiektow[actualyElements - 1].Background != Brushes.LightGreen)
                {
                    myList.ListaObiektow[actualyElements - 1].Background = Brushes.WhiteSmoke;  
                }

                // load elements to compare
                reversalArea[0] = myList.ListaObiektow[actualyElements];
                reversalArea[1] = myList.ListaObiektow[actualyElements+1];


                

                // if firs element is higher than second
                if (reversalArea[0].Height > reversalArea[1].Height)
                {
                    myList.ListaObiektow[actualyElements] = reversalArea[1];
                    myList.ListaObiektow[actualyElements+1] = reversalArea[0];
   
                    ArrangeElements();
                    numberOfRevers++;
                }

                // set colot for current element
                if ((myList.ListaObiektow[actualyElements].Background != Brushes.LightGreen) && (myList.ListaObiektow[actualyElements].Background != Brushes.LawnGreen))
                    myList.ListaObiektow[actualyElements].Background = Brushes.Blue;
                if ((myList.ListaObiektow[actualyElements + 1].Background != Brushes.LightGreen) && (myList.ListaObiektow[actualyElements].Background != Brushes.LawnGreen))
                    myList.ListaObiektow[actualyElements + 1].Background = Brushes.CadetBlue;

                actualyElements++; 
            }  
        }


        public void StopSorting()
        {
            
            actualyElements = 0;
            sortCompleate = false;
            numberOfRevers = 0;
            sortTimer.Stop();
        }

        // there is all settings for this application's Custom Window
        #region WindowSettings

        #region IconsActions

        // CURSOR CHANGE

        private void close_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            
        }

        private void maximize_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void minimize_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void minimize_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void maximize_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void close_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }


        //OnClick Actions

        private void close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(1);
        }

        private void maximize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }






        #endregion


        // Drag window
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        #endregion 


        //there is settings for SORT and MIX "pseudo" buttons (Border objects)
        #region Main Buttons Settings


        #region Cursor Change
        private void mix_borderClick_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void mix_borderClick_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void sort_borderClick_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void sort_borderClick_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }



        #endregion

        #region OnClick

        private void mix_borderClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            

            StopSorting();

            for (int i = 0; i < numberOfElements; i++)
            {
                myList.ListaObiektow[i].Background = Brushes.WhiteSmoke;
            }

            Mix();
        }  // MIX ELEMENTS

        private void sort_borderClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)  // SORT FUNCTION IS THERE
        {

            
            StopSorting();
            greenElement = 1;
            for (int i = 0; i < numberOfElements; i++)
            {
                myList.ListaObiektow[i].Background = Brushes.WhiteSmoke;
            }
            SortFunctionEvent();


        }

        #endregion

        // Resize
        public void Timer1Function(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(10);
            timer1.Tick += MainButtonSetSize;
            timer1.Start();
        }

        public void MainButtonSetSize(object sender, EventArgs e)
        {

            try
            {
                double gridWidth = this.Width;
                sort_borderClick.Width = gridWidth / 2;
                mix_borderClick.Width = gridWidth / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }


        #endregion

        
        //there is all functions for SETTINGS Grid
        #region All for SETTINGS

        // Reaction to mause on settingsGrid
        private void settings_grid_MouseEnter(object sender, MouseEventArgs e)
        {
            settings_grid.Background = Brushes.OrangeRed;
            settings_grid.Opacity = 0.8;
        }

        private void settings_grid_MouseLeave(object sender, MouseEventArgs e)
        {
            settings_grid.Background = null;
            settings_grid.Opacity = 1;
        }

        private void settings_grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StopSorting();
            if (settings_area.Opacity == 0)
            {
                sort_borderClick.IsEnabled = false;
                mix_borderClick.IsEnabled = false;
                settings_area.Visibility = Visibility.Visible;
            }
            if (settings_area.Opacity == 1)
            {
                sort_borderClick.IsEnabled = true;
                mix_borderClick.IsEnabled = true;
                
            }
            ShowOrHideSettingsEvent();
            Start = 0;


        }

        // Show/hide settings
        public void ShowOrHideSettingsEvent()
        {


            timer1.Start();

        }
        public void ShowOrHideSettings(object sender, EventArgs e)
        {

            if (Start == 0)
            {
                if (settings_area.Opacity == 0)
                    Start = 1;
                else
                    Start = 2;
            }
            if (Start == 1)
            {

                settings_area.Opacity += 0.05;
                if (settings_area.Opacity >= 1)
                {
                    settings_area.Opacity = 1;

                    Start = 11;
                    timer1.Stop();
                }
            }
            if (Start == 2)
            {
                settings_area.Opacity -= 0.05;
                if (settings_area.Opacity <= 0)
                {
                    settings_area.Opacity = 0;
                    settings_area.Visibility = Visibility.Hidden;
                    Start = 11;
                    timer1.Stop();

                }
            }

        }

        // APPLY
        private void apply_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(numberOfElements_Text.Text) <= 100 && Convert.ToInt32(numberOfElements_Text.Text) > 1)
                {
                    label3.Foreground = Brushes.Black;
                    numberOfElements = Convert.ToInt32(numberOfElements_Text.Text);
                    myList.NewElements(numberOfElements);
                    ArrangeElements();
                   
                }
                else
                    label3.Foreground = Brushes.Red;

            }
            catch(Exception ex)
            {
                label3.Foreground = Brushes.Red;
                
            }



            #region Set speed for sort
            try
            {
                switch (Convert.ToInt32(sortSpeed_Text.Text))
                {
                    case 1:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(1000);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 2:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(500);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 3:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(250);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 4:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(100);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 5:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(50);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 6:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(15);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 7:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(1);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 8:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(0.5);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 9:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(0.1);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    case 10:
                        sortTimer.Interval = TimeSpan.FromMilliseconds(0.01);
                        label3_Copy.Foreground = Brushes.Black;
                        break;
                    default:
                        label3_Copy.Foreground = Brushes.Red;
                        break;
                }
            }
            catch (Exception ex)
            {
                label3_Copy.Foreground = Brushes.Red;
            }
            
            if(label3.Foreground== Brushes.Black && label3_Copy.Foreground == Brushes.Black)
            {
                sort_borderClick.IsEnabled = true;
                mix_borderClick.IsEnabled = true;
                ShowOrHideSettingsEvent();
                Start = 0;
            }
            

            #endregion

        }

        // Close settings (cross image)
        private void image_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sort_borderClick.IsEnabled = true;
            mix_borderClick.IsEnabled = true;
            ShowOrHideSettingsEvent();
            Start = 0;
        }

        #endregion




    }


    // CLASSES // // // // // // // // // // // // //

    public class MainRectangle: Border
    {
        

        public MainRectangle()
        {
            Background = Brushes.WhiteSmoke;
            Width = 40;
            Height = 100; // max 400
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Bottom;
            Margin = new Thickness(12, 0, 0, 0); 
        }
        public MainRectangle(int numberOfElements)
        {
            Background = Brushes.WhiteSmoke;
            Width = (600/ numberOfElements)-5;
            Height = 100; // max 400
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Bottom;
            Margin = new Thickness(12, 0, 0, 0);
        }
    }

    public class ListOfRectangle
    {
        public MainRectangle[] ListaObiektow;
        

        public ListOfRectangle(int numberOfElements)
        {
            ListaObiektow = new MainRectangle[numberOfElements];

            for (int i = 0; i < numberOfElements; i++)
            {
                ListaObiektow[i] = new MainRectangle();
                
            }

        }
        public void NewElements(int NewNumberOfElements)
        {
            this.ListaObiektow = new MainRectangle[NewNumberOfElements];
            for (int i = 0; i < NewNumberOfElements; i++)
            {
                this.ListaObiektow[i] = new MainRectangle(NewNumberOfElements);
            }
            
        }
    }
}
