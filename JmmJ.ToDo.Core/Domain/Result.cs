using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JmmJ.ToDo.Core.Domain
{
	public class Result 
	{
		public string ResultMessage { get; set; }
		public IList<string> Exception { get; set; } = new List<string>();
		public HttpStatusCode StatusCode { get; set; }
	}
}
