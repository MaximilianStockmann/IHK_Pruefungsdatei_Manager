using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHK_Pruefungsdatei_Manager
{
    public partial class DefaultManager : Form
    {
        private object[] seasons = new object[] {
        "Frühling",
        "Winter"
        };
        private object[] years = new object[]
        {
            2024,
            2025,
            2026
        };

        private Label testLabel = new Label();

        public DefaultManager()
        {
            InitializeComponent();
            init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void init()
        {
            this.Fullscreen();
            this.initFilePickerGrouping();
            this.initChoiceGrouping();
        }

        private void initChoiceGrouping()
        {
            GroupBox choiceGrouping = new GroupBox();
            choiceGrouping.Dock = DockStyle.Top;

            ComboBox yearPicker = new ComboBox();
            yearPicker.Items.AddRange(years);

            ComboBox seasonPicker = new ComboBox();
            seasonPicker.Items.AddRange(seasons);

            choiceGrouping.Controls.Add(seasonPicker);
            choiceGrouping.Controls.Add(yearPicker);

            yearPicker.Dock = DockStyle.Left;
            seasonPicker.Dock = DockStyle.Left;

            // TODO: Add margin to seasonPicker for nicer visual

            this.Controls.Add(choiceGrouping);
        }

        private void initFilePickerGrouping()
        {
            GroupBox filePickerGrouping = new GroupBox();
            //filePickerGrouping.Dock = DockStyle.Fill;

            Label label = this.CreateMyLabel();
            filePickerGrouping.Controls.Add(label);

            filePickerGrouping.Dock = DockStyle.Top;

            this.Controls.Add(filePickerGrouping);
        }
        public Label CreateMyLabel()
        {
            // Create an instance of a Label.
            Label label1 = new Label();

            // Set the border to a three-dimensional border.
            label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // Set the ImageList to use for displaying an image.
            label1.ImageList = new ImageList();

            // Use the second image in imageList1.
            label1.ImageIndex = 1;

            // Align the image to the top left corner.
            label1.ImageAlign = ContentAlignment.TopLeft;

            // Specify that the text can display mnemonic characters.
            label1.UseMnemonic = true;
            // Set the text of the control and specify a mnemonic character.
            label1.Text = "Test Text:";

            /* Set the size of the control based on the PreferredHeight and PreferredWidth values. */
            label1.Size = new Size(label1.PreferredWidth, label1.PreferredHeight);

            label1.Dock = DockStyle.Top;

            label1.AllowDrop = true;
            label1.DragOver += label1_DragOver;

            //...Code to add the control to the form...
            return label1;
        }

        public void Fullscreen()
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void label1_DragOver(object sender, DragEventArgs e) =>
            e.Effect = DragDropEffects.All;

        // React to the drop on this control
        private void textBox1_DragDrop(object sender, DragEventArgs e) =>
            testLabel.Text = (string)e.Data.GetData(typeof(string));
    }
}
