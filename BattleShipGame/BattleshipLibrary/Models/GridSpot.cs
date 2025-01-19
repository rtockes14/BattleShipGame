namespace BattleshipLibrary.Models
{
    public class GridSpot
    {
        public string Coordinate { get; set; }
        public string Letter { get; set; }

        public int Number {  get; set; }

        public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;
    }
}