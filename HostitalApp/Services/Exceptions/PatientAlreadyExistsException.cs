namespace HospitalApp.Services.Exceptions
{
    public class PatientAlreadyExistsException : Exception
    {
        public PatientAlreadyExistsException(string s) : base(s) 
        { 
        }
    }
}
