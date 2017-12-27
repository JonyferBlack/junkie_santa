using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace JunkieSanta
{
    public class DataLogicModel
    {
        public readonly List<string> Names;
        private const int SantaImagesCount = 14;

        private readonly Random Randomizer = new Random();
        private int _imageIndex;
        private string _nameBoxText;
        private string _path;

        public DataLogicModel(string path)
        {
            _path = path;
            Names = new List<string>(ReadAllNames());
        }

        public int ImageIndex => _imageIndex;

        public void UpdateIndex()
        {
            _imageIndex = Randomizer.Next(1, SantaImagesCount);
        }

        public string NameBoxText
        {
            set { _nameBoxText = value; }
            get { return _nameBoxText; }
        }

        private string _predictedName = "no_name";
        public string PredictedName => _predictedName;

        private IEnumerable<string> ReadTackenNames()
        {
            try
            {
                return System.IO.File.ReadAllLines(@"~\taken.names");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        private IEnumerable<string> ReadAllNames()
        {
            using (StreamReader streamReader = File.OpenText($"{}\all.names"))
            {
                yield return streamReader.ReadLine();
            }
            yield return null;
        }


        private void WriteTakenNames(IEnumerable<string> names)
        {
            using (StreamWriter outputFile = new StreamWriter(@"!\taken.names"))
            {
                foreach (var line in names)
                    outputFile.WriteLine(line);
            }
        }

        public void FindPresentReciever(string name)
        {
            var takenNames = ReadTackenNames().Union(new []{ name }).ToList();
            var restNames = Names.Except(takenNames).ToArray();
            _predictedName = restNames[Randomizer.Next(1, restNames.Length)];
            takenNames.Add(_predictedName);
            WriteTakenNames(takenNames);
            WriteCurrentName();
        }

        private void WriteCurrentName()
        {
            using (StreamWriter outputFile = new StreamWriter($"~\'{NameBoxText}.game"))
            {
                outputFile.WriteLine(_predictedName);
            }
        }

        public void UpdateImage(ImageButton imageButton, string path)
        {
            UpdateIndex();
            imageButton.ImageUrl = $"{path}{ImageIndex}.jpg";
        }
    }
}