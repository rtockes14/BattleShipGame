using System;
using System.Collections.Generic;


namespace BattleshipLibrary.Models
{
    public class UserModel
    {


        public string UserName { get; set; }

		public int ShotCounter { get; set; } = 1;

		public int ShipsSunk { get; set; } = 0;

		public int Misses { get; set; } = 0;

		public int ShipsLost { get; set; } = 0;

		private List<GridSpot> _shipLocations;

		private List<GridSpot> _gridSpots; // TODO: Not sure what my plan was for this

		public List<string> ShotsFired {  get; set; }

		public UserModel() 
		{
			ShotsFired = new List<string>();
			_shipLocations = new List<GridSpot>();	
			_gridSpots = new List<GridSpot>();
		}
		
        public List<GridSpot> ShipLocations
        {
			get { return _shipLocations; }
			set
			{ //_shipLocations = value; }
				if (value != null)
				{
					_shipLocations.AddRange(value);
				}
				else
				{
					throw new ArgumentException(nameof(value), "Nothing was added to the list");
				}
			}
		}

		public List<GridSpot> gridSpots
		{
			get { return _gridSpots; }
			set
			{
				if (value != null)
				{
					_gridSpots.AddRange(value);
				}
			}
		}

		public void AddShotFired(GridSpot shot)
		{
			_gridSpots.Add(shot);
		}


	}
}
