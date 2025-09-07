using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using static Project1.Enums;

namespace Project1
{
    public class RatStates
    {
        public List<RatState> States { get; set; }
        private int topFramesAnimation;
        public RatStates() { }
        public void Load(string fileName, ContentManager content)
        {
            States = new List<RatState>();
            this.topFramesAnimation = int.Parse(ConfigurationManager.AppSettings["topFramesPerSpriteAnimation"]);
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                XDocument doc = XDocument.Load(stream);
                var states = doc.Descendants(typeof(RatState).Name);
                foreach (var element in states)
                {
                    States.Add(new RatState(element, content, this.topFramesAnimation));
                }
            }
        }
    }
}
