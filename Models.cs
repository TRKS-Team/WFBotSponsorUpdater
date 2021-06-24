using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WFBotSponsorUpdater
{
    public partial class ApiResult
    {
        public long Ec { get; set; }
        public string Em { get; set; }
        public Data Data { get; set; }
    }

    public partial class Data
    {
        public long HasMore { get; set; }
        [JsonPropertyName("list")]
        public User[] User { get; set; }
    }

    public partial class User
    {
        public string UserId { get; set; }
        public long Status { get; set; }
        public string Name { get; set; }
        public Uri Avatar { get; set; }
        public string Cover { get; set; }
        public string UrlSlug { get; set; }
        public long IsVerified { get; set; }
        public long VerifiedType { get; set; }
        public long IsNotRec { get; set; }
        public long ShowSponsoring { get; set; }
        public long HasMark { get; set; }
    }
    
}
