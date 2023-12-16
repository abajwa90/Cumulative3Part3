using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniviableProject.Models
{
    public class Teacher
    {
   
        public int id { get; set; } 
       
        public string firstName { get; set; }
    
        public string lastName { get; set; }
      
        public string employeeNumber { get; set; }

        public string hireDate { get; set; }
  
        public decimal salary { get; set; }

        public bool IsValid()
        {
            bool valid = true;

            if (firstName == null || lastName == null || employeeNumber == null || salary <=0 )
            {
           
                valid = false;
            }

            return valid;
        }
    }
}