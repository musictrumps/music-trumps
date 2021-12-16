namespace TrumpEngine.Model
{
    public class Game
    {
        public string UUID { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Player1_Cards { get; set; }
        public string Player2_Cards { get; set; }
        public int Player1_Points { get; set; }
        public int Player2_Points { get; set; }

        public int Player1_Turn { get; set; }
        public int Player2_Turn { get; set; }

        public string Player1_CurrentBand { get; set; }
        public string Player2_CurrentBand { get; set; }

        public Game()
        {
            this.Player1 = string.Empty;
            this.Player2 = string.Empty;
            this.Player1_Cards = string.Empty;
            this.Player2_Cards = string.Empty;
            this.Player1_CurrentBand = string.Empty;
            this.Player2_CurrentBand = string.Empty;
        }
    }
}
