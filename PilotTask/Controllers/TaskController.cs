
using PilotTask.Models;
using PilotTask.Repository;
using PilotTask.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace PilotTask.Controllers
{
    public class TaskController : Controller
    {
        #region CTOR & PROPERTIES
        private readonly TaskRepository taskRepository;
        public static int ProfileId = 0;
        public TaskController()
        {
            taskRepository = new TaskRepository();
        }
        #endregion

        #region GET
        public ActionResult GetTasksByProfileId(int profileId)
        {
            ModelState.Clear();

            var tasks = taskRepository.GetTasksByProfileId(profileId);

            ProfileId = profileId;
            return View(tasks.Select(task => new TaskViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                StartTime = task.StartTime,
                Status = task.Status,
                ProfileId = task.ProfileId
            }).ToList());
        }
        [HttpGet]
        public ActionResult EditTask(int id)
        {
            ModelState.Clear();

            var task = taskRepository.GetTask(id);

            return View(new TaskViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                StartTime = task.StartTime,
                Status = task.Status,
                ProfileId = task.ProfileId
            });
        }
        #endregion

        #region ADD
        [HttpGet]
        public ActionResult AddTask()
        {
            return View(new TaskViewModel { ProfileId = ProfileId });
        }
        [HttpPost]
        public ActionResult AddTask(TaskViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (taskRepository.AddTask(new Task
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        StartTime = vm.StartTime,
                        Status = vm.Status,
                        ProfileId = vm.ProfileId
                    }))
                    {
                        ViewBag.Message = "Task added successfully";
                    }
                }

                return View(new TaskViewModel { ProfileId = ProfileId });
            }
            catch
            {
                return View(new TaskViewModel { ProfileId = ProfileId });
            }
        }
        #endregion

        #region EDIT
        [HttpPost]
        public ActionResult EditTask(TaskViewModel vm)
        {
            try
            {
                taskRepository.UpdateTask(new Task
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Description = vm.Description,
                    StartTime = vm.StartTime,
                    Status = vm.Status,
                    ProfileId = vm.ProfileId
                });
                return RedirectToAction(nameof(GetTasksByProfileId),
                    new { profileId = vm.ProfileId });
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region DELETE
        public ActionResult DeleteTask(int id)
        {
            try
            {
                if (taskRepository.DeleteTask(id))
                {
                    ViewBag.AlertMsg = "Task deleted successfully";

                }
                return RedirectToAction(nameof(GetTasksByProfileId),
                    new { profileId = ProfileId });
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
