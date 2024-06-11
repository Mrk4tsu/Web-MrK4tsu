namespace Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        public long Id { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifyAt { get; set; }

        public bool? Status { get; set; }

        public virtual Account Account { get; set; }
    }
}
