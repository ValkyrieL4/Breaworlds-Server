using System.Collections.Generic;

namespace Breaworlds.Server
{
	public struct TournamentData
	{
		public int YY;

		public int MM;

		public int DD;

		public string Winner1;

		public string Winner2;

		public string Winner3;

		public TournamentType Type;

		public Dictionary<string, int> Points;
	}
}
