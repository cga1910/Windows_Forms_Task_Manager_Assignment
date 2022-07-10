using System;

namespace Assignment_06
{
    /// <summary>
    /// Class <c>Messages</c> is the base class for various categories of GUI messages.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The GUI message categories all have their own classes. Those subclasses are declared
    /// as fields here, with read access through properties. This keeps them ordered, and 
    /// easy to access by code.
    /// </para>
    /// <para>
    /// The point is to keep message strings safely separated from other code, and to avoid having
    /// identical messages occuring multiple times (especially if the application will be expanded
    /// or developed further, in the future).
    /// </para>
    /// </remarks>
    internal class Messages
    {
        private readonly Titles titles;
        private readonly Info info;
        private readonly Errors errors;
        private readonly Questions questions;
        private readonly Tips tips;

        public Messages()
        {
            titles = new Titles();
            info = new Info();
            errors = new Errors();
            questions = new Questions();
            tips = new Tips();
        }

        public Titles Title { get { return titles; } }
        public Info Info { get { return info; } }
        public Errors Error { get { return errors; } }
        public Questions Question { get { return questions; } }
        public Tips Tips { get { return tips; } }
    }

    /// <summary>
    /// Class <c>Titles</c> holds GUI messages that are used as titles for dialog windows.
    /// </summary>
    internal class Titles
    {
        private const string success =      "Success";
        private const string inputMissing = "Missing input";
        private const string confirm =      "Please confirm";
        private const string warning =      "Warning";
        private const string error =        "Error";

        public string Success { get { return success; } }
        public string InputMissing { get { return inputMissing; } }
        public string Confirm { get { return confirm; } }
        public string Warning { get { return warning; } }
        public string Error { get { return error; } }
    }

    /// <summary>
    /// Class <c>Info</c> holds non-specific GUI info messages that do not currently 
    /// fit in any other message category.
    /// </summary>
    internal class Info
    {
        private readonly string fileSaved = "Data saved to file: " + Environment.NewLine;
        private const string cancelEdit =   "Any changes made will be lost.";

        public string FileSaved { get { return fileSaved; } }
        public string CancelEdit { get { return cancelEdit; } }
    }

    /// <summary>
    /// Class <c>Errors</c> holds GUI messages for errors or other problems encountered by the user.
    /// </summary>
    internal class Errors
    {
        private const string fileOpen =             "Error reading the file.";
        private const string fileSave =             "Error saving to file.";
        private const string descriptionMissing =   "Please enter a description in the upper text field.";
        private const string taskSelectionMissing = "Please select a task from the list first.";

        public string OpeningFile { get { return fileOpen; } }
        public string SavingFile { get { return fileSave; } }
        public string DescriptionMissing { get { return descriptionMissing; } }
        public string TaskSelectionMissing { get { return taskSelectionMissing; } }
    }

    /// <summary>
    /// Class <c>Questions</c> holds GUI messages that are questions.
    /// </summary>
    internal class Questions
    {
        private const string quit =     "Do you really want to quit?";
        private const string delete =   "Are you sure you want to delete the selected task?";
        private const string newList =  "Are you sure that you want to delete all tasks?";
        private const string loadFile = "Reading tasks from file will overwrite the current task list. Do you want to proceed?";

        public string Quit { get { return quit; } }
        public string Delete { get { return delete; } }
        public string New { get { return newList; } }
        public string LoadFile { get { return loadFile; } }
    }

    /// <summary>
    /// Class <c>Tips</c> holds GUI messages that are used for user guidance,
    /// such as tool tips or other help messages.
    /// </summary>
    internal class Tips
    {
        private readonly string dateTime =       "Enter a date and time for your task." + Environment.NewLine + 
                                                 "Click the icon to select from a calendar.";
        private const string priority =          "Set the priority of the task.";
        private const string description =       "Describe your task here.";
        private const string descriptionLonger = "Room for more description.";
        private const string change =            "To change a task, select it, then press this button.";
        private const string delete =            "To delete a task, select it, then press this button.";
        private readonly string tasks =          "Select a task to change or delete it." + Environment.NewLine + 
                                                 "Double-click a task to begin editing.";
        private const string helpDefault =       "Place the mouse pointer over elements to get tips.";
        private readonly string helpEditMode =   "Click Save to apply changes." + Environment.NewLine + 
                                                 "Press Esc to cancel.";

        public string DateTime { get { return dateTime; } }
        public string Priority { get { return priority; } }
        public string Description { get { return description; } }
        public string DescriptionLonger { get { return descriptionLonger; } }
        public string Change { get { return change; } }
        public string Delete { get { return delete; } }
        public string Tasks { get { return tasks; } }
        public string HelpDefault { get { return helpDefault; } }
        public string HelpEditMode { get { return helpEditMode; } }

    }
}
