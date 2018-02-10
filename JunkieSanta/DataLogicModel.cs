using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using WebGrease.Css.Ast.Selectors;

namespace JunkieSanta
{
    public class DataLogicModel
    {
        private const int SantaImagesCount = 14;

        private readonly Random Randomizer = new Random();
        private int _imageIndex;
        private string _nameBoxText;

        public DataLogicModel()
        {
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


        private IEnumerable<string> ReadAllNames(HttpServerUtility server)
        {
            using (var streamReader = new StreamReader(server.MapPath("all.name")))
            {
                while (!streamReader.EndOfStream)
                {
                yield return streamReader.ReadLine();                    
                }
            }
            yield return null;
        }


        private void WriteTakenNames(IEnumerable<string> names, HttpServerUtility server)
        {
            using (var writer = new StreamWriter(server.MapPath("taken.name")))
            {
                foreach (var line in names)
                    writer.WriteLine(line);
            }
        }

        private void WriteLoggedNames(IEnumerable<string> names, HttpServerUtility server)
        {
            using (var writer = new StreamWriter(server.MapPath("logged.name")))
            {
                foreach (var line in names)
                    writer.WriteLine(line);
            }
        }

        private IEnumerable<string> ReadLoggedNames(HttpServerUtility server)
        {
            using (var streamReader = new StreamReader(server.MapPath("logged.name")))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }

        private IEnumerable<string> ReadTakenNames(HttpServerUtility server)
        {
            using (var streamReader = new StreamReader(server.MapPath("taken.name")))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }

        public IEnumerable<string> Names(HttpServerUtility server)
        {
            return ReadAllNames(server).Where(_=>!string.IsNullOrEmpty(_));
        }

        public void FindPresentReciever(string name, HttpServerUtility server)
        {
            var takenNames = ReadTakenNames(server).Where(_=>!string.IsNullOrEmpty(_)).ToList();
            takenNames.Add(name);
            var restNames = ReadAllNames(server).Where(_=>!string.IsNullOrEmpty(_)).Except(takenNames).ToArray();
            var loggedNames = ReadLoggedNames(server).Where(_ => !string.IsNullOrEmpty(_)).ToList();

            if (restNames.Length == 2)
            {
                _predictedName = restNames.First(_ => !loggedNames.Contains(_));
            }
            else
            {
                if (restNames.Length == 0)
                {
                    _predictedName = takenNames.First();
                }
                else
                {
                    _predictedName = restNames.Length > 1
                        ? restNames[Randomizer.Next(0, restNames.Length)]
                        : restNames.First();
                }
            }
            takenNames.Remove(takenNames.Last());
            takenNames.Add(_predictedName);
            WriteTakenNames(takenNames, server);
            loggedNames.Add(name);
            WriteLoggedNames(loggedNames, server);
        }

        public void UpdateImage(ImageButton imageButton, string path)
        {
            UpdateIndex();
            imageButton.ImageUrl = $"{path}{ImageIndex}.jpg";
        }

        public IEnumerable<string> LoggedNames(HttpServerUtility server)
        {
            using (var streamReader = new StreamReader(server.MapPath("logged.name")))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }

        public IEnumerable<string> TakenNames(HttpServerUtility server)
        {
            using (var streamReader = new StreamReader(server.MapPath("taken.name")))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }
    }
}