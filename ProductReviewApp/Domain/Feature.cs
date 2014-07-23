namespace ProductReviewApp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Feature")]
    public partial class Feature
    {
        public Feature()
        {
            Feedback = new HashSet<Feedback>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(64)]
        public string Image { get; set; }

        public bool IsRequest { get; set; }

        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Votes { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
