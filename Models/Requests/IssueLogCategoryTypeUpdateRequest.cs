﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Requests.Logs
{
	public class IssueLogCategoryTypeUpdateRequest : IssueLogCategoryTypeAddRequest
	{
		[Required]
		public int Id { get; set; }
	}
}
