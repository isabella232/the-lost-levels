using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TheLostLevels
{
    static class ModelProperties
    {
        public static Dictionary<String, float[]> Properties;

        static public void Initialize(String fileName)
        {
            Properties = new Dictionary<String, float[]>();
            TextReader reader = new StreamReader(@"Attributes\ModelProperties.txt");

            char[] delimiterChars = { ' ', '\t' };


            while (reader.Peek() != -1)
            {
                //add each line to the fileContents variable
                String fileContents = reader.ReadLine();

                if (fileContents != null)
                {
                    String[] words = fileContents.Split(delimiterChars);

                    var onlynumbers = new float[9];

                    int indexnum = -1;


                    foreach (string s in words)
                    {
                        if (indexnum >= 0)
                        {
                            onlynumbers[indexnum] = Convert.ToSingle(s);
                            

                        }

                        indexnum++;
                    }

                    Properties.Add(words[0], onlynumbers);
                    
                }
            }


            reader.Close();
        }
    }
}
