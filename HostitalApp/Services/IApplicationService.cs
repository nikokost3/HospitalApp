﻿namespace HospitalApp.Services
{
    public interface IApplicationService
    {
        UserService UserService { get; }
        PatientService PatientService { get; }
        DoctorService DoctorService { get; }
    }
}
