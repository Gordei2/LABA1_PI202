using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnTextChanged(object sender, KeyPressEventArgs e)
        {
            char character = e.KeyChar;
            var input = this.Controls.Find("number_field", true)[0] as TextBox;

            //if (!Regex.IsMatch(input.Text.ToString(), @"^[0-9]+(\.[0-9]+)?$", RegexOptions.None))
            //    e.Handled = true;
        }

        private void binaryClick(object sender, EventArgs e)
        {
            var input = this.Controls.Find("number_field", true)[0] as TextBox;
            if (!Regex.IsMatch(input.Text, @"^[0-1]+(\.[0-1]+)?$", RegexOptions.None))
            {
                MessageBox.Show("Incorrect number format", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                var label = this.Controls.Find("result", true)[0] as Label;

                var binaryConverter = new BinaryToHexConverter();

                label.Text = binaryConverter.Convert(input.Text.ToUpper());
            }
        }

        private void hexClick(object sender, EventArgs e)
        {
            var input = this.Controls.Find("number_field", true)[0] as TextBox;
            if (!Regex.IsMatch(input.Text, @"^[0-9a-fA-F]+(\.[0-9a-fA-F]+)?$", RegexOptions.None))
            {
                MessageBox.Show("Incorrect number format", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                var label = this.Controls.Find("result", true)[0] as Label;

                var hexConverter = new HexToBinaryConverter();

                label.Text = hexConverter.Convert(input.Text.ToUpper());
            }
        }
    }

    static class Extension
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            return array.Skip(offset)
                    .Take(length)
                    .ToArray();
        }
    }

    abstract class Converter
    {
        private int fracLength = 8;
        protected virtual int OldBase { get; }
        protected virtual int NewBase { get; }

        public abstract string Convert(string number);

        public (List<int>, List<int>) ConvertSeparately(string number)
        {
            (int whole, double fractional) = ToDecimal(number);
            var wholeArray = new List<int>();
            var fractionalArray = new List<int>();

            int rem;
            while (whole >= NewBase)
            {
                rem = whole % NewBase;
                whole /= NewBase;

                wholeArray.Add(rem);
            }
            wholeArray.Add(whole);
            wholeArray.Reverse();

            int fl = fracLength;
            while (fl != 0)
            {
                fractional *= NewBase;
                rem = (int)fractional;
                fractional -= rem;

                fractionalArray.Add(rem);
                fl--;
            }

            int lastMean = fractionalArray.FindLastIndex(e => e != 0);
            fractionalArray.RemoveRange(lastMean + 1, fractionalArray.Count - lastMean - 1);

            return (wholeArray, fractionalArray);
        }

        public abstract (int, double) ToDecimal(string number);
    }

    class BinaryToHexConverter : Converter
    {
        protected override int OldBase { get => 2; }
        protected override int NewBase { get => 16; }

        public override string Convert(string number)
        {
            (List<int> wholeArray, List<int> fractionalArray) = ConvertSeparately(number);
            return DigitToLetter(wholeArray) + ((fractionalArray.Count == 0) ? "" : ('.' + DigitToLetter(fractionalArray)));
        }

        public override (int, double) ToDecimal(string number)
        {
            var chars = number.ToCharArray();
            int point = number.IndexOf('.');
            var first = Array.ConvertAll(chars.SubArray(0, (point != -1) ? point : chars.Length), ch => (int)Char.GetNumericValue(ch));
            var second = (point != -1) ? Array.ConvertAll(chars.SubArray(point + 1, chars.Length - point - 1), ch => (int)Char.GetNumericValue(ch))
                : new int[0];

            double whole = 0, fractional = 0;

            for (int i = 0; i < first.Length; i++)
                whole += Math.Pow(OldBase, first.Length - 1 - i) * first[i];

            for (int i = 0; i < second.Length; i++)
            {
                fractional += Math.Pow(OldBase, -1 * (i + 1)) * second[i];
            }

            return ((int)whole, fractional);
        }

        private string DigitToLetter(List<int> numbers)
        {
            var result = "";

            foreach (var number in numbers)
            {
                if (number > 9)
                    for (int i = 0; i <= 'f' - 'a'; i++)
                    {
                        if (number == i + 10)
                            result += Char.ToString((char)('A' + i));
                    }
                else
                    result += number;
            }


            return result;
        }
    }

    class HexToBinaryConverter : Converter
    {
        protected override int OldBase { get => 16; }
        protected override int NewBase { get => 2; }
        public override string Convert(string number)
        {
            (List<int> wholeArray, List<int> fractionalArray) = ConvertSeparately(number);

            return String.Join("", wholeArray.ConvertAll<string>(e => e.ToString())) + ((fractionalArray.Count == 0)
                ? "" : ('.' + String.Join("", fractionalArray.ConvertAll<string>(e => e.ToString()))));
        }

        public override (int, double) ToDecimal(string number)
        {
            var chars = number.ToCharArray();
            int point = number.IndexOf('.');

            var first = Array.ConvertAll(chars.SubArray(0, (point != -1) ? point : chars.Length), ch =>
            {
                var cs = ch.ToString();
                if (char.IsDigit(ch))
                    return (int)Char.GetNumericValue(ch);
                else
                {
                    for (int i = 0; i <= 'f' - 'a'; i++)
                    {
                        if (ch == 'A' + i)
                            return 10 + i;
                    }
                    return 10;
                }
            });
            var second = (point != -1) ? Array.ConvertAll(chars.SubArray(point + 1, chars.Length - point - 1), ch =>
            {
                var cs = ch.ToString();
                if (char.IsDigit(ch))
                    return (int)Char.GetNumericValue(ch);
                else
                {
                    for (int i = 0; i <= 'F' - 'A'; i++)
                    {
                        if (ch == 'A' + i)
                            return 10 + i;
                    }
                    return 10;
                }
            }) : new int[0];

            double whole = 0, fractional = 0;

            for (int i = 0; i < first.Length; i++)
                whole += Math.Pow(OldBase, first.Length - 1 - i) * first[i];

            for (int i = 0; i < second.Length; i++)
            {
                fractional += Math.Pow(OldBase, -1 * (i + 1)) * second[i];
            }

            return ((int)whole, fractional);
        }
    }
}
