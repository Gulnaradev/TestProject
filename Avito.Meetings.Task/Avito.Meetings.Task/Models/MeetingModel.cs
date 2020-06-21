using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avito.Meetings.Task.Models
{
    public class MeetingModel
    {
        public int Id { get; set; }

        [JsonProperty("starttime")]
        public DateTime StartTime { get; set; }

        [JsonProperty("endtime")]
        public DateTime EndTime { get; set; }

        [JsonProperty("members")]
        public ICollection<MemberModel> Members { get; set; }
    }
}