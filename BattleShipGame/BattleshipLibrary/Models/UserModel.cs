using System;
using System.Collections.Generic;


namespace BattleshipLibrary.Models
{
    public class UserModel
    {
		public UserModel() { }

        public string UserName { get; set; }

		private List<GridSpot> _shipLocations;

		private List<GridSpot> _shotsFired;

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

		public List<GridSpot> ShotsFired
		{
			get { return _shotsFired; }
			set
			{
				if (value != null)
				{
					_shotsFired.AddRange(value);
				}
			}
		}

		public void AddShotFired(GridSpot shot)
		{
			_shotsFired.Add(shot);
		}


	}
}
