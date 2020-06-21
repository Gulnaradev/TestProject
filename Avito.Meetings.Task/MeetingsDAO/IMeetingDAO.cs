using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsDAO
{
    public interface IMeetingDAO
    {
        void AddMeeting(Meeting meeting);
        IEnumerable<Meeting> GetAllMeetings();
        void DeleteMeeting(int id);
        void DeleteMemberFromMeeting(Guid memberId, int meetingId);
        void AddMeetingMember(Member member, int meetingId);
    }
}
