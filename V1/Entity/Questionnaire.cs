using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using testDtoAndmapper.Entity;

namespace BuildDB_Team.entitys
{
    [Table("Questionnaire")]
    public class Questionnaire
    {
        [Key]
        public int QuestionnaireId { get; set; }
        [Column("UserId")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        [Column("QuestionnaireName")]
        public string QuestionnaireName { get; set; }

        [Required]
        [Column("ConcernedParty")]
        public int ConcernedParty { get; set; }

        [Required]
        [Column("LinkQuestionnaire")]
        public string LinkQuestionnaire { get; set; }

        [Required]
        public int QuestionnaireStatus { get; set; }

        [Required]
        [Column("DeliveryDate")]
        public string DeliveryDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        public string QuestionnaireType { get; set; }
    }
}
