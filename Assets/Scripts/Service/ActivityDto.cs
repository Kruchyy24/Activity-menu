using Newtonsoft.Json;

namespace Service
{
    public class ActivityDto
    {
    
        [JsonProperty(PropertyName = "activity")]
        public string TextActivity { get; set; }
        
        [JsonProperty(PropertyName = "type")]
        public string TextType { get; set; }
        
        [JsonProperty(PropertyName = "participants")]
        public int TextParticipants { get; set; }
        
        [JsonProperty(PropertyName = "price")]
        public float TextPrice { get; set; }
        
        [JsonProperty(PropertyName = "link")]
        public string TextLink { get; set; }
        
        [JsonProperty(PropertyName = "key")]
        public int TextKey { get; set; }
        
        [JsonProperty(PropertyName = "accessibility")]
        public float TextAccessibility { get; set; }
    }
}
