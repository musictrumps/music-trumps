using System;

namespace TrumpEngine.Model
{
    public class Band
    {
        public Band()
        {
            this.Visible = true;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public DateTime Begin { get; set; }

        public int Year
        {
            get { return this.Begin.Year; }
        }
        public string Summary { get; set; }

        public bool Visible { get; set; }
    }
}
