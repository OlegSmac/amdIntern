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
		private const int EXPERIENCE_YEARS = 10;
		private const int CERTIFICATE_COUNT = 3;
		
		//var nt = new List<string> {"MVC4", "Node.js", "CouchDB", "KendoUI", "Dapper", "Angular"}; //rename to _newTechnologies
		public static readonly List<string> OldTechnologies = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" }; 
		public static readonly List<string> UnfavorableDomains = new List<string>() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };
		public static readonly List<string> PreferredEmployers = new List<string>() { "Microsoft", "Google", "Fog Creek Software", "37Signals" };
		
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int? Exp { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public WebBrowser Browser { get; set; }
		public List<string> Certifications { get; set; }
		public string Employer { get; set; }
		public int RegistrationFee { get; set; }
		public List<Session> Sessions { get; set; }

		private bool DoesSpeakerMeetsRequirements()
		{
			return Exp > EXPERIENCE_YEARS ||
			       HasBlog ||
			       Certifications.Count() > CERTIFICATE_COUNT ||
			       PreferredEmployers.Contains(Employer) ||
			       !UnfavorableDomains.Contains(Email.Split('@').Last()) &&
			       !(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9);
		}

		private bool IsAnyApprovedSession()
		{
			return Sessions.Any(session =>
				!OldTechnologies.Any(oldTechnology =>
					session.Title.Contains(oldTechnology) || session.Description.Contains(oldTechnology)));
		}
		
		private void CalculateRegistrationFee()
		{
			if (Exp <= 1) RegistrationFee = 500;
			else if (Exp <= 3) RegistrationFee = 250;
			else if (Exp <= 5) RegistrationFee = 100;
			else if (Exp <= 9) RegistrationFee = 50;
			else RegistrationFee = 0;
		}

		/// <summary>
		/// Register a speaker
		/// </summary>
		/// <returns>speakerID</returns>
		public int? Register(IRepository repository)
		{
			if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentNullException("First Name is required");
			if (string.IsNullOrWhiteSpace(LastName)) throw new ArgumentNullException("Last name is required.");
			if (string.IsNullOrWhiteSpace(Email)) throw new ArgumentNullException("Email is required.");
			if (Sessions.Count() == 0) throw new ArgumentException("Can't register speaker with no sessions to present.");
			if (!DoesSpeakerMeetsRequirements()) throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our arbitrary and capricious standards.");
			if (IsAnyApprovedSession()) throw new NoSessionsApprovedException("No sessions approved.");
			
			
			//if we got this far, the speaker is approved
			//let's go ahead and register him/her now.
			//First, let's calculate the registration fee. 
			//More experienced speakers pay a lower fee.
			CalculateRegistrationFee();
			
			//Now, save the speaker and sessions to the db.
			try
			{
				return repository.SaveSpeaker(this);
			}
			catch (Exception e)
			{
				//in case the db call fails
				throw new Exception("Error saving speaker", e);
			}
		}
	}
}