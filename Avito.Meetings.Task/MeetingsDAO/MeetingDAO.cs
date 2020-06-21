using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity;

namespace MeetingsDAO
{
    public class MeetingDAO : IMeetingDAO
    {

        public void AddMeeting(Meeting meeting)
        {
            using (var context = new MyDBContext())
            {
                context.Meetings.Add(meeting);
                context.SaveChanges();
            }
        }

        public void AddMeetingMember(Member member, int meetingId)
        {
            using (var context = new MyDBContext())
            {
                context.Meetings.Where(m => m.Id == meetingId)?
                    .First().Members.Add(member);
                context.SaveChanges();
            }
        }

        public void DeleteMeeting(int id)
        {
            using (var context = new MyDBContext())
            {
                var meeting = context.Meetings.FirstOrDefault(x => x.Id == id);

                if (meeting != null)
                {
                    context.Entry(meeting)
                       .Collection(c => c.Members)
                       .Load();

                    context.Meetings.Remove(meeting);
                    context.SaveChanges();
                }
            }
        }

        public Meeting GetMeetingById(int id)
        {
            Meeting meeting = null;

            using (var context = new MyDBContext())
            {
                meeting = context.Meetings.Where(x => x.Id == id)?.First();
            }

            return meeting;
        }

        public Member GetMemberById(Guid id)
        {
            Member member = null;

            using (var context = new MyDBContext())
            {
                member = context.Members.Where(x => x.Id == id)?.First();
            }

            return member;
        }

        public void DeleteMemberFromMeeting(Guid memberId, int meetingId)
        {
            using (var context = new MyDBContext())
            {
                var member = context.Members.FirstOrDefault(x => x.Id == memberId);
                if (member != null)
                {
                    context.Members.Remove(member);
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<Meeting> GetAllMeetings()
        {
            List<Meeting> meetings = new List<Meeting>();

            using (var context = new MyDBContext())
            {
                meetings = context.Meetings
                    .Include(x => x.Members).Include(x => x.Members)
                    .ToList();
            }

            return meetings;
        }
    }
}
