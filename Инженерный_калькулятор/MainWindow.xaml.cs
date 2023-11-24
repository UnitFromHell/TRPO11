using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Инженерный_калькулятор
{
    /// <summary>
    /// Этот класс реализует фукции калькулятора
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public string D; // Переменная для хранения значения операции
        public string N1; // Переменная для хранения первого числа
        public bool N2 = false; // Флаг для отслеживания состояния операции
        public int a = 0; // Счетчик для кнопки "доп кнопки"
        public int l = 0; // Переменная для хранения значения
        public double b; // Переменная для хранения значения
        public int z = 0; // Переменная для хранения значения
        public bool inputstatus; // Флаг для отслеживания состояния ввода
        public bool k = false; // Флаг для отслеживания состояния

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (N2 == true)
        //    {
        //        N2 = false;
        //        textBox1.Text = "0";
        //    }
        //    Button B = (Button)sender;
        //    if (textBox1.Text == "0")
        //    {
        //        textBox1.Text ="" + B.Content;
        //    }
        //    else
        //    {
        //        textBox1.Text = textBox1.Text + B.Content;
        //    }
        //    if (N2 == true)
        //    {
        //        N2 = false;
        //        textBox2.Text = "0";
        //    }
        //    Button D = (Button)sender;
        //    if (textBox2.Text == "0")
        //    {
        //        textBox2.Text = "" + B.Content;
        //    }
        //    else
        //    {
        //        textBox2.Text = textBox2.Text + D.Content;
        //    }
        //}
        //private void Button_Click_can(object sender, RoutedEventArgs e)
        //{
        //    Button B = (Button)sender;
        //    D ="" + B.Content;
        //    N1 = textBox1.Text;
        //    N2 = true;
        //    Button E = (Button)sender;
        //    D = "" + E.Content;
        //    N1 = textBox2.Text;
        //    N2 = true;
        //}
        private void UpdateTextBox(Button button, TextBox textBox)
        {
            if (N2 == true)
            {
                N2 = false;
                textBox.Text = "0";
            }
            if (textBox.Text == "0")
            {
                textBox.Text = "" + button.Content;
            }
            else
            {
                textBox.Text = textBox.Text + button.Content;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            UpdateTextBox(button, textBox1);
            UpdateTextBox(button, textBox2);
        }
        
        
        private void Button_Click_can(object sender, RoutedEventArgs e)
        {
            //Получаем ссылку на объект кнопки, которая вызвала событие
            Button button = (Button)sender;
            //Сохраняем в переменную полученное действие
            D = "" + button.Content;
            //Сохраняем в переменню первое число
            N1 = textBox1.Text;
            //Активируем флаг, указывающий что вводится 2-е число
            N2 = true;
            //Сохраняем в переменную полученное действие
            D = "" + button.Content;
            //Сохраняем в переменную второе число
            N1 = textBox2.Text;
            //Активируем флаг, указывающий что вводится 2-е число
            N2 = true;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
        }



        private void Button_Click_ravno()
        {
            ParseInput(out double x1, out double x2);

            double result = 0;

            if (D == "+")
            {
                IOperation operation = new Addition();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "-")
            {
                IOperation operation = new Subtraction();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "*")
            {
                IOperation operation = new Multiplation();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "/")
            {
                IOperation operation = new Dividation();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "%")
            {
                IOperation operation = new Modulo();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "^")
            {
                IOperation operation = new Power();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "√")
            {
                IOperation operation = new PowerWithFraction();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "log")
            {
                IOperation operation = new Logarithm();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "logb")
            {
                IOperation operation = new LogarithmBase();
                result = operation.Calculate(x1, x2);
            }
            else if (D == "%")
            {
                IOperation operation = new Percentage();
                result = operation.Calculate(x1, x2);
            }

            UpdateOutput(result);
        }
        /// <summary>
        /// 
       /// Метод ParseInput(out double x1, out double x2) отвечает за анализ входных значений из переменной N1 и 
       /// свойства textBox1.Text и присвоение их переменным x1 и x2 соответственно.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        private void ParseInput(out double x1, out double x2)
        {
            try
            {
                //Присваиваем переменной первое число и конвертируем его в double
                x1 = Convert.ToDouble(N1);
                //Присваиваем переменной второе числва и конвертируем его в double
                x2 = Convert.ToDouble(textBox1.Text);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void UpdateOutput(double result)
        {
            textBox1.Text = result.ToString();
        }
        public interface IOperation
        {
            double Calculate(double x1, double x2);
        }

        public class Addition : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return x1 + x2;
            }
        }

        public class Subtraction : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return x1 - x2;
            }
        }
        public class Multiplation : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return x1 * x2;
            }
        }

        public class Dividation : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return x1 / x2;
            }
        }


        public class Modulo : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return x1 % x2;
            }
        }

        public class Power : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return Math.Pow(x1, x2);
            }
        }

        public class PowerWithFraction : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return Math.Pow(x1, 1 / x2);
            }
        }

        public class Logarithm : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return Math.Log(x1, x2);
            }
        }

        public class LogarithmBase : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return Math.Log(x1, x2);
            }
        }

        public class Percentage : IOperation
        {
            public double Calculate(double x1, double x2)
            {
                return x1 * 100 / x2;
            }
        }
        //корень
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Sqrt(x);
            textBox1.Text = result.ToString();
        }
        //квадрат
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Pow(x, 2);
            textBox1.Text = result.ToString();

        }
        //1/х
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = 1/x;
            textBox1.Text = result.ToString();
        }
        //+/-
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = -x;
            textBox1.Text = result.ToString();
        }
        //десятая
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (!textBox1.Text.Contains(","))
            textBox1.Text = textBox1.Text + ",";
        }
        //стерка
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
        }
        //доп кнопки
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (a % 2 == 0)
            {
                invise.Visibility = Visibility.Visible;
                invise2.Visibility = Visibility.Visible;
                invise3.Visibility = Visibility.Visible;
                invise4.Visibility = Visibility.Visible;
                invise5.Visibility = Visibility.Visible;
                invise6.Visibility = Visibility.Visible;
                
            }
            if (a % 2 != 0)
            {
                invise.Visibility = Visibility.Hidden;
                invise2.Visibility = Visibility.Hidden;
                invise3.Visibility = Visibility.Hidden;
                invise4.Visibility = Visibility.Hidden;
                invise5.Visibility = Visibility.Hidden;
                invise6.Visibility = Visibility.Hidden;
                
            }
            a++;
        }
        //Pi
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = 3.1415926535897932384626433832795;
            result = x;
            textBox1.Text = result.ToString();
        }
        //e в степени х
        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            double x, result;
            double e1 = 2.71828182845904;
            x = Convert.ToDouble(textBox1.Text);

            result = Math.Pow(e1, x);
            textBox1.Text = result.ToString();
        }
        
       // 2 в степени x
        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Pow(2, x);
            textBox1.Text = result.ToString();
        }
        //корень степени Y
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
          // double d, c, result;
            
           
          //  if (z % 2 == 0)
          //  {
          //    d = Convert.ToDouble(textBox1.Text);
          //      b = d;
          //      textBox1.Text = "Второе число";
                
          //  }
          //else if (z % 2 != 0)
          //  {
          //      c = Convert.ToDouble(textBox1.Text);
               
          //      result = Math.Pow(b, 1 / c);
          //      textBox1.Text = result.ToString();
          //  }
          //  z++;
        }
        //кубческий корень
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Pow(x, 1.0/3.0);
            textBox1.Text = result.ToString();
        }
        //х в кубе
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Pow(x, 3);
            textBox1.Text = result.ToString();
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            int x, result, temp;
            result = 1;
            int i = 0;
            int f = 1;
            x = Convert.ToInt32(textBox1.Text);
          while (i < x)
            {
                result *= f;
                f++;
                i++;
            }
            textBox1.Text = result.ToString();

        }
        //|x|
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            double x, result;
            result = 0;
            x = Convert.ToDouble(textBox1.Text);
            if (x < 0)
            {
                result = -x;
            }
            else
            {
                result = x;
            }
            textBox1.Text = result.ToString();
        }
       // ехр
        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            double x,  result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Exp(x);
            textBox1.Text = result.ToString();
        }
        //е
        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = 2.71828182845904;
            result = x;
            textBox1.Text = result.ToString();
        }
        //10^x
        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Pow(10, x);
            textBox1.Text = result.ToString();
        }
        //ln
        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Log(x);
            textBox1.Text = result.ToString();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            textBox1.Text = textBox1.Text + "(";
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            textBox1.Text = textBox1.Text + ")";
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            if (l % 2 == 0)
            {
                vise.Visibility = Visibility.Visible;
                vise2.Visibility = Visibility.Visible;
                vise3.Visibility = Visibility.Visible;
                vise4.Visibility = Visibility.Visible;
                vise5.Visibility = Visibility.Visible;
                vise6.Visibility = Visibility.Visible;
                vise7.Visibility = Visibility.Visible;
                vise8.Visibility = Visibility.Visible;
                vise9.Visibility = Visibility.Visible;
                vise10.Visibility = Visibility.Visible;
                vise11.Visibility = Visibility.Visible;
                vise12.Visibility = Visibility.Visible;
            }
            if (l % 2 != 0)
            {
                vise.Visibility = Visibility.Hidden;
                vise2.Visibility = Visibility.Hidden;
                vise3.Visibility = Visibility.Hidden;
                vise4.Visibility = Visibility.Hidden;
                vise5.Visibility = Visibility.Hidden;
                vise6.Visibility = Visibility.Hidden;
                vise7.Visibility = Visibility.Hidden;
                vise8.Visibility = Visibility.Hidden;
                vise9.Visibility = Visibility.Hidden;
                vise10.Visibility = Visibility.Hidden;
                vise11.Visibility = Visibility.Hidden;
                vise12.Visibility = Visibility.Hidden;
            }
            l++;
        }
        //sin
        private void Button_NeClick_(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Sin(x);
            textBox1.Text = result.ToString();
        }
        //cos
        private void Button_NeClick_2(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Cos(x);
            textBox1.Text = result.ToString();
        }
        //tg
        private void Button_NeClick_3(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Tan(x);
            textBox1.Text = result.ToString();
        }
        //ctg
        private void Button_NeClick_4(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = 1/Math.Tan(x);
            textBox1.Text = result.ToString();
        }
        //csc
        private void Button_NeClick_9(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = 1/Math.Sin(x);
            textBox1.Text = result.ToString();
        }
        //sec
        private void Button_NeClick_6(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = 1 / Math.Cos(x);
            textBox1.Text = result.ToString();
        }
        //asin
        private void Button_NeClick_7(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Asin(x);
            textBox1.Text = result.ToString();
        }
        //acos
        private void Button_NeClick_8(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Acos(x);
            textBox1.Text = result.ToString();

        }
        //atg
        private void Button_NeClick_5(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = Math.Atan(x);
            textBox1.Text = result.ToString();
        }
        //actg
        private void Button_NeClick_11(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = 1/Math.Atan(x);
            textBox1.Text = result.ToString();
        }
        //acsc
        private void Button_NeClick_12(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = 1 / Math.Asin(x);
            textBox1.Text = result.ToString();
        }
        //asec
        private void Button_NeClick_10(object sender, RoutedEventArgs e)
        {
            double x, result;
            x = Convert.ToDouble(textBox1.Text);
            result = 1 / Math.Acos(x);
            textBox1.Text = result.ToString();
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            k = false;
            da.Visibility = Visibility.Visible;
            da2.Visibility = Visibility.Visible;
            da3.Visibility = Visibility.Visible;
            da4.Visibility = Visibility.Visible;
            da5.Visibility = Visibility.Visible;
            da6.Visibility = Visibility.Visible;
            da7.Visibility = Visibility.Visible;
            da8.Visibility = Visibility.Visible;
            da9.Visibility = Visibility.Visible;
            da10.Visibility = Visibility.Visible;
            da11.Visibility = Visibility.Visible;
            da12.Visibility = Visibility.Visible;
            da14.Visibility = Visibility.Visible;
            da15.Visibility = Visibility.Visible;
            da16.Visibility = Visibility.Visible;
            da17.Visibility = Visibility.Visible;
            da18.Visibility = Visibility.Visible;
            da19.Visibility = Visibility.Visible;
            da20.Visibility = Visibility.Visible;
            da21.Visibility = Visibility.Visible;
            da22.Visibility = Visibility.Visible;
            da23.Visibility = Visibility.Visible;
            da24.Visibility = Visibility.Visible;
            da25.Visibility = Visibility.Visible;
            da26.Visibility = Visibility.Visible;
            da27.Visibility = Visibility.Visible;
            da28.Visibility = Visibility.Visible;
            da29.Visibility = Visibility.Visible;
            da30.Visibility = Visibility.Visible;
            da31.Visibility = Visibility.Visible;
            da32.Visibility = Visibility.Visible;
            da33.Visibility = Visibility.Visible;
            da34.Visibility = Visibility.Visible;
            da35.Visibility = Visibility.Visible;
            da36.Visibility = Visibility.Visible;
            da37.Visibility = Visibility.Visible;
            da200.Visibility = Visibility.Hidden;
            da201.Visibility = Visibility.Hidden;
            
            da203.Visibility = Visibility.Hidden;
            da204.Visibility = Visibility.Hidden;
            da205.Visibility = Visibility.Hidden;
            da206.Visibility = Visibility.Hidden;
            da207.Visibility = Visibility.Hidden;
            da208.Visibility = Visibility.Hidden;
            da209.Visibility = Visibility.Hidden;
            da210.Visibility = Visibility.Hidden;
           
            da212.Visibility = Visibility.Hidden;
            da213.Visibility = Visibility.Hidden;
            da214.Visibility = Visibility.Hidden;
            da215.Visibility = Visibility.Hidden;
            da216.Visibility = Visibility.Hidden;
            da217.Visibility = Visibility.Hidden;
            da218.Visibility = Visibility.Hidden;
            da219.Visibility = Visibility.Hidden;
            
            da221.Visibility = Visibility.Hidden;
            da222.Visibility = Visibility.Hidden;
            da223.Visibility = Visibility.Hidden;
            da224.Visibility = Visibility.Hidden;
            da225.Visibility = Visibility.Hidden;
            da226.Visibility = Visibility.Hidden;
            da227.Visibility = Visibility.Hidden;
            da228.Visibility = Visibility.Hidden;
            da229.Visibility = Visibility.Hidden;
            da230.Visibility = Visibility.Hidden;
            da231.Visibility = Visibility.Hidden;
            da232.Visibility = Visibility.Hidden;
            da233.Visibility = Visibility.Hidden;
            da234.Visibility = Visibility.Hidden;
            da235.Visibility = Visibility.Hidden;
            da236.Visibility = Visibility.Hidden;
            da237.Visibility = Visibility.Hidden;
            da100.Visibility = Visibility.Hidden;
            da101.Visibility = Visibility.Hidden;
            da102.Visibility = Visibility.Hidden;
            da103.Visibility = Visibility.Hidden;
            da104.Visibility = Visibility.Hidden;
            da105.Visibility = Visibility.Hidden;
            da106.Visibility = Visibility.Hidden;
            da107.Visibility = Visibility.Hidden;
            da108.Visibility = Visibility.Hidden;
            da109.Visibility = Visibility.Hidden;
            da110.Visibility = Visibility.Hidden;
            da111.Visibility = Visibility.Hidden;
            da112.Visibility = Visibility.Hidden;
            da114.Visibility = Visibility.Hidden;
            da115.Visibility = Visibility.Hidden;
            da116.Visibility = Visibility.Hidden;
            da117.Visibility = Visibility.Hidden;
            da118.Visibility = Visibility.Hidden;
            da119.Visibility = Visibility.Hidden;
            da120.Visibility = Visibility.Hidden;
            da121.Visibility = Visibility.Hidden;
            da122.Visibility = Visibility.Hidden;
            da123.Visibility = Visibility.Hidden;
            da113.Visibility = Visibility.Hidden;
            textBox2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            k = false;
            da.Visibility = Visibility.Hidden;
            da2.Visibility = Visibility.Hidden;
            da3.Visibility = Visibility.Hidden;
            da4.Visibility = Visibility.Hidden;
            da5.Visibility = Visibility.Hidden;
            da6.Visibility = Visibility.Hidden;
            da7.Visibility = Visibility.Hidden;
            da8.Visibility = Visibility.Hidden;
            da9.Visibility = Visibility.Hidden;
            da10.Visibility = Visibility.Hidden;
            da11.Visibility = Visibility.Hidden;
            da12.Visibility = Visibility.Hidden;
            da14.Visibility = Visibility.Hidden;
            da15.Visibility = Visibility.Hidden;
            da16.Visibility = Visibility.Hidden;
            da17.Visibility = Visibility.Hidden;
            da18.Visibility = Visibility.Hidden;
            da19.Visibility = Visibility.Hidden;
            da20.Visibility = Visibility.Hidden;
            da21.Visibility = Visibility.Hidden;
            da22.Visibility = Visibility.Hidden;
            da23.Visibility = Visibility.Hidden;
            da24.Visibility = Visibility.Hidden;
            da25.Visibility = Visibility.Hidden;
            da26.Visibility = Visibility.Hidden;
            da27.Visibility = Visibility.Hidden;
            da28.Visibility = Visibility.Hidden;
            da29.Visibility = Visibility.Hidden;
            da30.Visibility = Visibility.Hidden;
            da31.Visibility = Visibility.Hidden;
            da32.Visibility = Visibility.Hidden;
            da33.Visibility = Visibility.Hidden;
            da34.Visibility = Visibility.Hidden;
            da35.Visibility = Visibility.Hidden;
            da200.Visibility = Visibility.Hidden;
            da201.Visibility = Visibility.Hidden;
         
            da203.Visibility = Visibility.Hidden;
            da204.Visibility = Visibility.Hidden;
            da205.Visibility = Visibility.Hidden;
            da206.Visibility = Visibility.Hidden;
            da207.Visibility = Visibility.Hidden;
            da208.Visibility = Visibility.Hidden;
            da209.Visibility = Visibility.Hidden;
            da210.Visibility = Visibility.Hidden;

            da212.Visibility = Visibility.Hidden;
            da213.Visibility = Visibility.Hidden;
            da214.Visibility = Visibility.Hidden;
            da215.Visibility = Visibility.Hidden;
            da216.Visibility = Visibility.Hidden;
            da217.Visibility = Visibility.Hidden;
            da218.Visibility = Visibility.Hidden;
            da219.Visibility = Visibility.Hidden;
       
            da221.Visibility = Visibility.Hidden;
            da222.Visibility = Visibility.Hidden;
            da223.Visibility = Visibility.Hidden;
            da224.Visibility = Visibility.Hidden;
            da225.Visibility = Visibility.Hidden;
            da226.Visibility = Visibility.Hidden;
            da227.Visibility = Visibility.Hidden;
            da228.Visibility = Visibility.Hidden;
            da229.Visibility = Visibility.Hidden;
            da230.Visibility = Visibility.Hidden;
            da231.Visibility = Visibility.Hidden;
            da232.Visibility = Visibility.Hidden;
            da233.Visibility = Visibility.Hidden;
            da234.Visibility = Visibility.Hidden;
            da235.Visibility = Visibility.Hidden;
            da236.Visibility = Visibility.Hidden;
            da237.Visibility = Visibility.Hidden;
            da36.Visibility = Visibility.Hidden;
            da37.Visibility = Visibility.Hidden;
            da100.Visibility = Visibility.Visible;
            da101.Visibility = Visibility.Visible;
            da102.Visibility = Visibility.Visible;
            da103.Visibility = Visibility.Visible;
            da104.Visibility = Visibility.Visible;
            da105.Visibility = Visibility.Visible;
            da106.Visibility = Visibility.Visible;
            da107.Visibility = Visibility.Visible;
            da108.Visibility = Visibility.Visible;
            da109.Visibility = Visibility.Visible;
            da110.Visibility = Visibility.Visible;
            da111.Visibility = Visibility.Visible;
            da112.Visibility = Visibility.Visible;
            da114.Visibility = Visibility.Visible;
            da115.Visibility = Visibility.Visible;
            da116.Visibility = Visibility.Visible;
            da117.Visibility = Visibility.Visible;
            da118.Visibility = Visibility.Visible;
            da119.Visibility = Visibility.Visible;
            da120.Visibility = Visibility.Visible;
            da121.Visibility = Visibility.Visible;
            da122.Visibility = Visibility.Visible;
            da123.Visibility = Visibility.Visible;
            da113.Visibility = Visibility.Visible;
            textBox2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            textBox2.Visibility = Visibility.Visible;
            k = true;
            da.Visibility = Visibility.Hidden;
            da2.Visibility = Visibility.Hidden;
            da3.Visibility = Visibility.Hidden;
            da4.Visibility = Visibility.Hidden;
            da5.Visibility = Visibility.Hidden;
            da6.Visibility = Visibility.Hidden;
            da7.Visibility = Visibility.Hidden;
            da8.Visibility = Visibility.Hidden;
            da9.Visibility = Visibility.Hidden;
            da10.Visibility = Visibility.Hidden;
            da11.Visibility = Visibility.Hidden;
            da12.Visibility = Visibility.Hidden;
            da14.Visibility = Visibility.Hidden;
            da15.Visibility = Visibility.Hidden;
            da16.Visibility = Visibility.Hidden;
            da17.Visibility = Visibility.Hidden;
            da18.Visibility = Visibility.Hidden;
            da19.Visibility = Visibility.Hidden;
            da20.Visibility = Visibility.Hidden;
            da21.Visibility = Visibility.Hidden;
            da22.Visibility = Visibility.Hidden;
            da23.Visibility = Visibility.Hidden;
            da24.Visibility = Visibility.Hidden;
            da25.Visibility = Visibility.Hidden;
            da26.Visibility = Visibility.Hidden;
            da27.Visibility = Visibility.Hidden;
            da28.Visibility = Visibility.Hidden;
            da29.Visibility = Visibility.Hidden;
            da30.Visibility = Visibility.Hidden;
            da31.Visibility = Visibility.Hidden;
            da32.Visibility = Visibility.Hidden;
            da33.Visibility = Visibility.Hidden;
            da34.Visibility = Visibility.Hidden;
            da35.Visibility = Visibility.Hidden;
            da36.Visibility = Visibility.Hidden;
            da37.Visibility = Visibility.Hidden;
            da100.Visibility = Visibility.Hidden;
            da101.Visibility = Visibility.Hidden;
            da102.Visibility = Visibility.Hidden;
            da103.Visibility = Visibility.Hidden;
            da104.Visibility = Visibility.Hidden;
            da105.Visibility = Visibility.Hidden;
            da106.Visibility = Visibility.Hidden;
            da107.Visibility = Visibility.Hidden;
            da108.Visibility = Visibility.Hidden;
            da109.Visibility = Visibility.Hidden;
            da110.Visibility = Visibility.Hidden;
            da111.Visibility = Visibility.Hidden;
            da112.Visibility = Visibility.Hidden;
            da114.Visibility = Visibility.Hidden;
            da115.Visibility = Visibility.Hidden;
            da116.Visibility = Visibility.Hidden;
            da117.Visibility = Visibility.Hidden;
            da118.Visibility = Visibility.Hidden;
            da119.Visibility = Visibility.Hidden;
            da120.Visibility = Visibility.Hidden;
            da121.Visibility = Visibility.Hidden;
            da122.Visibility = Visibility.Hidden;
            da123.Visibility = Visibility.Hidden;
            da113.Visibility = Visibility.Hidden;
            da200.Visibility = Visibility.Visible;
            da201.Visibility = Visibility.Visible;
          
            da203.Visibility = Visibility.Visible;
            da204.Visibility = Visibility.Visible;
            da205.Visibility = Visibility.Visible;
            da206.Visibility = Visibility.Visible;
            da207.Visibility = Visibility.Visible;
            da208.Visibility = Visibility.Visible;
            da209.Visibility = Visibility.Visible;
            da210.Visibility = Visibility.Visible;
          
            da212.Visibility = Visibility.Visible;
            da213.Visibility = Visibility.Visible;
            da214.Visibility = Visibility.Visible;
            da215.Visibility = Visibility.Visible;
            da216.Visibility = Visibility.Visible;
            da217.Visibility = Visibility.Visible;
            da218.Visibility = Visibility.Visible;
            da219.Visibility = Visibility.Visible;
          
            da221.Visibility = Visibility.Visible;
            da222.Visibility = Visibility.Visible;
            da223.Visibility = Visibility.Visible;
            da224.Visibility = Visibility.Visible;
            da225.Visibility = Visibility.Visible;
            da226.Visibility = Visibility.Visible;
            da227.Visibility = Visibility.Visible;
            da228.Visibility = Visibility.Visible;
            da229.Visibility = Visibility.Visible;
            da230.Visibility = Visibility.Visible;
            da231.Visibility = Visibility.Visible;
            da232.Visibility = Visibility.Visible;
            da233.Visibility = Visibility.Visible;
            da234.Visibility = Visibility.Visible;
            da235.Visibility = Visibility.Visible;
            da236.Visibility = Visibility.Visible;
            da237.Visibility = Visibility.Visible;
            
        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            textBox1.Text = String.Empty;
            inputstatus = true;
        }

        private void da231_Click(object sender, RoutedEventArgs e)
        {
            da232.IsEnabled = true;
            da233.IsEnabled = true;
            da234.IsEnabled = true;
            da235.IsEnabled = true;
            da236.IsEnabled = true;
            da237.IsEnabled = true;
        }

        

        private void da230_Click(object sender, RoutedEventArgs e)
        {
            da232.IsEnabled = false;
            da233.IsEnabled = false;
            da234.IsEnabled = false;
            da235.IsEnabled = false;
            da236.IsEnabled = false;
            da237.IsEnabled = false;
        }

        private void da229_Click(object sender, RoutedEventArgs e)
        {
            da232.IsEnabled = false;
            da233.IsEnabled = false;
            da234.IsEnabled = false;
            da235.IsEnabled = false;
            da236.IsEnabled = false;
            da237.IsEnabled = false;
        }

        private void da228_Click(object sender, RoutedEventArgs e)
        {

        }

        private void da228_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_can2(object sender, RoutedEventArgs e)
        {
            int r = Convert.ToInt32(textBox1.Text);
            string result = Convert.ToString(r, 2);
            textBox1.Text = result.ToString();
        }

        private void Button_Click_can3(object sender, RoutedEventArgs e)
        {
            int r = Convert.ToInt32(textBox1.Text);
            string result = Convert.ToString(r, 10);
            textBox1.Text = result.ToString();
        }

        private void Button_Click_can4(object sender, RoutedEventArgs e)
        {
            int r = Convert.ToInt32(textBox1.Text);
            string result = Convert.ToString(r, 8);
            textBox1.Text = result.ToString();
        }

        private void Button_Click_can5(object sender, RoutedEventArgs e)
        {
            int r = Convert.ToInt32(textBox1.Text);
            string result = Convert.ToString(r, 16);
            textBox1.Text = result.ToString();

        }
    }
}
