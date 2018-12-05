using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Requests.Logs
{
	public class IssueLogResponseAddRequest
	{
		[Required]
		public int IssueLogId { get; set; }
	
		[MaxLength(4000, ErrorMessage = "Max amount of characters exceeded.")]
		public string ResponseText { get; set; }

		[Required]
		public int CreatedById { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}