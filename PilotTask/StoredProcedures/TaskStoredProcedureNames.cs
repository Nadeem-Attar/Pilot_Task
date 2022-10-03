
namespace PilotTask.StoredProcedures
{
    public static class TaskStoredProcedureNames
    {
        public static string AddNewTask => nameof(AddNewTask);
        public static string GetTask => nameof(GetTask);
        public static string GetTasksByProfileId => nameof(GetTasksByProfileId);
        public static string UpdateTask => nameof(UpdateTask);
        public static string DeleteTaskById => nameof(DeleteTaskById);
    }
}