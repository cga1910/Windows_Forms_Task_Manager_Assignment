using System;
using System.Windows.Forms;

namespace Assignment_06
{
    /// <summary>
    /// Partial class <c>MainForm</c> provides the main GUI window.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is responsible for user interaction, validating user input, 
    /// formatting and displaying data and error messages.
    /// </para>
    /// <para>
    /// <c>MainForm</c> uses the class <c>TaskManager</c> which is used for 
    /// adding, deleting, changing and getting todo/task data.
    /// </para>
    /// <para>
    /// <c>MainForm</c> uses the class <c>Messages</c> which contains
    /// a library of GUI messages for communication with the user.
    /// </para>
    /// <para>
    /// <c>MainForm</c> also instantiates the class <c>Clock</c> which provides
    /// the GUI clock.
    /// </para>
    /// <para>
    /// This form uses the font Cascadia Code: https://github.com/microsoft/cascadia-code
    /// for input and the task listbox.
    /// </para>
    /// </remarks>
    public partial class MainForm : Form
    {
        private TaskManager taskManager;
        private Clock clock;
        private readonly Messages msg;

        // the @ symbol tells the compiler to interpret the string as a verbatim string, i.e.
        // the backslash character will be interpreted as a backslash, not an escape character
        private string fileName = Application.StartupPath + @"\todo.txt";
        private const int timerInterval = 1000; // milliseconds
        private bool editMode;

        public MainForm()
        {
            msg = new Messages();

            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Method <c>InitializeGUI</c> initializes the GUI to default values, 
        /// clears the task list, and instantiates a new TaskManager object.
        /// </summary>
        private void InitializeGUI()
        {
            DisableEditMode();

            taskManager = new TaskManager(); // needed here because of menu option "New"
            lstTasks.Items.Clear();

            menuFileOpen.Enabled = true;
            menuFileSave.Enabled = false;
            
            InitializeTextBoxes();
            InitializeDateTimePicker();
            InitializePriorityComboBox();

            InitializeToolTips();

            InitializeClock();
        }

        /// <summary>
        /// Method <c>InitializeTextBoxes</c> clears the task description textboxes 
        /// by updating each with an empty string.
        /// </summary>
        private void InitializeTextBoxes()
        {
            txtDescription.Text = string.Empty;
            txtDescriptionMultiline.Text = string.Empty;
        }

        /// <summary>
        /// Method <c>InitializeDateTimePicker</c> configures the date / time picker
        /// control with a custom format, and resets the control to its default value: 
        /// today's date and the current time.
        /// </summary>
        private void InitializeDateTimePicker()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " yyyy-MM-dd    HH:mm";
            dateTimePicker1.ResetText();
        }

        /// <summary>
        /// Method <c>InitializePriorityComboBox</c> fills the task priority combobox
        /// with the pre-defined priority levels found in the <c>PriorityType</c> enum.
        /// </summary>
        /// <remarks>
        /// Replaces any underscore with a space before adding a name to the ComboBox.
        /// </remarks>
        private void InitializePriorityComboBox()
        {
            cmbPriority.Items.Clear();

            foreach (string priority in Enum.GetNames(typeof(PriorityType)))
                cmbPriority.Items.Add(priority.Replace("_", " "));

            cmbPriority.SelectedIndex = (int)PriorityType.Normal;
        }

        /// <summary>
        /// Method <c>InitializeClock</c> starts the clock by setting the timer's 
        /// tick interval and starting the timer.
        /// </summary>
        private void InitializeClock()
        {
            timer.Interval = timerInterval;
            clock = new Clock();
            timer.Start();
        }

        /// <summary>
        /// Method <c>InitializeToolTips</c> configures various tooltips using strings 
        /// from the Message class.
        /// </summary>
        private void InitializeToolTips()
        {
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;

            toolTip1.SetToolTip(dateTimePicker1, msg.Tips.DateTime);
            toolTip1.SetToolTip(cmbPriority, msg.Tips.Priority);
            toolTip1.SetToolTip(btnChange, msg.Tips.Change);
            toolTip1.SetToolTip(lstTasks, msg.Tips.Tasks);
            toolTip1.SetToolTip(btnDelete, msg.Tips.Delete);
            toolTip1.SetToolTip(txtDescription, msg.Tips.Description);
            toolTip1.SetToolTip(txtDescriptionMultiline, msg.Tips.DescriptionLonger);
        }

