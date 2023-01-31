using Newtonsoft.Json;

namespace MailGraphAnalysis.Entity.JSON
{
    public partial class Image
    {
        [JsonProperty("thumb")]
        public string Thumb { get; set; }
    }
}
