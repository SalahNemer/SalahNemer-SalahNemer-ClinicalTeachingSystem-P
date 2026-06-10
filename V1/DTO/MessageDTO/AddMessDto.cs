using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.DTO.firebase
{
    public class AddMessDto
    {
        public string SenderId { get; set; }
        public string ReseverId { get; set; }
        public string Contant { get; set; }
    }
}
