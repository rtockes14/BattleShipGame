namespace BattleshipLibrary.Models
{
    public class GridSpot
    {
        public string Coordinate { get; set; }
        public string Letter { get; set; }

        public int Number {  get; set; }

        public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;

        //public TileLetterIndex LetterIndex { get; set; } = TileLetterIndex.N; // Not yet Populated
    }
}