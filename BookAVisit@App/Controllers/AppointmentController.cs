using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAVisit_App.DataContext;
using BookAVisit_App.Models;
using BookAVisit_App.Repositories;
using BookAVisit_App.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace BookAVisit_App.Controllers
{
    public class AppointmentController : Controller
    {// atrybut readonly uniemożliwia przypadkowe nadanie nowej wartości polu _appointmentRepository
        private readonly AppointmentRepository _appointmentRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        public AppointmentController(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            this.userManager = userManager;
            this.context = context;
        }

        // Tutaj wyświetlam wszystkie wizyty
        public ViewResult GetAllAppointment()
        {
            var model = _appointmentRepository.GetAllAppointment();
            ViewBag.TerazJest = DateTime.Now;
            return View(model);
            ;
        }

        //Wizytę umawia Pacjent - get
        [HttpGet]
        public ViewResult CreateAppointment(int id)
        {
            AppointmentViewModel appointment = new AppointmentViewModel();
            appointment.DoctorID = _doctorRepository.GetDoctor(id).DoctorID;
            //parsowanie do int
            appointment.UserID = userManager.GetUserId(User);
            return View(appointment);
        }

        // Wizytę umawia Pacjent - post
        [HttpPost]
        public RedirectToActionResult CreateAppointment(AppointmentViewModel appointment)
        {
            if (context.Appointments.Where(x => x.AppointmentDate == appointment.AppointmentDate && x.Doctor.DoctorID == appointment.Doctor.DoctorID).Any())
            {
                return RedirectToAction("AppointmentExists");
            }

            else
            {
                Models.Appointment appointmentModel = new Models.Appointment();

                appointmentModel.Doctor = context.Doctors.FirstOrDefault(x => x.DoctorID == appointment.Doctor.DoctorID);
                appointmentModel.UserName = userManager.GetUserName(User);
                appointmentModel.AppointmentDate = appointment.AppointmentDate;

                Appointment newAppointment = _appointmentRepository.Add(appointmentModel);
                context.SaveChanges();
            }
            return RedirectToAction("MyAppointment");
        }


        public IActionResult AppointmentExists()
        {
            return View();
        }

        // Tutaj Pacjent umawia wizytę
        public IActionResult Appointment(int id)
        {
            AppointmentViewModel appointmentViewModel = new AppointmentViewModel()
            {
                UserID = userManager.GetUserId(User),
                Doctor = _doctorRepository.GetDoctor(id),
                DoctorID = _doctorRepository.GetDoctor(id).DoctorID,
            };

            return View(appointmentViewModel);
        }


        // Po umówieniu wizyty, Pacjent przechodzi do Podsumowania wizyty - AppointmentDetails
        public ViewResult AppointmentDetails(int id)
        {
            AppointmentDetailsViewModel appointmentDetailsViewModel = new AppointmentDetailsViewModel()
            {

                UserID = userManager.GetUserId(User),
                Appointment = _appointmentRepository.GetAppointment(id),
                Doctor = _doctorRepository.GetDoctor(id),
            };

            return View(appointmentDetailsViewModel);
        }

        // Po umówieniu wizyty, podsumowanie można zapisać do PDF
        public async Task<IActionResult> AppointmentPDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return new ViewAsPdf(appointment);
        }

        // Tutaj wyświetlają się wizyty tylko dla zalogowanego pacjenta
        public async Task<IActionResult> MyAppointment(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var appointment = _appointmentRepository.GetAllAppointment();
            appointment = appointment.Where(p => p.UserName == user.UserName);
            ViewBag.TerazJest = DateTime.Now;
            return View(appointment);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        // Edytuję wizytę
        [HttpGet]
        public ViewResult EditAppointment(int id)
        {
            Appointment appointment = _appointmentRepository.GetAppointment(id);
            AppointmentEditViewModel appointmentEditViewModel = new AppointmentEditViewModel
            {
                AppointmentID = appointment.AppointmentID,
                DoctorID = appointment.DoctorID,
                UserName = appointment.UserName,
                AppointmentDate = appointment.AppointmentDate

            };
            return View(appointmentEditViewModel);
        }

        // Dokonuję zmian w edytowanej wizycie
        [HttpPost]
        public IActionResult EditAppointment(AppointmentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Appointment appointment = _appointmentRepository.GetAppointment(model.AppointmentID);
                appointment.DoctorID = model.DoctorID;
                appointment.UserName = model.UserName;
                appointment.AppointmentDate = model.AppointmentDate;

                _appointmentRepository.Update(appointment);

                return RedirectToAction("MyAppointment");
            }
            return View();
        }

        // Pokazuje szczegóły dla wizyty
        public async Task<IActionResult> DetailAppointment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // Usuwa wizytę
        public ActionResult DeleteAppointment(int id)
        {
            // before deleting a Pacjent, we need to find them first
            Appointment appointment = context.Appointments.Find(id);
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                context.SaveChanges();
            }
            return RedirectToAction("MyAppointment");
        }

        private bool AppointmentExists(int id)
        {
            return context.Appointments.Any(e => e.AppointmentID == id);
        }
    }
}