using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultileaderExtractor001
{
    class MultileaderExtractorApp
    {

        private string status;

        public string getStatus()
        {
            return this.status;
        }

        public List<string> dataExtractor(string inputData, string separator)
        {
            //string inputData = inputData;
            // string text = System.IO.File.ReadAllText(@"C:\Users\dell\Desktop\Jakosta\test.txt");
            //zamieniamy separator na typu Char by rozdzielić obiekty i wrzucic do tablicy, 
            //z nawiasami powinno zabezpieczyć przed przypadklowym wystąpieniem nil w jakiejś nazwie
            inputData = inputData.Replace(")nil(", "#");
            string[] objects = inputData.Split('#');

            List<string> mleaders = new List<string>();
            int nrOfNoMleaders = 0;
            foreach (string item in objects)
            {

                if (item.Contains("MULTILEADER")) //szukamy obiektów typu MULTILEADER
                {
                    mleaders.Add(item);


                }
                else
                {
                    nrOfNoMleaders++;
                }

            }

            List<string> outputLines = new List<string>();    //inicjalizacja listy z danymi wyjściowymi 1 element listy = 1 wiersz
            outputLines.Add("Liczba przetworzonych obiektów typu MULTILEADER: " + mleaders.Count);//pierwszy wiersz INFORMACYJNY


            //grzebiemy w każdym multileaderze i wyrzucamy co trzeba:
            string parameter;
            string outputLine;
            string load;
            string label;
            List<string> coordinates = new List<string>();

            foreach (string item in mleaders)
            {

                parameter = item.Replace(") (", "#"); //pozbywamy się nawiasów i wrzucamy parametry do tablicy
                string[] prms = parameter.Split('#');

                outputLine = "";
                load = "";
                label = "";
                coordinates.Clear();
                //lecimy po parametrach:
                for (int i = 0; i < prms.Length; i++)
                {
                    if (prms[i].Contains("304 . LEADER_LINE{"))//w nastepnym polu będą współrzędne
                    {
                        string[] temp = prms[i + 1].Split(' ');
                        coordinates.Add(temp[1] + separator + temp[2]);//wysupłane xy
                    }
                    else if (prms[i].Contains("304 ."))//tutaj powinien byc opis
                    {
                        string[] temp = prms[i].Split(' ');
                        label = temp[2];

                        if (label.Contains("x"))
                        {
                            string[] loads = temp[2].Split('x');
                            load = loads[1];
                        }
                        else
                        {
                            load = label;
                        }
                    }
                }

                //zapis do listy reprezentującej linie pliku wyjsciowego:
                foreach (string item1 in coordinates)
                {
                    outputLine = item1 + separator + load + separator + label;
                    outputLines.Add(outputLine); //zapis kolejnych wierszy do listy
                }
            }


            if (mleaders.Count == 0)
            {
                status = "UWAGA: w załadowanym pliku nie znaleziono obiektów typu MULTILEADER!!!";
            }
            else if ((mleaders.Count > 0) && (nrOfNoMleaders == 0))
            {
                status = "znalazłem " + mleaders.Count + " obiektów MULTILEADER";
            }
            else
            {
                status = "znalazłem " + mleaders.Count + " obiektów MULTILEADER, i " + nrOfNoMleaders + " innych obiektów";
            }
            MessageBox.Show(status, "Status: ", MessageBoxButtons.OK);


            return outputLines;
        }
    }
}