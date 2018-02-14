using System.Collections.Generic;

namespace HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Prescriptions = new List<PatientMedicament>();
            this.Visitations = new List<Visitation>();
            this.Diagnoses = new List<Diagnose>();
        }

        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool HasInsurance { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }
    }
}