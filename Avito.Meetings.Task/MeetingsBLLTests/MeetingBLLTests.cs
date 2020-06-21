using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingsBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Moq;
using MeetingsDAO;

namespace MeetingsBLL.Tests
{
    [TestClass()]
    public class MeetingBLLTests
    {
        private const string timeErrorMessage = "The start of the meeting must be ealier than the end of the meeting";
        private const string requiredFieldsMessage = "StartTime and EndTime fields are required";
        private const string requestErrorMessage = "Incorrect request format";
        private const string requiredEmailErrorMessage = "Email is required";
        private const string incorrectEmailErrorMessage = "Incorrect email format for member";

        [TestMethod()]
        public void AddMeetingTest_MeetingAddedCorrectly()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
                StartTime = new DateTime(2020, 08, 08, 12, 30, 0),
                EndTime = new DateTime(2020, 08, 08, 13, 00, 0),
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsFalse(result.Any(), "Expected empty error collection");
        }

        [TestMethod()]
        public void AddMeetingTest_IncorrectTimeFormat()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
                StartTime = new DateTime(2020, 08, 08, 16, 30, 0),
                EndTime = new DateTime(2020, 08, 08, 13, 00, 0),
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(timeErrorMessage, result.First(), "Expected error message");
        }

        [TestMethod()]
        public void AddMeetingTest_EmptyStartTime()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
                EndTime = new DateTime(2020, 08, 08, 13, 00, 0),
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(requiredFieldsMessage, result.First(), "Expected error message");
        }


        [TestMethod()]
        public void AddMeetingTest_EmptyEndTime()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
                StartTime = new DateTime(2020, 08, 08, 13, 00, 0),
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(requiredFieldsMessage, result.First(), "Expected error message");
        }

        [TestMethod()]
        public void AddMeetingTest_BothEmptyTimeFields()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(requiredFieldsMessage, result.First(), "Expected error message");
        }

        [TestMethod()]
        public void AddMeetingTest_NullMeetingValue()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = null;

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(requestErrorMessage, result.First(), "Expected error message");
        }

        [TestMethod()]
        public void AddMeetingTest_EmptyMemberEmail()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
                StartTime = new DateTime(2020, 08, 08, 12, 30, 0),
                EndTime = new DateTime(2020, 08, 08, 13, 00, 0),
                Members = new List<Member>()
                {
                    new Member()
                    {
                        FirstName = "user1"
                    }
                }
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsTrue(result.Any(), "Error expected");
            Assert.AreEqual(requiredEmailErrorMessage, result.First());
        }

        [TestMethod()]
        public void AddMeetingTest_IncorrectEmailFormat()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
                StartTime = new DateTime(2020, 08, 08, 12, 30, 0),
                EndTime = new DateTime(2020, 08, 08, 13, 00, 0),
                Members = new List<Member>()
                {
                    new Member()
                    {
                        FirstName = "user1",
                        Email = "user1gmail.com"
                    }
                }
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsTrue(result.Any(), "Error expected");
        }

        [TestMethod()]
        public void AddMeetingTest_AddedCorrectlyWithMemberCollection()
        {
            var meetindDao = new Mock<IMeetingDAO>();
            var meetingLogic = new MeetingBLL(meetindDao.Object);

            Meeting meeting = new Meeting()
            {
                Id = 1,
                StartTime = new DateTime(2020, 08, 08, 12, 30, 0),
                EndTime = new DateTime(2020, 08, 08, 13, 00, 0),
                Members = new List<Member>()
                {
                    new Member()
                    {
                        FirstName = "user1",
                        Email = "user1@gmail.com"
                    }
                }
            };

            var result = meetingLogic.AddMeeting(meeting);

            Assert.IsFalse(result.Any());
        }
    }
}