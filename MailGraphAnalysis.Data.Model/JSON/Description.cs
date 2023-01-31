using Newtonsoft.Json;

namespace MailGraphAnalysis.Entity.JSON
{
    public partial class Description
    {
        [JsonProperty("en")]
        public string En { get; set; }
    }
}
