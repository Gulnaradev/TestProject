using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBLL
{
    public interface IMeetingBLL
    {
        IEnumerable<string> AddMeeting(Meeting meeting);
        IEnumerable<Meeting> GetAllMeetings();
        void DeleteMeeting(int id);
        IEnumerable<string> AddMeetingMember(Member member, int meetingId);
        void DeleteMemberFromMeeting(Guid memberId, int meetingId);
    }
}
