using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecagonAuthorTest.Models.Response
{
    public class UsernameResponse
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        public UsernameResponse()
        {
            Data = new List<Datum>();
        }
    }

    public partial class Datum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("submitted")]
        public long Submitted { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("submission_count")]
        public long SubmissionCount { get; set; }

        [JsonProperty("comment_count")]
        public long CommentCount { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }
    }
}