        /// <summary>
        /// Executes once every timer tick (the interval is specified in the field <c>timerInterval</c>)
        /// to update the clock label's text.
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblClock.Text = clock.UpdateClock(); // Uses the current time from DateTime.Now
            //lblClock.Text = clock.UpdateHomeMadeClock(); // Uses the home-made clock instead
        }

        /// <summary>
        /// Executes when the Add button is clicked
        /// </summary>
        /// <remarks>
        /// Reads user input and calls the <c>TaskManager</c> to add a new task to the list
        /// </remarks>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Task task = ReadInput();

            if (taskManager.AddNewTask(task))
                UpdateGUI();
        }
        /// <summary>
        /// Executes when the Save button is clicked
        /// </summary>
        /// <remarks>
        /// Reads user input and calls the <c>TaskManager</c> to change the selected task in the list
        /// </remarks>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Task task = ReadInput();

            if (taskManager.ChangeTaskAt(lstTasks.SelectedIndex, task))
            {
                //MessageBox.Show("Your task was updated.");
                UpdateGUI();
            }
        }

        /// <summary>
        /// Reads user input and checks for obligatory input. If the obligatory input was
        /// found, data is saved to a new <c>Task</c> object.
        /// </summary>
        /// <returns>A <c>Task</c> object containing the user input.</returns>
        private Task ReadInput()
        {
            Task task = new Task();

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show(msg.Error.DescriptionMissing, msg.Title.InputMissing);
                return null;
            }

            task.Description = txtDescription.Text;
            task.DescriptionMultiline = txtDescriptionMultiline.Text;
            task.DescriptionMultilineCount = txtDescriptionMultiline.Lines.Length;
            task.DateAndTime = dateTimePicker1.Value;
            task.Priority = (PriorityType)cmbPriority.SelectedIndex;

            return task;
        }

        /// <summary>
        /// Clears input controls and the task list, calls <c>TaskManager</c> to get an
        /// array of currently saved tasks, and adds the them to the GUI task list.
        /// </summary>
        private void UpdateGUI()
        {
            DisableEditMode();

            // Clear the inputs
            txtDescription.Clear();
            txtDescriptionMultiline.Clear();
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;

            // Update the task list
            lstTasks.Items.Clear();
            string[] infoStrings = taskManager.GetInfoStringsArray();
            if (infoStrings != null)
                lstTasks.Items.AddRange(infoStrings);

            if (lstTasks.Items.Count > 0) menuFileSave.Enabled = true;
        }

        /// <summary>
        /// Enables edit mode by configuring the GUI.
        /// </summary>
        private void EnableEditMode()
        {
            editMode = true;
            groupBoxAddEdit.Text = "Edit task";

            lstTasks.Enabled = false;

            btnChange.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Visible = false;
            btnSave.Visible = true;

            lblHelp.Text += "\n\n" + msg.Tips.HelpEditMode;
        }
        
        /// <summary>
        /// Disables edit mode by configuring the GUI.
        /// </summary>
        private void DisableEditMode()
        {
            editMode = false;
            groupBoxAddEdit.Text = "Add a task";
            
            lstTasks.Enabled = true;

            btnChange.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Visible = true;
            btnSave.Visible = false;

            lblHelp.Text = msg.Tips.HelpDefault;
        }

        /// <summary>
        /// Executes when the File menu option New is clicked.
        /// </summary>
        /// <remarks>
        /// Reinitializes the GUI. Presents the user with a confirmation dialog if the GUI task list is not empty.
        /// </remarks>
        private void menuFileNew2_Click(object sender, EventArgs e)
        {
            if (lstTasks.Items.Count > 0)
            {
                DialogResult confirmDelete = MessageBox.Show(msg.Question.New, msg.Title.Confirm,
                    MessageBoxButtons.YesNo);

                if (confirmDelete == DialogResult.No) return;
            }
            InitializeGUI();
        }

        /// <summary>
        /// Executes when the File menu option Open is clicked.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Calls the <c>TaskManager</c> to initiate reading task data from file.
        /// </para>
        /// <para>
        /// Displays an error message if there was a problem reading the file.
        /// </para>
        /// </remarks>
        private void menuFileOpen2_Click(object sender, EventArgs e)
        {
            if (lstTasks.Items.Count > 0)
            {
                DialogResult confirmLoadFile = MessageBox.Show(msg.Question.LoadFile, msg.Title.Confirm,
                    MessageBoxButtons.YesNo);

                if (confirmLoadFile == DialogResult.No) return;
            }

            try
            {
                taskManager.ReadDataFromFile(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(msg.Error.OpeningFile + "\n\n" + ex.Message, msg.Title.Error);
            }

            UpdateGUI();
        }

        /// <summary>
        /// Executes when the File menu option Save is clicked.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Calls the <c>TaskManager</c> to initiate writing the current tasks to file.
        /// </para>
        /// <para>
        /// Displays an error message if there was a problem writing the file, or an
        /// info message if successful.
        /// </para>
        /// </remarks>
        private void menuFileSave2_Click(object sender, EventArgs e)
        {
            try
            {
                taskManager.WriteDataToFile(fileName);
                MessageBox.Show(msg.Info.FileSaved + fileName, msg.Title.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(msg.Error.SavingFile + "\n\n" + ex.Message, msg.Title.Error);
            }
        }

        /// <summary>
        /// Executes when the File menu option Exit is clicked.
        /// </summary>
        /// <remarks>
        /// Shows a confirmation dialog before exiting the application.
        /// </remarks>
        private void menuFileExit2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(msg.Question.Quit, msg.Title.Confirm,
                MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.OK)
                Application.Exit();
        }

        /// <summary>
        /// Executes when the Help menu option About is clicked.
        /// </summary>
        /// <remarks>
        /// Shows the About form containing app information.
        /// </remarks>
        private void menuHelpAbout2_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        /// <summary>
        /// Executes when the Change button is clicked.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Enables edit mode and displays the currently selected task in the input controls, 
        /// for the user to edit.
        /// </para>
        /// <para>
        /// Shows an error dialog if no task was selected.
        /// </para>
        /// </remarks>
        private void btnChange_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index >= 0)
            {
                EnableEditMode();

                Task task = taskManager.GetTask(index);
                txtDescription.Text = task.Description;
                txtDescriptionMultiline.Text = task.DescriptionMultiline;
                dateTimePicker1.Value = task.DateAndTime;
                cmbPriority.SelectedIndex = (int)task.Priority;
            }
            else
                MessageBox.Show(msg.Error.TaskSelectionMissing);
        }

        /// <summary>
        /// Executes when the Delete button is clicked.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Calls the <c>TaskManager</c> to delete the selected task.
        /// </para>
        /// <para>
        /// Shows a confirmation dialog before deletion.
        /// </para>
        /// <para>
        /// Shows an error dialog if no task was selected.
        /// </para>
        /// <para>
        /// After deletion, if the task list is empty, disables the Change and Delete buttons
        /// and the File menu option Save.
        /// </para>
        /// </remarks>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index >= 0)
            {
                DialogResult confirmDelete = MessageBox.Show(msg.Question.Delete, msg.Title.Confirm,
                    MessageBoxButtons.YesNo);

                if (confirmDelete == DialogResult.Yes)
                {
                    if (taskManager.DeleteTaskAt(index)) UpdateGUI();
                }
                else return;
            }
            else MessageBox.Show(msg.Error.TaskSelectionMissing);

            if (lstTasks.Items.Count == 0)
            {
                btnChange.Enabled = false;
                btnDelete.Enabled = false;
                menuFileSave.Enabled = false;
            }
        }

        /// <summary>
        /// Executes when the selected index of the task listbox changes.
        /// </summary>
        /// <remarks>
        /// Enables the Change and Delete buttons.
        /// </remarks>
        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex >= 0)
            {
                btnChange.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        /// <summary>
        /// Executes when the user double-clicks the task listbox.
        /// </summary>
        /// <remarks>
        /// Calls the <c>btnChange_Click</c> method to initiate edit mode.
        /// </remarks>
        private void lstTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnChange_Click(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Executes when the Esc keyboard key is pressed.
        /// </summary>
        /// <remarks>
        /// Shows a confirmation dialog if the app is currently in task editing mode, 
        /// before canceling the editing.
        /// </remarks>
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (editMode == true && e.KeyChar == (char)Keys.Escape)
            {
                DialogResult confirmCancelEdit = MessageBox.Show(
                    msg.Info.CancelEdit, msg.Title.Warning, 
                    MessageBoxButtons.OKCancel);

                if (confirmCancelEdit == DialogResult.OK) UpdateGUI();
                else return;
            }
        }

    }
}
