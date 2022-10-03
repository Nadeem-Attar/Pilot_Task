using PilotTask.DB;
using PilotTask.Models;
using PilotTask.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PilotTask.Repository
{
    public class TaskRepository : ConnectionDB
    {
        private SqlConnection sqlConnection;

        #region METHODS
        public bool AddTask(Task taskObject)
        {

            sqlConnection = SqlConnectionDB();
            SqlCommand com = new SqlCommand(
                TaskStoredProcedureNames.AddNewTask, sqlConnection);
            com.CommandType = CommandType.StoredProcedure;

            foreach (var propertyInfo in taskObject.GetType().GetProperties())
            {
                if (propertyInfo.Name != "Id")
                {
                    com.Parameters.AddWithValue($"@{propertyInfo.Name}",
                        propertyInfo.GetValue(taskObject, null));
                }
            }
            sqlConnection.Open();
            int i = com.ExecuteNonQuery();
            sqlConnection.Close();

            if (i >= 1)
                return true;

            return false;
        }
        public Task GetTask(int id)
        {
            sqlConnection = SqlConnectionDB();

            Task oneTask = null;
            SqlCommand com = new SqlCommand(
                TaskStoredProcedureNames.GetTask, sqlConnection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            sqlConnection.Open();
            da.Fill(dt);
            sqlConnection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                oneTask = new Task
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["Name"]),
                    Description = Convert.ToString(dr["Description"]),
                    StartTime = Convert.ToDateTime(dr["StartTime"]),
                    Status = Convert.ToInt32(dr["Status"]),
                    ProfileId = Convert.ToInt32(dr["ProfileId"])
                };
            }

            return oneTask;
        }
        public List<Task> GetTasksByProfileId(int profileId)
        {
            sqlConnection = SqlConnectionDB();
            var TaskList = new List<Task>();

            SqlCommand com = new SqlCommand(
                TaskStoredProcedureNames.GetTasksByProfileId, sqlConnection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProfileId", profileId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            sqlConnection.Open();
            da.Fill(dt);
            sqlConnection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                TaskList.Add(
                    new Task
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Description = Convert.ToString(dr["Description"]),
                        StartTime = Convert.ToDateTime(dr["StartTime"]),
                        Status = Convert.ToInt32(dr["Status"]),
                        ProfileId = Convert.ToInt32(dr["ProfileId"]),
                    }
                    );
            }

            return TaskList;
        }
        public bool UpdateTask(Task taskObject)
        {

            sqlConnection = SqlConnectionDB();
            SqlCommand com = new SqlCommand(
                TaskStoredProcedureNames.UpdateTask, sqlConnection);

            com.CommandType = CommandType.StoredProcedure;
            foreach (var propertyInfo in taskObject.GetType().GetProperties())
            {
                com.Parameters.AddWithValue($"@{propertyInfo.Name}",
                        propertyInfo.GetValue(taskObject, null));
            }
            sqlConnection.Open();
            int i = com.ExecuteNonQuery();
            sqlConnection.Close();

            if (i >= 1)
                return true;

            return false;
        }
        public bool DeleteTask(int Id)
        {
            sqlConnection = SqlConnectionDB();
            SqlCommand com = new SqlCommand(
                TaskStoredProcedureNames.DeleteTaskById, sqlConnection);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);

            sqlConnection.Open();
            int i = com.ExecuteNonQuery();
            sqlConnection.Close();
            if (i >= 1)
                return true;

            return false;
        }
        #endregion
    }
}