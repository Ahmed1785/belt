using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;



namespace belt.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        // [DataType(DataType.Date)]
        public DateTime Time { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [ValidateDate]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        public User Planner { get; set; }

        public List<Participant> Participants { get; set; }

        // public List<User> User { get; set; }

        public int UserId {get; set;}

        public Activity()
        {
            Participants = new List<Participant>();
        }

    }

    public class ValidateDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime Today = DateTime.Now; 
            if(value is DateTime)
            {
                DateTime InputDate = (DateTime)value;
                if (InputDate > Today)
                {
                    return ValidationResult.Success;
                } else {
                    return new ValidationResult("Cannot have your activity in the past");
                }
            }
        return new ValidationResult("Please enter valid date");
    }
}

  
}