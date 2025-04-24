using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using project1.Lesson_13_Assignment.BusinessLayer.Exceptions;

namespace project1.Lesson_13_Assignment.BusinessLayer
{
	/// <summary>
	/// Represents a single speaker
	/// First version: https://github.com/mustafadagdelen/dirty-code-for-clean-code-sample/blob/master/BusinessLayer/Speaker.cs
	/// </summary>
	public class Speaker
	{
		//var nt = new List<string> {"MVC4", "Node.js", "CouchDB", "KendoUI", "Dapper", "Angular"}; //rename to _newTechnologies
		public static IReadOnlyList<string> OldTechnologies = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" }; 
		public static IReadOnlyList<string> UnfavorableDomains = new List<string>() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };
		public static IReadOnlyList<string> PreferredEmployers = new List<string>() { "Microsoft", "Google", "Fog Creek Software", "37Signals" };
		
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Employer { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public int? Experience { get; set; }
		public WebBrowser Browser { get; set; }
		
		public List<string> Certifications { get; set; }
		public List<Session> Sessions { get; set; }
	}
}