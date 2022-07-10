using System;
using System.Collections.Generic;

namespace Assignment_06
{
    /// <summary>
    /// Class <c>TaskManager</c> holds the current list of todo-tasks, providing tools 
    /// for managing the list.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is responsible for keeping the user-created list of todo-tasks. The class
    /// provides methods for adding, deleting, changing and getting task data, as well as 
    /// methods for calling the <c>FileManager</c> to read / write to file.
    /// </para>
    /// <para>
    /// Creating a new object of this class will create a new List that can hold objects
    /// of type Task.
    /// </para>
    /// </remarks>
    internal class TaskManager
    {
        List<Task> taskList;

        public TaskManager()
        {
            taskList = new List<Task>();
        }

        /// <summary>
        /// Method <c>GetTask</c> gets a Task object reference from the list.
        /// </summary>
        /// <param name="index">The index at which to get the Task object reference.</param>
        /// <returns>A Task object reference.</returns>
        public Task GetTask(int index)
        {
            if (CheckIndex(index))
                return taskList[index];
            else
                return null;
        }

        /// <summary>
        /// Method <c>CheckIndex</c> checks whether the parameter-provided value is 
        /// within the bounds of the task list index span.
        /// </summary>
        /// <param name="index">The integer value to test against the task list index.</param>
        /// <returns><c>true</c> if the provided value is within the bounds of the 
        /// task list index span.</returns>
        private bool CheckIndex(int index)
        {
            if (index >= 0 && index < taskList.Count)
                return true;
            
            return false;
        }

        /// <summary>
        /// Adds a new user-defined task to the list.
        /// </summary>
        /// <param name="newTask">The reference to an object of type Task.</param>
        /// <returns><c>true</c> if the Task was added successfully to the list.</returns>
        public bool AddNewTask(Task newTask)
        {
            if (newTask != null)
            {
                taskList.Add(newTask);
                return true;
            }
            return false;
        }

        /// <summary>
        /// An overloaded variant that takes specific Task data as parameters, instead of taking a Task object.
        /// </summary>
        /// <param name="newTime">User-provided date and time.</param>
        /// <param name="description">User-provided description.</param>
        /// <param name="priority">User-provided priority.</param>
        /// <returns><c>true</c> if the Task was added successfully to the list.</returns>
        public bool AddNewTask(DateTime newTime, string description, PriorityType priority)
        {
            Task newTask = new Task(newTime, description, priority);

            if (newTask != null)
            {
                taskList.Add(newTask);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Another overloaded variant that takes a complete set of Task data as parameters, instead of taking a Task object.
        /// </summary>
        /// <param name="newTime">User-provided date and time.</param>
        /// <param name="description">User-provided description.</param>
        /// <param name="descriptionMultiline">User-provided multiline description.</param>
        /// <param name="multilineCount">The number of lines in the multiline description.</param>
        /// <param name="priority">User-provided priority.</param>
        /// <returns><c>true</c> if the Task was added successfully to the list.</returns>
        public bool AddNewTask(DateTime newTime, string description, string descriptionMultiline, int multilineCount, PriorityType priority)
        {
            Task newTask = new Task(newTime, description, descriptionMultiline, multilineCount, priority);

            if (newTask != null)
            {
                taskList.Add(newTask);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes a Task in the list.
        /// </summary>
        /// <param name="index">Integer value of the index at which to change.</param>
        /// <param name="task">A reference to a Task object to replace with.</param>
        /// <returns><c>true</c> if the Task was changed successfully (it was not null, and the index was within bounds).</returns>
        public bool ChangeTaskAt(int index, Task task)
        {
            if (task != null && CheckIndex(index))
            {
                taskList[index] = task;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes a task in the list.
        /// </summary>
        /// <param name="index">Integer value of the index at which to delete.</param>
        /// <returns><c>true</c> if the Task was deleted from the list.</returns>
        public bool DeleteTaskAt(int index)
        {
            if (CheckIndex(index))
            {
                taskList.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets an array of strings, formatted for the task list.
        /// </summary>
        /// <returns>An array of strings.</returns>
        public string[] GetInfoStringsArray()
        {
            string[] infoStrings = new string[taskList.Count];

            for (int i = 0; i < infoStrings.Length; i++)
            {
                infoStrings[i] = taskList[i].ToString();
            }
            return infoStrings;
        }

        /// <summary>
        /// Calls the file manager in order to write the task list to a file.
        /// </summary>
        /// <param name="fileName">A string containing the full path and name of the file to write.</param>
        public void WriteDataToFile(string fileName)
        {
            FileManager fileManager = new FileManager();
            fileManager.SaveTaskListToFile(taskList, fileName);
        }

        /// <summary>
        /// Calls the file manager in order to read tasks from a file, and store them in the task list.
        /// </summary>
        /// <param name="fileName">A string containing the full path and name of the file to read from.</param>
        public void ReadDataFromFile(string fileName)
        {
            FileManager fileManager = new FileManager();
            fileManager.ReadTaskListFromFile(taskList, fileName);
        }
    }
}
