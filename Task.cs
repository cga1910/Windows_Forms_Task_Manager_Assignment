using System;

namespace Assignment_06
{
    /// <summary>
    /// Class <c>Task</c> constitutes a user-defined todo-task.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class holds all task specific information, such as date & time, description
    /// and priorty. These variables can be accessed through their properties.
    /// </para>
    /// <para>
    /// There are four constructor variants which makes it easy to create new objects of this
    /// class with various amounts of information known.
    /// </para>
    /// </remarks>
    internal class Task
    {
        private DateTime dateTime;
        private string description;
        private string descriptionMultiline;
        private int descriptionMultilineCount;
        private PriorityType priority;

        /// <summary>
        /// Constructor that takes no arguments.
        /// </summary>
        /// <remarks>
        /// This constructor sets the task's priority to a default value of Normal.
        /// </remarks>
        public Task()
        {
            priority = PriorityType.Normal;
        }

        /// <summary>
        /// A chained constructor that takes a DateTime object reference.
        /// </summary>
        /// <remarks>
        /// This constructor calls the next constructor in the chain, forwarding the
        /// DateTime parameter, an empty string for the task's description, and a
        /// task priority of Normal.
        /// </remarks>
        /// <param name="dateTime">A reference to an object of type DateTime.</param>
        public Task(DateTime dateTime) : this(dateTime, string.Empty, PriorityType.Normal)
        { 

        }

        /// <summary>
        /// A chained constructor that takes a DateTime object reference, a task description,
        /// and a task priority.
        /// </summary>
        /// <remarks>
        /// This constructor calls the next constructor in the chain, forwarding the
        /// DateTime parameter, the task's description string, a default value (an empty string)
        /// for the task's multiline description, a value of 0 for the number of lines in the
        /// multiline description (used for file writing/reading), and the task's priority value.
        /// </remarks>
        /// <param name="dateTime">A reference to an object of type DateTime.</param>
        /// <param name="description">A string containing a short task description.</param>
        /// <param name="priority">A task priority enum value of type PriorityType.</param>
        public Task(DateTime dateTime, string description, PriorityType priority) : this(dateTime, description, string.Empty, 0, priority)
        {
            this.dateTime = dateTime;
            this.description = description;
            this.priority = priority;
        }

        /// <summary>
        /// The last constructor in the chain, taking all the types of task data: 
        /// a DateTime variable, the task's description string, the task's multiline
        /// description string, the number of lines in the multiline string, and the
        /// task's priority value.
        /// </summary>
        /// <param name="dateTime">A reference to an object of type DateTime.</param>
        /// <param name="description">A string containing a short task description.</param>
        /// <param name="descriptionMultiline">A string containting a multiline task description.</param>
        /// <param name="descriptionMultilineCount">The number of lines in the task's multiline description.</param>
        /// <param name="priority">A task priority enum value of type PriorityType.</param>
        public Task(DateTime dateTime, string description, string descriptionMultiline, int descriptionMultilineCount, PriorityType priority)
        {
            this.dateTime = dateTime;
            this.description = description;
            this.descriptionMultiline = descriptionMultiline;
            this.descriptionMultilineCount = descriptionMultilineCount;
            this.priority = priority;
        }

        /// <summary>
        /// Property for getting and setting the <c>dateTime</c> field.
        /// </summary>
        public DateTime DateAndTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        /// <summary>
        /// Property for getting and setting the task's description. Only sets the
        /// description if the incoming value is not null or empty.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { if (!string.IsNullOrEmpty(value)) description = value; }
        }

        /// <summary>
        /// Property for getting and setting the task's multiline description. Only 
        /// sets the multiline description if the incoming value is not null or empty.
        /// </summary>
        public string DescriptionMultiline
        {
            get { return descriptionMultiline; }
            set { if (!string.IsNullOrEmpty(value)) descriptionMultiline = value; }
        }

        /// <summary>
        /// Property for getting and setting the number of lines in the task's
        /// multiline description.
        /// </summary>
        public int DescriptionMultilineCount
        {
            get { return descriptionMultilineCount; }
            set { descriptionMultilineCount = value; }
        }

        /// <summary>
        /// Property for getting and setting the task's priority level value.
        /// </summary>
        public PriorityType Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /// <summary>
        /// Method <c>GetTimeString</c> formats a string with the task's user-provided time, 
        /// used when formatting a complete task string to be displayed in the GUI.
        /// </summary>
        /// <returns>A formatted string containing the task's user-provided time.</returns>
        private string GetTimeString()
        {
            string time = string.Format("{0}:{1}", 
                dateTime.Hour.ToString("00"),
                dateTime.Minute.ToString("00"));

            return time;
        }

        /// <summary>
        /// Method <c>GetPriorityString</c> cleans up the task's priority level name, replacing
        /// all underscore characters (required in the enum) with spaces.
        /// </summary>
        /// <returns>A string containing the enum name, cleaned from underscores.</returns>
        public string GetPriorityString()
        {
            string txtOut = Enum.GetName(typeof(PriorityType), priority);
            
            return txtOut.Replace("_", " ");
        }

        /// <summary>
        /// Method <c>ToString</c> formats a string containting the user-provided data 
        /// to represent the task in the GUI task list.
        /// </summary>
        /// <remarks>
        /// The multiline task description is omitted.
        /// </remarks>
        /// <returns>A formatted string with user-provided task data.</returns>
        public override string ToString()
        {
            string textOut = 
                $"{dateTime.ToLongDateString(),-23} " + 
                $"{GetTimeString(),-8} " +
                $"{GetPriorityString(),-16} " + 
                $"{description,0}";

            return textOut;
        }

    }
}
