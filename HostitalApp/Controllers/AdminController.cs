using AutoMapper;
using HospitalApp.Data;
using HospitalApp.DTO;
using HospitalApp.Models;
using HospitalApp.Repositories;
using HospitalApp.Services;
using HospitalApp.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Controllers
{
	[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
		public List<UserDTO>? UsersDTO { get; set; } = new();
		public List<Error> ErrorArray { get; set; } = new();
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;


		private readonly IMapper? _mapper;
		private readonly IApplicationService _applicationService;
		private readonly UsersDoctorsPatientDbContext _dbContext;

		public AdminController(IApplicationService applicationService, UsersDoctorsPatientDbContext dbContext) : base()
		{
			_applicationService = applicationService;
			_dbContext = dbContext;
		}

        public IActionResult Index()
		{
			return View();
		}
    }
}
