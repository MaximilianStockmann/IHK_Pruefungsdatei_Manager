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
        private Label label1 = new Label();
        GroupBox choiceGrouping = new GroupBox();
        ComboBox yearPicker = new ComboBox();
        ComboBox seasonPicker = new ComboBox();
        GroupBox filePickerGrouping = new GroupBox();
        Label label = new Label();

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
            this.initComponents();
            this.initLayout();
        }

        private void initChoiceGrouping()
        {
            yearPicker.Items.AddRange(years);
            seasonPicker.Items.AddRange(seasons);

            choiceGrouping.Controls.Add(seasonPicker);
            choiceGrouping.Controls.Add(yearPicker);

            // TODO: Add margin to seasonPicker for nicer visual

            this.Controls.Add(choiceGrouping);
        }

        private void initFilePickerGrouping()
        {
            //filePickerGrouping.Dock = DockStyle.Fill;

            filePickerGrouping.Controls.Add(label);


            this.Controls.Add(filePickerGrouping);
        }
        public void CreateMyLabel()
        {
            // Set the border to a three-dimensional border.
            label.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // Set the ImageList to use for displaying an image.
            label.ImageList = new ImageList();

            // Use the second image in imageList1.
            label.ImageIndex = 1;

            // Align the image to the top left corner.
            label.ImageAlign = ContentAlignment.TopLeft;

            // Specify that the text can display mnemonic characters.
            label.UseMnemonic = true;
            // Set the text of the control and specify a mnemonic character.
            label.Text = "Test Text:";

            /* Set the size of the control based on the PreferredHeight and PreferredWidth values. */
            label.Size = new Size(label1.PreferredWidth, label1.PreferredHeight);

            label.AllowDrop = true;
            label.DragOver += label_DragOver;
            label.DragDrop += label_DragDrop;

            this.Controls.Add(label);
        }

        public void Fullscreen()
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void initComponents()
        {
            this.CreateMyLabel();
            this.initFilePickerGrouping();
            this.initChoiceGrouping();
        }

        private void initLayout()
        {
            label.Dock = DockStyle.Top;
            filePickerGrouping.Dock = DockStyle.Top;
            choiceGrouping.Dock = DockStyle.Top;
            yearPicker.Dock = DockStyle.Left;
            seasonPicker.Dock = DockStyle.Left;
        }

        // Visual effects on drag hover
        private void label_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        // React to the drop on this control
        private void label_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    // TODO: typechecking so that this doesn't throw an error
                    (sender as Label).Text = filePath;
                    Console.WriteLine(filePath);
                }
            }
        }
    }
}
