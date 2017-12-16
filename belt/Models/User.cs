using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;



namespace belt.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(3)]
        public string First_Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Last_Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }

        public List<Participant> Participants {get; set;}

        public User()
        {
            Participants = new List<Participant>();
        }

    }

    public class LoginUser
    {


        [Key]
        public long loguser_id { get; set; }

        [Required]
        [EmailAddress]
        public string LogEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string LogPassword { get; set; }

    }

      


    }