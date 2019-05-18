using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SrtMostUsedWords
{
    public partial class Form1 : Form
    {
        String fileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.ShowDialog();
            fileName = opf.FileName;

            Dictionary<string, int> dict = CreateDictionary();
            DataTable dt = ToDataTable(dict);
            dataGridView1.DataSource = dt;
        }

        private Dictionary<string, int> CreateDictionary()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            String fileContent = File.ReadAllText(fileName);

            fileContent = fileContent.Replace("<i>", "");
            fileContent = fileContent.Replace("</i>", "");
            fileContent = fileContent.Replace("\n", " ");
            fileContent = fileContent.Replace(".", " ");
            fileContent = fileContent.Replace("?", " ");
            fileContent = fileContent.Replace("!", " ");
            fileContent = fileContent.Replace(",", " ");
            fileContent = fileContent.Replace("<br>", " ");


            fileContent = fileContent.ToLower();

            String[] words = fileContent.Split(' ');
            foreach (String word in words)
            {
                if (!dict.ContainsKey(word))
                    dict.Add(word, 1);
                else
                    dict[word]++;
            }
            return dict;
        }

        static DataTable ToDataTable(Dictionary<string, int> item)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Col1", typeof(string));
            result.Columns.Add("Col2", typeof(int));

            foreach (var key in item.Keys)
            {
                if (key.Contains(":"))
                    continue;
                if (int.TryParse(key, out int x))
                    continue;
                result.Rows.Add(new object[] { key, item[key] });
            }
            
            return result;
        }

    }
}
