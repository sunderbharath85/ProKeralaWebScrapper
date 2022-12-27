using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarWebScrapperApp
{
    public class ColoredItem
    {
        public string Text { get; set; }
        public Color Color { get; set; }
    };
    public class Logger
    {
        public ListBox listBox { get; set; }

        public Logger(ListBox listBox)
        {
            this.listBox = listBox;
        }

        private string constructMessage(string message)
        {
            string messageText = $"{DateTime.Now.ToString("g")} - {message}";
            return messageText;
        }

        private void addMessage(ColoredItem coloredItem)
        {
            listBox.Items.Add(coloredItem);
            listBox.TopIndex = listBox.Items.Count - 1;
        }

        public void log(string message)
        {
            var coloredItem = new ColoredItem { Color = Color.White, Text = constructMessage(message) };
            addMessage(coloredItem);
        }

        public void info(string message)
        {
            var coloredItem = new ColoredItem { Color = Color.Cyan, Text = constructMessage(message) };
            addMessage(coloredItem);
        }

        public void warning(string message)
        {
            var coloredItem = new ColoredItem { Color = Color.Orange, Text = constructMessage(message) };
            addMessage(coloredItem);
        }

        public void success(string message)
        {
            var coloredItem = new ColoredItem { Color = Color.Green, Text = constructMessage(message) };
            addMessage(coloredItem);
        }

        public void error(string message)
        {
            var coloredItem = new ColoredItem { Color = Color.Red, Text = constructMessage(message) };
            addMessage(coloredItem);
        }
    }
}
