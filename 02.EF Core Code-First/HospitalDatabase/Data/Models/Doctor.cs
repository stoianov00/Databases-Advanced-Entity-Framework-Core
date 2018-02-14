using System.Collections.Generic;

namespace HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Visitations = new List<Visitation>();
        }

        public int DoctorId { get; set; }   

        public string Name { get; set; }

        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

    }
}