﻿namespace HospitalApp.Services.Exceptions
{
    public class DoctorAlreadyExistsException : Exception
    {
        public DoctorAlreadyExistsException(string s) : base(s)
        {
        }
    }
}
