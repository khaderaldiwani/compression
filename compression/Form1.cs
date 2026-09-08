using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compression
{
    public partial class Form1 : Form
    {
        List<FileEntry> filesToCompress = new List<FileEntry>();
        CancellationTokenSource ctsStop;
        CancellationTokenSource cts;
        long totalOriginalSize = 0;
        long totalCompressedSize = 0;

        int lastCompressedIndex = 0;
        string archiveFilePath = "";
        string[] startupFiles;

        public Form1(string[] files = null)
        {
            InitializeComponent();
            startupFiles = files;
        }

        public Form1()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

        }
        private bool isCanceled = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            algorithmSelector.Items.AddRange(new string[] { "Huffman", "ShannonFano" });
            algorithmSelector.SelectedIndex = 0;
            checkBox1.Checked = false;
            passwordBox.BackColor = Color.Gray;
            normalRadio.Checked = true;
            if (startupFiles != null)
            {
                foreach (var file in startupFiles)
                {
                    if (File.Exists(file))
                    {
                        filesToCompress.Add(new FileEntry
                        {
                            FullPath = file,
                            RelativePath = Path.GetFileName(file)
                        });
                        listBoxObject.Items.Add(Path.GetFileName(file));
                    }
                    else if (Directory.Exists(file))
                    {
                        var allFiles = Directory.GetFiles(file, "*.*", SearchOption.AllDirectories);
                        foreach (var innerFile in allFiles)
                        {
                            filesToCompress.Add(new FileEntry
                            {
                                FullPath = innerFile,
                                RelativePath = GetRelativePath(file, innerFile)
                            });
                            listBoxObject.Items.Add(Path.GetFileName(innerFile));
                        }
                    }
                }
            }

            
        }

        private void addFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in open.FileNames)
                {
                    filesToCompress.Add(new FileEntry { FullPath = file, RelativePath = Path.GetFileName(file) });
                    listBoxObject.Items.Add(Path.GetFileName(file));
                }
            }
            progressBar1.Maximum = filesToCompress.Count;
        }

        private void addFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
           
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderDialog.SelectedPath;
                var allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                foreach (var file in allFiles)
                {
                    string relativePath = GetRelativePath(folderPath, file);
                    filesToCompress.Add(new FileEntry { FullPath = file, RelativePath = relativePath });
                    listBoxObject.Items.Add(relativePath);
                }
            }
            progressBar1.Maximum = filesToCompress.Count;
        }

        private async void compressWithoutThread()
        {
            isCanceled = false;
            totalOriginalSize = 0;
            totalCompressedSize = 0;


            if (filesToCompress.Count == 0)
            {
                MessageBox.Show("يرجى اختيار ملفات أولاً.");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Compressed Archive|*.cmp";
            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            string algorithm = algorithmSelector.SelectedItem?.ToString() ?? "Huffman";
            archiveFilePath = saveDialog.FileName;
            cts = new CancellationTokenSource();
            ctsStop = new CancellationTokenSource();
            lastCompressedIndex = 0;
            progressBar1.Value = 0;
            status.Text = "جاري الضغط...";

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(archiveFilePath, FileMode.Create)))
                {
                    string password = passwordBox.Text ?? "";
                    writer.Write(algorithm);
                    writer.Write(password);
                    writer.Write(filesToCompress.Count);

                    for (int i = 0; i < filesToCompress.Count; i++)
                    {
                        if (isCanceled)
                        {
                            status.Text = "العملية ملغاة.";
                            break;
                        }

                        if (cts.IsCancellationRequested)
                        {
                            lastCompressedIndex = i;
                            status.Text = "تم الإيقاف المؤقت.";
                            return;
                        }

                        byte[] data = File.ReadAllBytes(filesToCompress[i].FullPath);
                        byte[] compressed;
                        string encodedTree;

                        if (algorithm == "Huffman")
                        {
                            Hunffman h = new Hunffman();
                            h.Compress(data);
                            compressed = h.BitArrayToByteArray(h.Tobitarray(data));
                            encodedTree = h.GetEncodedTree();
                        }
                        else if (algorithm == "ShannonFano")
                        {
                            ShannonFano s = new ShannonFano();
                            s.Compress(data);
                            compressed = s.BitArrayToByteArray(s.Tobitarray(data));
                            encodedTree = s.GetEncodedTree();
                        }
                        else
                        {
                            MessageBox.Show("خوارزمية غير مدعومة!");
                            return;
                        }

                        writer.Write(filesToCompress[i].RelativePath);
                        writer.Write(encodedTree);
                        writer.Write(compressed.Length);
                        writer.Write(compressed);
                        progressBar1.Value = i + 1;
                        status.Text = $"تم ضغط الملف {i + 1} من {filesToCompress.Count}";
                        totalOriginalSize += data.Length;
                        totalCompressedSize += compressed.Length;

                        await Task.Delay(30);
                    }
                    double totalRatio = (1.0 - ((double)totalCompressedSize / totalOriginalSize)) * 100;
                    MessageBox.Show($"تم ضغط الأرشيف بنسبة: {totalRatio:F2}%");


                    filesToCompress.Clear();
                    listBoxObject.Items.Clear();
                    progressBar1.Value = 0;
                }
                if (isCanceled && File.Exists(archiveFilePath))
                {
                    try
                    {
                        File.Delete(archiveFilePath);
                        MessageBox.Show("تم إلغاء الضغط وحذف الملف الناتج.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("لم يتم حذف الملف الناتج: " + ex.Message);
                    }
                }
                isCanceled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الضغط: " + ex.Message);
            }
        }

        private async void compressButton_Click(object sender, EventArgs e)
        {
            if (normalRadio.Checked)
            {
               compressWithoutThread();
            }
            if (fastRadio.Checked) {
                compressWithThread();
                }
        }
       
        private async void compressWithThread()
        {
            isCanceled = false;
            totalOriginalSize = 0;
            totalCompressedSize = 0;

            if (filesToCompress.Count == 0)
            {
                MessageBox.Show("يرجى اختيار ملفات أولاً.");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Compressed Archive|*.cmp";
            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            string algorithm = algorithmSelector.SelectedItem?.ToString() ?? "Huffman";
            archiveFilePath = saveDialog.FileName;
            ctsStop = new CancellationTokenSource();
            lastCompressedIndex = 0;
            progressBar1.Value = 0;
            status.Text = "جاري الضغط...";

            try
            {
                string password = passwordBox.Text ?? "";

                using (BinaryWriter writer = new BinaryWriter(File.Open(archiveFilePath, FileMode.Create)))
                {
                    writer.Write(algorithm);
                    writer.Write(password);
                    writer.Write(filesToCompress.Count);

                    for (int i = 0; i < filesToCompress.Count; i++)
                    {
                        if (isCanceled)
                        {
                            status.Text = "العملية ملغاة.";
                            break;
                        }

                        int index = i;
                        var file = filesToCompress[index];

                        var result = await Task.Run(() =>
                        {
                            byte[] data = File.ReadAllBytes(file.FullPath);
                            byte[] compressed;
                            string encodedTree;

                            if (algorithm == "Huffman")
                            {
                                Hunffman h = new Hunffman();
                                h.Compress(data);
                                compressed = h.BitArrayToByteArray(h.Tobitarray(data));
                                encodedTree = h.GetEncodedTree();
                            }
                            else if (algorithm == "ShannonFano")
                            {
                                ShannonFano s = new ShannonFano();
                                s.Compress(data);
                                compressed = s.BitArrayToByteArray(s.Tobitarray(data));
                                encodedTree = s.GetEncodedTree();
                            }
                            else
                            {
                                throw new InvalidOperationException("خوارزمية غير مدعومة!");
                            }

                            return (RelativePath: file.RelativePath, EncodedTree: encodedTree, Compressed: compressed, OriginalSize: data.Length);
                        });

                        writer.Write(result.RelativePath);
                        writer.Write(result.EncodedTree);
                        writer.Write(result.Compressed.Length);
                        writer.Write(result.Compressed);

                        totalOriginalSize += result.OriginalSize;
                        totalCompressedSize += result.Compressed.Length;

                        progressBar1.Value = i + 1;
                        status.Text = $"تم ضغط الملف {i + 1} من {filesToCompress.Count}";
                    }
                }

                if (isCanceled && File.Exists(archiveFilePath))
                {
                    try
                    {
                        File.Delete(archiveFilePath);
                        MessageBox.Show("تم إلغاء الضغط وحذف الملف الناتج.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("لم يتم حذف الملف الناتج: " + ex.Message);
                    }

                    filesToCompress.Clear();
                    listBoxObject.Items.Clear();
                    progressBar1.Value = 0;
                    return;
                }

                double totalRatio = (1.0 - ((double)totalCompressedSize / totalOriginalSize)) * 100;
                MessageBox.Show($"تم ضغط الأرشيف بنسبة: {totalRatio:F2}%");

                filesToCompress.Clear();
                listBoxObject.Items.Clear();
                progressBar1.Value = 0;
                isCanceled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الضغط: " + ex.Message);
            }
        }

        private string GetRelativePath(string basePath, string fullPath)
        {
            Uri baseUri = new Uri(AppendDirectorySeparatorChar(basePath));
            Uri fullUri = new Uri(fullPath);
            return Uri.UnescapeDataString(baseUri.MakeRelativeUri(fullUri).ToString()).Replace('/', Path.DirectorySeparatorChar);
        }

        private string AppendDirectorySeparatorChar(string path)
        {
            return path.EndsWith(Path.DirectorySeparatorChar.ToString()) ? path : path + Path.DirectorySeparatorChar;
        }

        private void deCompressButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Compressed Archive|*.cmp";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            using (BinaryReader reader = new BinaryReader(File.Open(ofd.FileName, FileMode.Open)))
            {
                string algorithm = reader.ReadString();           
                string archivePassword = reader.ReadString();    
                if (!string.IsNullOrEmpty(archivePassword))
                {
                    string input = PromptPassword();

                    if (input != archivePassword)
                    {
                        MessageBox.Show("كلمة السر غير صحيحة!");
                        return;
                    }
                }

                int fileCount = reader.ReadInt32();

                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;

                for (int i = 0; i < fileCount; i++)
                {

                    string name = reader.ReadString();
                    string treeData = reader.ReadString();
                    int len = reader.ReadInt32();
                    byte[] encoded = reader.ReadBytes(len);

                    byte[] decoded;

                    if (algorithm == "Huffman")
                    {
                        Hunffman h = new Hunffman();
                        h.LoadEncodedTree(treeData);
                        decoded = h.Decode(new BitArray(encoded));
                    }
                    else if (algorithm == "ShannonFano")
                    {
                        ShannonFano s = new ShannonFano();
                        s.LoadEncodedTree(treeData);
                        decoded = s.Decode(new BitArray(encoded));
                    }
                    else
                    {
                        MessageBox.Show("خوارزمية غير مدعومة!");
                        return;
                    }

                    string outPath = Path.Combine(fbd.SelectedPath, name);
                    Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                    File.WriteAllBytes(outPath, decoded);
                }

                status.Text = "تم فك الضغط بنجاح.";
            }
        }
        private string PromptPassword()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.Text = "أدخل كلمة السر";

                TextBox textBox = new TextBox() { Left = 20, Top = 20, Width = 240, UseSystemPasswordChar = true };
                Button confirmation = new Button() { Text = "موافق", Left = 180, Width = 80, Top = 60, DialogResult = DialogResult.OK };

                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }
        }
        private void deCompressSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Compressed Archive|*.cmp";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            using (BinaryReader reader = new BinaryReader(File.Open(ofd.FileName, FileMode.Open)))
            {
                string algorithm = reader.ReadString();           
                string archivePassword = reader.ReadString();     

                if (!string.IsNullOrEmpty(archivePassword))
                {
                    string input = PromptPassword();

                    if (input != archivePassword)
                    {
                        MessageBox.Show("كلمة السر غير صحيحة!");
                        return;
                    }
                }

                int fileCount = reader.ReadInt32();
                List<string> fileNames = new List<string>();
                List<byte[]> fileDataList = new List<byte[]>();
                List<string> treeList = new List<string>();

                for (int i = 0; i < fileCount; i++)
                {
                    string name = reader.ReadString();
                    string tree = reader.ReadString();
                    int len = reader.ReadInt32();
                    byte[] data = reader.ReadBytes(len);

                    fileNames.Add(name);
                    treeList.Add(tree);
                    fileDataList.Add(data);
                }

                using (Form selectForm = new Form())
                {
                    selectForm.Text = "اختر ملفًا لاستخراجه";
                    ListBox listBox = new ListBox() { Dock = DockStyle.Fill };
                    Button extractBtn = new Button() { Text = "استخراج", Dock = DockStyle.Bottom };

                    foreach (string name in fileNames)
                        listBox.Items.Add(name);

                    selectForm.Controls.Add(listBox);
                    selectForm.Controls.Add(extractBtn);

                    string selectedFile = null;
                    extractBtn.Click += (s, ea) =>
                    {
                        if (listBox.SelectedIndex >= 0)
                        {
                            selectedFile = fileNames[listBox.SelectedIndex];
                            selectForm.DialogResult = DialogResult.OK;
                            selectForm.Close();
                        }
                    };

                    if (selectForm.ShowDialog() == DialogResult.OK && selectedFile != null)
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() != DialogResult.OK)
                            return;

                        int index = fileNames.IndexOf(selectedFile);
                        byte[] decoded;

                        if (algorithm == "Huffman")
                        {
                            Hunffman h = new Hunffman();
                            h.LoadEncodedTree(treeList[index]);
                            decoded = h.Decode(new BitArray(fileDataList[index]));
                        }
                        else if (algorithm == "ShannonFano")
                        {
                            ShannonFano s = new ShannonFano();
                            s.LoadEncodedTree(treeList[index]);
                            decoded = s.Decode(new BitArray(fileDataList[index]));
                        }
                        else
                        {
                            MessageBox.Show("خوارزمية غير مدعومة!");
                            return;
                        }

                        string path = Path.Combine(fbd.SelectedPath, selectedFile);
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        File.WriteAllBytes(path, decoded);
                        status.Text = "تم استخراج الملف.";
                    }
                }
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            status.Text = "تم الإيقاف المؤقت.";
        }


        private async void resumeButton_Click(object sender, EventArgs e)
        {
            isCanceled = false;
            if (string.IsNullOrEmpty(archiveFilePath) || lastCompressedIndex >= filesToCompress.Count)
            {
                MessageBox.Show("لا يمكن الاستئناف. تأكد من وجود ضغط سابق.");
                return;
            }

            cts = new CancellationTokenSource();
            progressBar1.Value = lastCompressedIndex;
            status.Text = "استئناف الضغط...";

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(archiveFilePath, FileMode.Append)))
                {
                    string algorithm = algorithmSelector.SelectedItem?.ToString() ?? "Huffman";

                    for (int i = lastCompressedIndex; i < filesToCompress.Count; i++)
                    {

                        if (isCanceled)
                        {
                            status.Text = "العملية ملغاة.";
                            break;
                        }

                        if (cts.IsCancellationRequested)
                        {
                            lastCompressedIndex = i;
                            status.Text = "تم الإيقاف المؤقت.";
                            return;
                        }

                        byte[] data = File.ReadAllBytes(filesToCompress[i].FullPath);
                        byte[] compressed;
                        string encodedTree;

                        if (algorithm == "Huffman")
                        {
                            Hunffman h = new Hunffman();
                            h.Compress(data);
                            compressed = h.BitArrayToByteArray(h.Tobitarray(data));
                            encodedTree = h.GetEncodedTree();
                        }
                        else if (algorithm == "ShannonFano")
                        {
                            ShannonFano s = new ShannonFano();
                            s.Compress(data);
                            compressed = s.BitArrayToByteArray(s.Tobitarray(data));
                            encodedTree = s.GetEncodedTree();
                        }
                        else
                        {
                            MessageBox.Show("خوارزمية غير مدعومة!");
                            return;
                        }

                        writer.Write(filesToCompress[i].RelativePath);
                        writer.Write(encodedTree);
                        writer.Write(compressed.Length);
                        writer.Write(compressed);

                        progressBar1.Value = i + 1;
                        status.Text = $"تم استئناف وضغط الملف {i + 1} من {filesToCompress.Count}";
                        totalOriginalSize += data.Length;
                        totalCompressedSize += compressed.Length;

                        await Task.Delay(30);
                    }

                    double totalRatio = (1.0 - ((double)totalCompressedSize / totalOriginalSize)) * 100;
                    MessageBox.Show($"تم ضغط الأرشيف بنسبة: {totalRatio:F2}%");

                    filesToCompress.Clear();

                    listBoxObject.Items.Clear();
                    lastCompressedIndex = 0;
                }

                if (isCanceled && File.Exists(archiveFilePath))
                {
                    try
                    {
                        File.Delete(archiveFilePath);
                        MessageBox.Show("تم إلغاء الضغط وحذف الملف الناتج.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("لم يتم حذف الملف الناتج: " + ex.Message);
                    }
                }
                isCanceled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الاستئناف: " + ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            isCanceled = true;
            ctsStop?.Cancel();
            status.Text = "تم إلغاء عملية الضغط.";

        }

        
        private void contextMenuButtonAdd_Click(object sender, EventArgs e)
        {
              ContextMenuIntegration.AddContextMenu(Application.ExecutablePath);

        }

        private void contextMenuButtonRemove_Click(object sender, EventArgs e)
        {
            ContextMenuIntegration.RemoveContextMenu();

        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                passwordBox.Enabled = true;
                passwordBox.BackColor = Color.Khaki;
            }
            else {
                passwordBox.Enabled = false;
                
                passwordBox.BackColor = Color.Gray;
            }
        }

        private void normalRadio_CheckedChanged(object sender, EventArgs e)
        {
            pauseButton.Enabled = true;
            resumeButton.Enabled = true;
        }

        private void fastRadio_CheckedChanged(object sender, EventArgs e)
        {
            pauseButton.Enabled = false;
            resumeButton.Enabled = false;
        }
    }

    public class FileEntry
    {
        public string FullPath { get; set; }
        public string RelativePath { get; set; }
    }
}

