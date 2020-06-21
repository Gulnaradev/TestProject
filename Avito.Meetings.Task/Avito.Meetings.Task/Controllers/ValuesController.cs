using AutoMapper;
using Avito.Meetings.Task.Models;
using DependencyResolver;
using Entities;
using MeetingsBLL;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace Avito.Meetings.Task.Controllers
{
    public class ValuesController : ApiController
    {
        private static IMeetingBLL _meetingBLL;

        public ValuesController()
        {
            IKernel ninjectKernel = new StandardKernel();
            Config.RegisterServices(ninjectKernel);
            _meetingBLL = ninjectKernel.Get<MeetingBLL>();
        }

        public IHttpActionResult GetAllMeetings()
        {
            var mapper = GetMapper();
            var meetings = _meetingBLL.GetAllMeetings();
            return Json(meetings);
        }

        [HttpPost]
        public IHttpActionResult AddMeeting([FromBody]MeetingModel value)
        {
            var mapper = GetMapper();
            var meeting = mapper.Map<Meeting>(value);
            return Json(_meetingBLL.AddMeeting(meeting));
        }

        [HttpPost]
        public void DeleteMeeting(int id)
        {
            _meetingBLL.DeleteMeeting(id);
        }

        [HttpPost]
        [Route("api/Values/DeleteMember/{meetingId}/{memberId}")]
        public void DeleteMember(int meetingId, Guid memberId)
        {
            _meetingBLL.DeleteMemberFromMeeting(memberId, meetingId);
        }

        [HttpPost]
        public IHttpActionResult AddMeetingMember([FromBody]MemberModel value, int id)
        {
            var mapper = GetMapper();
            var member = mapper.Map<Member>(value);

            return Json(_meetingBLL.AddMeetingMember(member, id));
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MeetingModel, Meeting>();
                cfg.CreateMap<Meeting, MeetingModel>();
                cfg.CreateMap<Member, MemberModel>();
                cfg.CreateMap<MemberModel, Member>();
            });

            return config.CreateMapper();
        }
    }
}
