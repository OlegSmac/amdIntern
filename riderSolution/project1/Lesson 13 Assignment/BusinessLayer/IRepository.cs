using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project1.Lesson_13_Assignment.BusinessLayer
{
    public interface IRepository
    {
        int SaveSpeaker(Speaker speaker);
    }
}