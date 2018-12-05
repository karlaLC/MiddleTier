using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Requests.Logs
{
	public class IssueLogStatusTypeAddRequest
	{		
		[Required]
		[MaxLength(50, ErrorMessage = "Max amount of characters exceeded.")]
		public string TypeName { get; set; }

		[MaxLength(150, ErrorMessage = "Max amount of characters exceeded.")]
		public string TypeDescription { get; set; }
	}
}
