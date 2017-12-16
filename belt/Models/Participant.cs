using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;



namespace belt.Models
{
    
    public class Participant
    {
        public int UserId { get; set; }

        public int ActivityId { get; set; }

        [Key]
        public int ParticipantId { get; set; }

        public User Guest { get; set; }

    }
}