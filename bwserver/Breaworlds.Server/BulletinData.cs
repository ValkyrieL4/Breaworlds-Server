using System.Collections.Generic;

namespace Breaworlds.Server
{
	public struct BulletinData
	{
		public bool Public;

		public bool Author;

		public List<CommentData> Text;
	}
}
