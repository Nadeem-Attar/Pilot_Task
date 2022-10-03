using PilotTask.Models;
using PilotTask.Repository;
using PilotTask.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace PilotTask.Controllers
{
    public class ProfileController : Controller
    {
        #region CTOR & PROPERTIES
        private readonly ProfileRepository profileRepository;
        public ProfileController()
        {
            profileRepository = new ProfileRepository();
        }
        #endregion

        #region GET
        public ActionResult GetAllProfiles()
        {
            ModelState.Clear();

            var profiles = profileRepository.GetAllProfiles();
            return View(profiles.Select(profile => new ProfileViewModel
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DateOfBirth = profile.DateOfBirth,
                PhoneNumber = profile.PhoneNumber,
                EmailId = profile.EmailId
            }).ToList());
        }
        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            ModelState.Clear();

            var profile = profileRepository.GetProfile(id);
            return View(new ProfileViewModel
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DateOfBirth = profile.DateOfBirth,
                PhoneNumber = profile.PhoneNumber,
                EmailId = profile.EmailId
            });
        }
        #endregion

        #region ADD
        [HttpGet]
        public ActionResult AddProfile()
        {
            return View(new ProfileViewModel());
        }
        [HttpPost]
        public ActionResult AddProfile(ProfileViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (profileRepository.AddProfile(new Profile
                    {
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        DateOfBirth = vm.DateOfBirth,
                        PhoneNumber = vm.PhoneNumber,
                        EmailId = vm.EmailId,
                    }))
                    {
                        ViewBag.Message = "Profile added successfully";
                    }
                }

                return View(new ProfileViewModel());
            }
            catch
            {
                return View(new ProfileViewModel());
            }
        }
        #endregion

        #region EDIT
        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel vm)
        {
            try
            {
                profileRepository.UpdateProfile(new Profile
                {
                    Id = vm.Id,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    DateOfBirth = vm.DateOfBirth,
                    PhoneNumber = vm.PhoneNumber,
                    EmailId = vm.EmailId
                });
                return RedirectToAction(nameof(GetAllProfiles));
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region DELETE
        public ActionResult DeleteProfile(int id)
        {
            try
            {
                if (profileRepository.DeleteProfile(id))
                {
                    ViewBag.AlertMsg = "Profile deleted successfully";

                }
                return RedirectToAction(nameof(GetAllProfiles));

            }
            catch
            {
                ViewBag.Message = "you can not remove this profile because he has a tasks";

                return RedirectToAction(nameof(GetAllProfiles));
            }
        }
        #endregion
    }
}
