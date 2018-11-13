using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultileaderExtractor001
{
    class FileIO
    {
        public string readData()
        {
            // Displays an OpenFileDialog 
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "|*.txt";
            openFileDialog1.Title = "Wybierz plik ZEST";

            // Show the Dialog.  

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                string data = sr.ReadToEnd();
                sr.Close();
                return data;
            }
            else
            {
                MessageBox.Show("", "UWAGA! coś poszło nie tak..", MessageBoxButtons.OK);
                return null;
            }
        }

        public void writeData(List<string> outputData)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "|*.txt";
            saveFileDialog1.Title = "Gdzie zapisać plik?";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                System.IO.File.WriteAllLines(saveFileDialog1.FileName, outputData);
                MessageBox.Show(saveFileDialog1.FileName, "Plik zapisano:", MessageBoxButtons.OK);
            }
        }
    }
}
