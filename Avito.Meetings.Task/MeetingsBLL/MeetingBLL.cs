using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Entities;
using MeetingsDAO;
using Ninject;

namespace MeetingsBLL
{
    public class MeetingBLL : IMeetingBLL
    {
        private const string timeErrorMessage = "The start of the meeting must be ealier than the end of the meeting";
        private const string requiredFieldsMessage = "StartTime and EndTime fields are required";
        private const string requestErrorMessage = "Incorrect request format";
        private const string requiredEmailErrorMessage = "Email is required";
        private const string incorrectEmailErrorMessage = "Incorrect email format for member";

        IMeetingDAO _meetingDAO;

        public MeetingBLL(IMeetingDAO meetingDAO)
        {
            _meetingDAO = meetingDAO;
        }

        public IEnumerable<string> AddMeeting(Meeting meeting)
        {
            var errors = ValidateMeeting(meeting);

            if (errors.Any())
            {
                return errors;
            }

            if (meeting.Members != null)
            {
                foreach (var member in meeting.Members)
                {
                    errors = ValidateMeetingMember(member);

                    if (errors.Any())
                    {
                        return errors;
                    }

                    member.Id = Guid.NewGuid();
                }
            }
            _meetingDAO.AddMeeting(meeting);

            return errors;
        }

        public IEnumerable<string> AddMeetingMember(Member member, int meetingId)
        {
            var errors = ValidateMeetingMember(member);

            if (errors.Any())
            {
                return errors;
            }

            member.Id = Guid.NewGuid();
            _meetingDAO.AddMeetingMember(member, meetingId);

            return errors;
        }

        public void DeleteMeeting(int id)
        {
            _meetingDAO.DeleteMeeting(id);
        }

        public void DeleteMemberFromMeeting(Guid memberId, int meetingId)
        {
            _meetingDAO.DeleteMemberFromMeeting(memberId, meetingId);
        }

        public IEnumerable<Meeting> GetAllMeetings()
        {
            return _meetingDAO.GetAllMeetings();
        }

        #region Private methods

        private IEnumerable<string> ValidateMeeting(Meeting meeting)
        {
            var errors = new List<string>();

            if (meeting == null)
            {
                errors.Add("Incorrect request format");
            }

            if (meeting != null && (meeting.StartTime == default(DateTime) || meeting.EndTime == default(DateTime)))
            {
                errors.Add("StartTime and EndTime fields are required");
            }

            if ((meeting != null && meeting.StartTime != default(DateTime) && meeting.EndTime != default(DateTime)) 
                && meeting.StartTime >= meeting.EndTime)
            {
                errors.Add("The start of the meeting must be ealier than the end of the meeting");
            }

            return errors;
        }

        private IEnumerable<string> ValidateMeetingMember(Member member)
        {
            var errors = new List<string>();

            if (member == null)
            {
                errors.Add("Incorrect request format");
            }

            if (member != null && string.IsNullOrEmpty(member.Email))
            {
                errors.Add("Email is required");
            }

            if (member != null && !string.IsNullOrEmpty(member.Email) && !IsValidEmail(member.Email))
            {
                errors.Add("Incorrect email format for member");
            }

            return errors;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        #endregion Private methods
    }
}
