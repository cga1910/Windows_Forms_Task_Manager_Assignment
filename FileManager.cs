using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment_06
{
    /// <summary>
    /// Class <c>FileManager</c> provides methods to read and write the user-created
    /// list of tasks to a file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The field <c>fileVersionToken</c> holds a string to be written as the first
    /// line of the file. It marks the file as written by this application.
    /// </para>
    /// <para>
    /// The field <c>fileVersionNr</c> holds a double to be written to the second
    /// line of the file. It specifies the file version.
    /// </para>
    /// <para>
    /// This class is based on example code by instructor Farid Naisan. It has been refactored, 
    /// and modified to include reading and writing of a multiline task description. The save/read 
    /// methods now return / throw exceptions only. These are catched higher up the try-catch 
    /// hierarchy, in the MainForm class, in order to display more informative error messages.
    /// </para>
    /// </remarks>
    internal class FileManager
    {
        private const string fileVersionToken = "ToDo_v1.0";
        private const double fileVersionNr = 1.0;
        private StreamReader reader; // Global reader to avoid having to pass it as a parameter many times


        // METHODS FOR WRITING TO FILE ---------------------------------------------------------

        /// <summary>
        /// Method <c>SaveTaskListToFile</c> tries to save the user-created task list to a file.
        /// </summary>
        /// <param name="taskList">A reference to the List of Task objects to be saved to file.</param>
        /// <param name="fileName">A string containing the full path and filename to be written to.</param>
        public void SaveTaskListToFile(List<Task> taskList, string fileName)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);

                writer.WriteLine(fileVersionToken); // write line 1
                writer.WriteLine(fileVersionNr);    // write line 2
                writer.WriteLine(taskList.Count);   // write line 3

                WriteTasks(taskList, writer);
            }
            catch (Exception e)
            {
                throw e; // Throws the exception up the chain
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        /// <summary>
        /// Method <c>WriteTasks</c> writes every task in the user-created task list to file.
        /// </summary>
        /// <remarks>
        /// The multiline task description is not written to file if empty.
        /// </remarks>
        /// <param name="taskList">A reference to the List of Task objects to be saved to file.</param>
        /// <param name="writer">A reference to the StreamWriter to be used for writing lines to file.</param>
        private void WriteTasks(List<Task> taskList, StreamWriter writer)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                writer.WriteLine(taskList[i].Description);
                writer.WriteLine(taskList[i].DescriptionMultilineCount);
                WriteMultilineDescription(taskList, writer, i);
                writer.WriteLine(taskList[i].Priority.ToString());
                writer.WriteLine(taskList[i].DateAndTime.Year);
                writer.WriteLine(taskList[i].DateAndTime.Month);
                writer.WriteLine(taskList[i].DateAndTime.Day);
                writer.WriteLine(taskList[i].DateAndTime.Hour);
                writer.WriteLine(taskList[i].DateAndTime.Minute);
                writer.WriteLine(taskList[i].DateAndTime.Second);
            }
        }

        /// <summary>
        /// Method <c>WriteMultilineDescription</c> writes a task's multiline description to file,
        /// given that the multiline description contains any rows.
        /// </summary>
        /// <param name="taskList">A reference to the List of Task objects to be written to file.</param>
        /// <param name="writer">A reference to the StreamWriter to be used for writing lines to file.</param>
        /// <param name="i">The index of the Task object in the <c>taskList</c>, from where to get the multiline description.</param>
        private void WriteMultilineDescription(List<Task> taskList, StreamWriter writer, int i)
        {
            if (taskList[i].DescriptionMultilineCount > 0)
                writer.WriteLine(taskList[i].DescriptionMultiline);
        }


        // METHODS FOR READING FROM FILE -------------------------------------------------------

        /// <summary>
        /// Method <c>ReadTaskListFromFile</c> attempts to read task data from a file, and
        /// save any tasks read to the task list.
        /// </summary>
        /// <remarks>
        /// Exceptions will be catched, and thrown again to be handled in the GUI class.
        /// </remarks>
        /// <param name="taskList">A reference to the List of Task objects to save task data to.</param>
        /// <param name="fileName">A string containing the full path and filename to read from.</param>
        public void ReadTaskListFromFile(List<Task> taskList, string fileName)
        {
            reader = null;
            try
            {
                reader = new StreamReader(fileName); 

                if (IsFileCompatible())
                    ReadTasks(PrepareTaskList(taskList)); // List is cleared only if the compatibility check passes
            }
            catch (Exception e)
            {
                throw e; // Throws the exception up the chain
            }
            finally // Always executed
            {
                if (reader != null) reader.Close();
            }
        }

        /// <summary>
        /// Method <c>PrepareTaskList</c> prepares the task list.
        /// </summary>
        /// <remarks>
        /// The list will be reinstantiated if null, otherwise cleared.
        /// </remarks>
        /// <param name="taskList">A reference to the List of Task objects to prepare.</param>
        /// <returns>A reference to the now prepared List that can hold objects of type Task.</returns>
        private List<Task> PrepareTaskList(List<Task> taskList)
        {
            if (taskList != null)
                taskList.Clear();
            else
                taskList = new List<Task>();

            return taskList;
        }

        /// <summary>
        /// Method <c>IsFileCompatible</c> checks file compatibility by comparing the file's 
        /// app version token and version number with the application's values.
        /// </summary>
        /// <returns><c>true</c> if the file seems compatible with the application.</returns>
        /// <exception cref="Exception">The file might be incompatible with the application.</exception>
        private bool IsFileCompatible()
        {
            string versionTest = reader.ReadLine(); // read line 1
            double version = double.Parse(reader.ReadLine()); // read line 2

            if (versionTest == fileVersionToken && version == fileVersionNr)
            {
                return true;
            }
            else throw new Exception("Unsupported file version.");
        }

        /// <summary>
        /// Method <c>ReadTasks</c> reads all tasks in the file, adding them to the task list
        /// to be displayed in the GUI.
        /// </summary>
        /// <param name="taskList">A reference to the List of Task objects to add tasks to.</param>
        private void ReadTasks(List<Task> taskList)
        {
            int taskCount = int.Parse(reader.ReadLine()); // read line 3

            for (int i = 0; i < taskCount; i++)
            {
                Task task = new Task();

                ReadDescription(task);
                ReadMultilineDescription(task);
                ReadPriority(task);
                ReadDateAndTime(task);

                taskList.Add(task);
            }
        }

        /// <summary>
        /// Method <c>ReadDescription</c> reads a task description from the StreamReader and saves
        /// it in a Task object.
        /// </summary>
        /// <param name="task">A reference to the Task object to be updated with the task description.</param>
        private void ReadDescription(Task task)
        {
            task.Description = reader.ReadLine();
        }

        /// <summary>
        /// Method <c>ReadMultilineDescription</c> reads the task's multiline description
        /// from the StreamReader, based on the row count of the multiline description.
        /// </summary>
        /// <remarks>
        /// If the number of rows in the multiline description is zero, no reading will occur.
        /// </remarks>
        /// <param name="task">A reference to the Task object to be updated with the multiline description.</param>
        private void ReadMultilineDescription(Task task)
        {
            task.DescriptionMultilineCount = int.Parse(reader.ReadLine());

            for (int line = 0; line < task.DescriptionMultilineCount; line++)
                task.DescriptionMultiline += reader.ReadLine() + Environment.NewLine;
        }

        /// <summary>
        /// Method <c>ReadPriority</c> reads the task's priority level from the StreamReader and
        /// saves it in a Task object.
        /// </summary>
        /// <param name="task">A reference to the Task object to be updated with the priority level.</param>
        private void ReadPriority(Task task)
        {
            task.Priority = (PriorityType)Enum.Parse(typeof(PriorityType), reader.ReadLine());
        }

        /// <summary>
        /// Method <c>ReadDateAndTime</c> reads the task's date and time from the StreamReader
        /// and saves it in a Task object.
        /// </summary>
        /// <param name="task">A reference to the Task object to be updated with the date and time.</param>
        private void ReadDateAndTime(Task task)
        {
            int year = int.Parse(reader.ReadLine());
            int month = int.Parse(reader.ReadLine());
            int day = int.Parse(reader.ReadLine());
            int hour = int.Parse(reader.ReadLine());
            int minute = int.Parse(reader.ReadLine());
            int second = int.Parse(reader.ReadLine());

            task.DateAndTime = new DateTime(year, month, day, hour, minute, second);
        }

    }
}
