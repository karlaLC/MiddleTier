using Prospect.Models.Domain.Timelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Responses
{
	public class TimelineFeedResponse: TimelineFeed
	{
		public int UserBaseId { get; set; } 
		public string UserName { get; set; } 
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string AvatarURL { get; set; }
		public int AppRoleId { get; set; }
		public int FollowingUserId { get; set; }
		public Boolean TimelineOwner { get; set; }
		public string TimelineType { get; set; }
		public string ExtraFeedImage { get; set; }
	}
}