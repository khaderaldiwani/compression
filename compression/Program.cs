using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace compression
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (args.Length == 2 && args[0] == "--extract" && File.Exists(args[1]))
                {
                    string archivePath = args[1];
                    string outputDir = Path.GetDirectoryName(archivePath);

                    try
                    {
                        using (BinaryReader reader = new BinaryReader(File.OpenRead(archivePath)))
                        {
                            string algorithm = reader.ReadString();
                            string password = reader.ReadString();
                            int fileCount = reader.ReadInt32();

                            for (int i = 0; i < fileCount; i++)
                            {
                                string name = reader.ReadString();
                                string tree = reader.ReadString();
                                int len = reader.ReadInt32();
                                byte[] data = reader.ReadBytes(len);

                                byte[] decoded;

                                if (algorithm == "Huffman")
                                {
                                    Hunffman h = new Hunffman();
                                    h.LoadEncodedTree(tree);
                                    decoded = h.Decode(new BitArray(data));
                                }
                                else if (algorithm == "ShannonFano")
                                {
                                    ShannonFano s = new ShannonFano();
                                    s.LoadEncodedTree(tree);
                                    decoded = s.Decode(new BitArray(data));
                                }
                                else
                                {
                                    MessageBox.Show("الخوارزمية غير مدعومة.");
                                    return;
                                }

                                string outPath = Path.Combine(outputDir, name);
                                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                                File.WriteAllBytes(outPath, decoded);
                            }

                            MessageBox.Show("تم فك الضغط بنجاح في نفس المجلد.");
                        }

                        return; // إنهاء البرنامج بعد فك الضغط التلقائي
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("خطأ أثناء فك الضغط: " + ex.Message);
                        return;
                    }
                }

                Application.Run(new Form1(args));

      
        }
    }
    
}
