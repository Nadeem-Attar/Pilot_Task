using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PilotTask.DB;
using PilotTask.Models;
using PilotTask.StoredProcedures;

namespace PilotTask.Repository
{
    public class ProfileRepository : ConnectionDB
    {
        private SqlConnection sqlConnection;

        #region METHODS
        public bool AddProfile(Profile profileObject)
        {
            try
            {
                sqlConnection = SqlConnectionDB();
                SqlCommand com = new SqlCommand(
                    ProfileStoredProcedureNames.AddNewProfile, sqlConnection);
                com.CommandType = CommandType.StoredProcedure;

                foreach (var propertyInfo in profileObject.GetType().GetProperties())
                {
                    if (propertyInfo.Name != "Id")
                    {
                        com.Parameters.AddWithValue($"@{propertyInfo.Name}",
                            propertyInfo.GetValue(profileObject, null));
                    }
                }
                sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                sqlConnection.Close();
                if (i >= 1)
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Profile GetProfile(int id)
        {
            sqlConnection = SqlConnectionDB();

            Profile oneProfile = null;
            SqlCommand com = new SqlCommand(
                ProfileStoredProcedureNames.GetProfile, sqlConnection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            sqlConnection.Open();
            da.Fill(dt);
            sqlConnection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                oneProfile = new Profile
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                    PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                    EmailId = Convert.ToString(dr["EmailId"])
                };
            }

            return oneProfile;
        }
        public List<Profile> GetAllProfiles()
        {
            sqlConnection = SqlConnectionDB();
            var ProfileList = new List<Profile>();


            SqlCommand com = new SqlCommand(
                ProfileStoredProcedureNames.GetProfiles, sqlConnection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            sqlConnection.Open();
            da.Fill(dt);
            sqlConnection.Close();
            foreach (DataRow dr in dt.Rows)
            {

                ProfileList.Add(
                    new Profile
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                        PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                        EmailId = Convert.ToString(dr["EmailId"])
                    }
                    );
            }

            return ProfileList;
        }
        public bool UpdateProfile(Profile profileObject)
        {
            sqlConnection = SqlConnectionDB();
            SqlCommand com = new SqlCommand(
                ProfileStoredProcedureNames.UpdateProfile, sqlConnection);

            com.CommandType = CommandType.StoredProcedure;

            foreach (var propertyInfo in profileObject.GetType().GetProperties())
            {
                com.Parameters.AddWithValue($"@{propertyInfo.Name}",
                         propertyInfo.GetValue(profileObject, null));
            }
            sqlConnection.Open();
            int i = com.ExecuteNonQuery();
            sqlConnection.Close();
            if (i >= 1)
                return true;

            return false;
        }
        public bool DeleteProfile(int Id)
        {
            sqlConnection = SqlConnectionDB();
            SqlCommand com = new SqlCommand(
                ProfileStoredProcedureNames.DeleteProfileById, sqlConnection);

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