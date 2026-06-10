using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;
using System.Text.Json.Serialization;

namespace DevetionStudetns.DTO.firebase
{
    public class GEtMessageDto
    {
        public string SenderId { get; set; }
        public string ReseverId { get; set; }
        public string Contant { get; set; }
        [JsonIgnore]
        public DateTime DateSend { get; set; } = DateTime.Now;
        public string DateTimeSend => DateSend.ToString("yyyy-MM-dd / HH:mm:ss");
    }
}
