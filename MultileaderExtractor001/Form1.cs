using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultileaderExtractor001
{
    public partial class MleaderExtractor : Form
    {
        public MleaderExtractor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileIO fileIO = new FileIO();
            MultileaderExtractorApp multiLeaderExtractor = new MultileaderExtractorApp();
            List<string> dataToWrite = multiLeaderExtractor.dataExtractor(fileIO.readData(), ";");
            if (dataToWrite.Count > 1)
            {
                fileIO.writeData(dataToWrite);
            }
        }

        private void MleaderExtractor_Load(object sender, EventArgs e)
        {

        }
    }
}
