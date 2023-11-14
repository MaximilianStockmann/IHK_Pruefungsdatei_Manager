using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: Refactor Label Text into actual DataModel

namespace IHK_Pruefungsdatei_Manager
{
    enum DocumentType
    {
        Aufgabe,
        Lösung,
        Lösungshinweis,
        Belegsatz
    }

    public partial class DefaultManager : Form
    {
        private Label label1 = new Label();
        GroupBox choiceGrouping = new GroupBox();
        ComboBox yearPicker = new ComboBox();
        ComboBox seasonPicker = new ComboBox();
        ComboBox tradeDisciplinePicker = new ComboBox();
        GroupBox filePickerGrouping = new GroupBox();

        private object[] seasons = new object[] {
            "Frühling",
            "Winter"
        };

        private object[] tradeDisciplines = new object[] {
            "FIAN",
            "FISI",
            "FIDP",
            "FIDN",
            "MATSE"
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
            tradeDisciplinePicker.Items.AddRange(tradeDisciplines);

            choiceGrouping.Controls.Add(seasonPicker);
            choiceGrouping.Controls.Add(yearPicker);

            choiceGrouping.Controls.Add(tradeDisciplinePicker);

            // TODO: Add margin to seasonPicker for nicer visual

            this.Controls.Add(choiceGrouping);
        }

        private void initFilePickerGrouping()
        {
            int DocumentTypeEnumSize = Enum.GetNames(typeof(DocumentType)).Length;

            for (int i = DocumentTypeEnumSize; i >= 0; i--)
            {
                Console.WriteLine(i);
                Label label = new Label();
                label.Dock = DockStyle.Top;
                label.Text = ((DocumentType)i).ToString();

                label.AllowDrop = true;
                label.DragOver += label_DragOver;
                label.DragDrop += label_DragDrop;
                filePickerGrouping.Controls.Add(label);
            }

            this.Controls.Add(filePickerGrouping);
        }
        public Label CreateMyLabel()
        {
            Label label = new Label();
            // Set the border to a three-dimensional border.
            label.BorderStyle = BorderStyle.None;
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

            return label;
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
            filePickerGrouping.Dock = DockStyle.Top;
            choiceGrouping.Dock = DockStyle.Top;
            yearPicker.Dock = DockStyle.Left;
            seasonPicker.Dock = DockStyle.Left;
            tradeDisciplinePicker.Dock = DockStyle.Left;

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
                    Console.WriteLine(GenerateFilePath("C:\\Users\\Max\\IHK_Pruefungsdatei_Manager_Testordner\\"));
                    writeFile(filePath, GenerateFilePath("C:\\Users\\Max\\IHK_Pruefungsdatei_Manager_Testordner\\"));
                }
            }
        }

        // TODO: Error handling an automatic generation of missing folders
        private string GenerateFilePath(string basePath)
        {
            string filePath;

            string year = this.yearPicker.SelectedItem.ToString();
            string season = this.seasonPicker.SelectedItem.ToString();
            string tradeDiscipline = this.tradeDisciplinePicker.SelectedItem.ToString();

            filePath = basePath + year + "_" + season + "_" + tradeDiscipline + ".docx"; // how to get doctype here?

            return filePath;
        }

        private void writeFile(string filePath, string newFilePath)
        {
            File.Copy(filePath, newFilePath);
        }
    }
}
