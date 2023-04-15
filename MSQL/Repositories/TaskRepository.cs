﻿using Dapper;
using Microsoft.Data.SqlClient;
using BusinessLogic.Models;
using BusinessLogic.Repositories;

namespace MSQL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private string connectionString = "Data Source=GBVPC; Initial Catalog=ToDoList; Integrated Security=True; TrustServerCertificate=True";

        public TaskModel GetById(int id)
        {
            string query = @"
                SELECT *
                FROM Tasks
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<TaskModel>(query, new { Id = id });
            }
        }

        public IEnumerable<TaskModel> GetTasksList()
        {
            string query = @"
                SELECT * FROM Tasks
                ORDER BY CASE
	                WHEN DueDate IS NULL THEN 1
	                ELSE 0
	                END,
	                DueDate
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<TaskModel>(query).ToList();
            }
        }

        public int Create(TaskModel task)
        {
            string query = @"
                INSERT INTO Tasks (Title, DueDate, CategoryId)
                VALUES (@Title, @DueDate, @CategoryId)
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(query, task);
            }
        }

        public int Complete(int id)
        {
            string query = @"
                UPDATE Tasks
                SET IsCompleted = IsCompleted ^ 1
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(query, new { Id = id });
            }
        }

        public int Delete(int id)
        {
            string query = @"
                DELETE Tasks
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(query, new { Id = id });
            }
        }
    }
}
