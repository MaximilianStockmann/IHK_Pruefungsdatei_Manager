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
        Loesung,
        Loesungshinweis,
        Belegsatz
    }

    public partial class DefaultManager : Form
    {
        private string basePath = @"C:\Users\Max\IHK_Pruefungsdatei_Manager_Testordner\";

        private Label label1 = new Label();
        GroupBox choiceGrouping = new GroupBox();
        ComboBox yearPicker = new ComboBox();
        ComboBox seasonPicker = new ComboBox();
        ComboBox tradeDisciplinePicker = new ComboBox();
        GroupBox filePickerGrouping = new GroupBox();
        Button confirmButton = new Button();
        TableLayoutPanel filePickerTable = new TableLayoutPanel();
        RichTextBox pathTextBox = new RichTextBox();
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

        Dictionary<DocumentType, string> DocTypeUIMapping =
        new Dictionary<DocumentType, string>();

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

        private void initUIMapping()
        {
            DocTypeUIMapping.Add(DocumentType.Aufgabe, "Aufgabe");
            DocTypeUIMapping.Add(DocumentType.Loesung, "Lösung");
            DocTypeUIMapping.Add(DocumentType.Loesungshinweis, "Lösungshinweis");
            DocTypeUIMapping.Add(DocumentType.Belegsatz, "Belegsatz");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void init()
        {
            initUIMapping();
            this.Fullscreen();
            this.initComponents();
            this.initLayout();
        }

        private void initChoiceGrouping()
        {
            yearPicker.Items.AddRange(years);
            seasonPicker.Items.AddRange(seasons);
            tradeDisciplinePicker.Items.AddRange(tradeDisciplines);

            choiceGrouping.Controls.Add(pathTextBox);
            pathTextBox.Click += pathTextBox_ClickHandler;

            choiceGrouping.Controls.Add(seasonPicker);
            choiceGrouping.Controls.Add(yearPicker);

            choiceGrouping.Controls.Add(tradeDisciplinePicker);

            // TODO: Add margin to seasonPicker for nicer visual

            this.Controls.Add(choiceGrouping);
        }

        private void initFilePickerGrouping()
        {
            int DocumentTypeEnumSize = Enum.GetNames(typeof(DocumentType)).Length;

            this.filePickerTable.RowCount = 4;
            this.filePickerTable.ColumnCount = 2;

            filePickerGrouping.Controls.Add(this.filePickerTable);

            this.confirmButton.Click += confirmButton_clicked;
            this.confirmButton.Text = "Anwenden";
            this.filePickerGrouping.Controls.Add(this.confirmButton);
            this.confirmButton.Dock = DockStyle.Bottom;

            this.filePickerGrouping.Controls.Add(this.confirmButton);

            for (int i = DocumentTypeEnumSize-1; i >= 0; i--)
            {
                Label fileLabel = new Label();
                Label descriptorLabel = new Label();

                descriptorLabel.Dock = DockStyle.Left;
                descriptorLabel.Text = DocTypeUIMapping[(DocumentType)i];

                fileLabel.Dock = DockStyle.Top;
                fileLabel.Text = "Datei einfügen";
                fileLabel.Name = ((DocumentType)i).ToString();

                fileLabel.AllowDrop = true;
                fileLabel.DragOver += label_DragOver;
                fileLabel.DragDrop += label_DragDrop;

                this.filePickerTable.Controls.Add(descriptorLabel);
                this.filePickerTable.Controls.Add(fileLabel);
            }

            //this.confirmButton.Dock = DockStyle.Fill;

            this.Controls.Add(filePickerGrouping);
        }

        public void Fullscreen()
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void initComponents()
        {
            this.initFilePickerGrouping();
            this.initChoiceGrouping();
        }

        // TODO: Make filePath Column larger, so that whole Filepath is Shown
        private void initLayout()
        {
            filePickerTable.Dock = DockStyle.Left;
            filePickerGrouping.Dock = DockStyle.Fill;
            choiceGrouping.Dock = DockStyle.Top;
            yearPicker.Dock = DockStyle.Left;
            seasonPicker.Dock = DockStyle.Left;
            tradeDisciplinePicker.Dock = DockStyle.Left;
            pathTextBox.Dock = DockStyle.Left;
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
                }
            }
        }

        private void confirmButton_clicked(object sender, EventArgs e)
        {
            foreach (Control c in this.filePickerTable.Controls)
            {
                if (c.GetType() == typeof(Label) && c.Text != "Datei einfügen")
                {
                    if (c.Name == "Aufgabe")
                    {
                        Console.WriteLine("Absolutely in the switch case");
                        writeFile(c.Text, GenerateFilePath(c as Label));
                    }
                    if (c.Name == "Loesung")
                    {
                        Console.WriteLine("Loesung");
                        Console.WriteLine("Absolutely in the switch case");
                        writeFile(c.Text, GenerateFilePath(c as Label));
                    }
                    if (c.Name == "Loesungshinweis")
                    {
                        Console.WriteLine("Loesungshinweis");
                        Console.WriteLine("Absolutely in the switch case");
                        writeFile(c.Text, GenerateFilePath(c as Label));
                    }
                    if (c.Name == "Belegsatz")
                    {
                        Console.WriteLine("Belegsatz");
                        Console.WriteLine("Absolutely in the switch case");
                        writeFile(c.Text, GenerateFilePath(c as Label));
                    }
                }
            }
        }

        private void pathTextBox_ClickHandler(object sender , EventArgs e)
        {
            // Display the openFile dialog.
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    // Output the requested file in richTextBox1.
                    this.pathTextBox.Text = folderBrowserDialog.SelectedPath;
                    this.basePath = this.pathTextBox.Text;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                    + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                }
                Invalidate();
            } else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        // TODO: Error handling an automatic generation of missing folders
        private string GenerateFilePath(Label label)
        {
            Console.WriteLine("In generateFilePath()");
            string filePath;

            string year = this.yearPicker.SelectedItem.ToString();
            string season = this.seasonPicker.SelectedItem.ToString();
            string tradeDiscipline = this.tradeDisciplinePicker.SelectedItem.ToString();

            Console.WriteLine("base Path" + this.basePath);

            filePath = this.basePath + "\\" + year + "_" + season + "_" + tradeDiscipline + "_" + label.Name;
            
            return filePath;
        }

        private void writeFile(string filePath, string newFilePath)
        {
            Console.WriteLine(Path.GetExtension(filePath));
            newFilePath = Path.ChangeExtension(newFilePath, Path.GetExtension(filePath));

            string directoryPath = Path.GetDirectoryName(newFilePath);
            string fileName = Path.GetFileName(newFilePath);

            string[] fileNameParts = fileName.Split('_');

            string subdirectoryYear = fileNameParts[0];

            string subdirectoryYearFull = Path.Combine(directoryPath, subdirectoryYear);

            if (!Directory.Exists(subdirectoryYearFull))
            {
                Directory.CreateDirectory(subdirectoryYearFull);
            }

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine(directoryPath + " does not exist!");
            }

            newFilePath = Path.Combine(subdirectoryYearFull, fileName);

            if (File.Exists(newFilePath))
            {
                // TODO: this should open a dialog to ask the user
                File.Delete(newFilePath);
            }

            File.Copy(filePath, newFilePath);
        }
    }
}
