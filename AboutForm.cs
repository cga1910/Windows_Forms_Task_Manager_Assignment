using System.Reflection;
using System.Windows.Forms;

namespace Assignment_06
{
    /// <summary>
    /// Partial class <c>AboutForm</c> provides the About GUI window,
    /// which can be accessed from the Help menu.
    /// </summary>
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Method <c>InitializeGUI</c> updates the About window with attribute information 
        /// from the currently executing Assembly.
        /// </summary>
        private void InitializeGUI()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyDescriptionAttribute descriptionAttribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            string description = descriptionAttribute.Description;

            lblAbout.Text = description;
            lblHeading.Text = "About ToDo";
        }
    }
}
