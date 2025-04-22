using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project1.Lesson_13_Assignment.BusinessLayer
{
    /// <summary>
    /// Represents a single conference session
    /// </summary>
    public class Session
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }

        public Session(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}