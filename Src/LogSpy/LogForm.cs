using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LogSpy
{
    public partial class LogForm : Form
    {
        new string FilePath = "";
        // Define regular expressions to match error, warning and info messages
        Regex errorRegex = new Regex(@"\b(error|exception|fail(ed)?|fatal)\b", RegexOptions.IgnoreCase);
        Regex warningRegex = new Regex(@"\b(warning|caution|alert)\b", RegexOptions.IgnoreCase);
        Regex infoRegex = new Regex(@"\b(info(rmation)?|note|remark)\b", RegexOptions.IgnoreCase);

        public LogForm(string FileLocationToLoad)
        {
            InitializeComponent();
            FilePath = FileLocationToLoad;

        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            Text = FilePath;
            LoadLogEntries();
        }
        private void LoadLogEntries()
        {
            LogList.Clear();


            LogList.Visible = false;
            string[] lines = File.ReadAllLines(FilePath);

            //// Initialize the progress bar
            //LoadingBar.Value = 0;
            //LoadingBar.Maximum = LogList.Lines.Length;


            //    // Update the progress bar
            //    LoadingBar.Value = i + 1;
            //}

            // Show the RichTextBox and reset the progress bar
            LogList.Visible = true;
            GC.Collect();

            // Load in the log file
            //string[] lines = File.ReadAllLines(FilePath);

            //// Set the maximum value of the progress bar to the number of lines in the log file
            //LoadingBar.Maximum = lines.Length;

            // Loop through each line in the log file
            for (int i = 0; i < lines.Length; i++)
            {
                Refresh();
                string line = lines[i];

                // Check if the line contains an error message
                Match errorMatch = errorRegex.Match(line);
                if (errorMatch.Success)
                {
                    // Set the color of the error message to red
                    LogList.SelectionColor = Color.White;
                    LogList.SelectionBackColor = Color.Red;
                    LogList.AppendText(line);
                    LogList.AppendText(Environment.NewLine);
                    continue;
                }

                // Check if the line contains a warning message
                Match warningMatch = warningRegex.Match(line);
                if (warningMatch.Success)
                {
                    // Set the color of the warning message to orange
                    LogList.SelectionColor = Color.Black;
                    LogList.SelectionBackColor = Color.Yellow;
                    LogList.AppendText(line);
                    LogList.AppendText(Environment.NewLine);

                    continue;
                }

                // If the line does not contain an error, warning or info message, just append it as is
                LogList.AppendText(line);
                LogList.AppendText(Environment.NewLine);

                Refresh();
            }

            //LogList.Clear();

            //// Load in the log file
            //string[] lines = File.ReadAllLines(FilePath);

            //// Define regular expressions to match error, warning and info messages
            //Regex errorRegex = new Regex(@"\b(error|exception|fail(ed)?|fatal)\b", RegexOptions.IgnoreCase);
            //Regex warningRegex = new Regex(@"\b(warning|caution|alert)\b", RegexOptions.IgnoreCase);
            //Regex infoRegex = new Regex(@"\b(info(rmation)?|note|remark)\b", RegexOptions.IgnoreCase);

            //// Loop through each line in the log file
            //foreach (string line in lines)
            //{
            //    // Check if the line contains an error message
            //    Match errorMatch = errorRegex.Match(line);
            //    if (errorMatch.Success)
            //    {
            //        // Set the color of the error message to red
            //        LogList.SelectionColor = Color.White;
            //        LogList.SelectionBackColor = Color.Red;
            //        LogList.AppendText(line + "\n");
            //        continue;
            //    }

            //    // Check if the line contains a warning message
            //    Match warningMatch = warningRegex.Match(line);
            //    if (warningMatch.Success)
            //    {
            //        // Set the color of the warning message to orange
            //        LogList.SelectionColor = Color.Black;
            //        LogList.SelectionBackColor = Color.Yellow;
            //        LogList.AppendText(line + "\n");
            //        continue;
            //    }

            //    // Check if the line contains an info message
            //    Match infoMatch = infoRegex.Match(line);
            //    if (infoMatch.Success)
            //    {
            //        // Set the color of the info message to blue
            //        LogList.SelectionColor = Color.White;
            //        LogList.SelectionBackColor = Color.Blue;
            //        LogList.AppendText(line + "\n");
            //        continue;
            //    }

            //    // If the line does not contain an error, warning or info message, just append it as is
            //    LogList.AppendText(line + "\n");
            //}


            //// Go through each line in the log file
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    string line = lines[i];
            //    bool isError = false;
            //    bool isWarning = false;
            //    bool isInfo = false;

            //    // Check if the line contains any of the error keywords
            //    foreach (string errorKeyword in errorWords)
            //    {
            //        if (line.ToLower().Contains(errorKeyword.ToLower()))
            //        {
            //            isError = true;
            //            break;
            //        }
            //    }

            //    // Set the color of the line based on whether it's an error or not
            //    Color lineColor = isError ? Color.Red : Color.Black;

            //    // Add the line to the RichTextBox with the appropriate color
            //    LogList.SelectionStart = LogList.TextLength;
            //    LogList.SelectionLength = 0;
            //    LogList.SelectionColor = lineColor;
            //    LogList.AppendText(line + Environment.NewLine);
            //    LogList.SelectionColor = LogList.ForeColor;

            // Check if the line contains an info message
            //if (line.ToLower().Contains("info"))
            //{
            //    // Set the color of the info message to blue
            //    LogList.SelectionColor = Color.Black;
            //    LogList.SelectionBackColor = Color.White;
            //}

            // Add the line to the RichTextBox
        }
        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogList.Dispose();
            GC.Collect();
            Dispose();
        }

    }
        //private void OnLogFileChanged(object sender, FileSystemEventArgs e)
        //{
        //    // Load new log entries
        //    string[] newEntries = File.ReadAllLines(FilePath);

        //    // Find the index of the last entry in the ListBox
        //    int lastIndex = LogList.Items.Count - 1;

        //    // Add any new entries to the ListBox
        //    for (int i = lastIndex + 1; i < newEntries.Length; i++)
        //    {
        //        // Add the log entry to the ListBox
        //        Invoke(new Action(() =>
        //        {
        //            LogList.Items.Add(newEntries[i]);
        //        }));
        //    }
        //}

        //private void LogList_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    //e.DrawBackground();

        //    //// Get the text of the current item
        //    //string text = LogList.Items[e.Index].ToString();

        //    //// Set the background color to white
        //    //e.Graphics.FillRectangle(Brushes.White, e.Bounds);
        //    //// Set the text color to black
        //    //e.Graphics.DrawString(text, e.Font, Brushes.Black, e.Bounds);

        //    //if (errorWords.Any(w => text.ToLower().Contains(w)))
        //    //{
        //    //    // Set the background color to red
        //    //    e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
        //    //    // Set the text color to Yellow
        //    //    e.Graphics.DrawString(text, e.Font, Brushes.Yellow, e.Bounds);
        //    //}
        //    //if (warningWords.Any(w => text.ToLower().Contains(w)))
        //    //{
        //    //    // Set the background color to yellow
        //    //    e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
        //    //    // Set the text color to Black
        //    //    e.Graphics.DrawString(text, e.Font, Brushes.Black, e.Bounds);
        //    //}

        //    //e.DrawFocusRectangle();

        //    e.DrawBackground();

        //    if (e.Index >= 0)
        //    {
        //        string text = LogList.Items[e.Index].ToString();

        //        if (text.ToLower().Contains("error") || text.ToLower().Contains("failed"))
        //        {
        //            e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
        //            e.Graphics.DrawString(text, e.Font, Brushes.White, e.Bounds);
        //        }
        //        else
        //        {
        //            e.Graphics.DrawString(text, e.Font, Brushes.Black, e.Bounds);
        //        }
        //    }

        //    e.DrawFocusRectangle();
        //}
}
