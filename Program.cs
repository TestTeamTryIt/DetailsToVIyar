using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using static System.Console;


namespace DetailsToViyar
{
    class Program
    {
        static string GetFilePath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            int i = 0;
            foreach (var file in di.GetFiles())
            {
                WriteLine($"\t   {++i} {file.Name}");
            }
            Write("\n\t > Оберіить номер файлу: ");
            int n = int.Parse(Console.ReadLine());
            if (n > 0 && n <= di.GetFiles().Length)
            {
                WriteLine($"\n\t   Обрали: '{di.GetFiles()[n - 1].Name}'");
                return di.GetFiles()[n - 1].FullName;
            }
            else
            {
                WriteLine("\n\t  Помилкове введення!");
                return default;
            }
        }
        static void Main(string[] args)
        {

            string rootFolder = Directory.GetCurrentDirectory() + @"\Convert\";
            string pathIn = rootFolder + @"Input\";
            string pathOut = rootFolder + @"Output\";
            if (!Directory.Exists(pathIn))
                Directory.CreateDirectory(pathIn);
            if (!Directory.Exists(pathOut))
                Directory.CreateDirectory(pathOut);

            string path;
            string pathProjectIn;
            string pathProjectOut;
            int i = 0;

            WriteLine();
            Write("\t   Вибір файлів для роботи...");
            WriteLine("\n\t======================================================================");
            WriteLine($"\t   Файли з переліком деталей:");
            path = GetFilePath(rootFolder);//select *.txt file with details
            WriteLine($"\n\t   Файли ViyarPRO для вставлення деталей зі списку:");
            pathProjectIn = GetFilePath(pathIn);// select *.project file for inserting details
            string outputFileName = Path.GetFileNameWithoutExtension(pathProjectIn) + "_out" + Path.GetExtension(pathProjectIn);
            pathProjectOut = pathOut + outputFileName;// name for new *.project file with details


                SuppliersList suppliersList = new SuppliersList();
                string s;
                string[] separators = new string[] { "\t", "\r", "\n" };
                string[] subs;

                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        s = sr.ReadToEnd();
                        subs = s.Split(separators, StringSplitOptions.None);
                    }
                    i = 0;
                    while (i < subs.Length - 6)
                    {
                        if (subs[i + 4] != "" && !subs[i + 4].Contains("T1") && !subs[i + 4].Contains("M1") && !subs[i + 4].Contains("M5"))
                        {
                            if (subs[i + 4].Contains("SP1"))
                                suppliersList.AddDetail(0, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // viyar
                            else if (subs[i + 4].Contains("SP2"))
                                suppliersList.AddDetail(1, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // mt
                            else if (subs[i + 4].Contains("SP3"))
                                suppliersList.AddDetail(2, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // вибор
                            else if (subs[i + 4].Contains("SP4"))
                                suppliersList.AddDetail(3, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // hafele
                            else if (subs[i + 4].Contains("SP5"))
                                suppliersList.AddDetail(4, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // ads
                            else if (subs[i + 4].Contains("SP6"))
                                suppliersList.AddDetail(5, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // vdm
                            else if (subs[i + 4].Contains("SP7"))
                                suppliersList.AddDetail(6, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // kronas
                            else
                                suppliersList.AddDetail(7, subs[i], subs[i + 1], subs[i + 2], subs[i + 3], subs[i + 4]); // other
                        }
                        i += 6;
                    }
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }

                // insert details to Viyar PRO *.project file
                WriteLine("\t   Вставлення деталей у ViyarPRO *.project файл...");
                WriteLine("\t======================================================================");
                Supplier currentSupplier = suppliersList.GetSupplier(0); //0 => Viyar

                //foreach (var detail in currentSupplier)
                //{
                //    WriteLine($"\t   {detail.Article}   " +
                //        $"{detail.Name}   " +
                //        $"{detail.Units}   " +
                //        $"{detail.Quantity}   " +
                //        $"{detail.Code}");
                //}

                XDocument doc;
                XElement root;

                if (!File.Exists(pathProjectIn))
                {
                    WriteLine("\n\t   Вхідний файл не знайдено!");
                }
                else
                {
                    doc = XDocument.Load(pathProjectIn);
                    root = doc.Element("project");
                    if ((root.Element("viyar").Elements()
                        .Where(r => r.Name == "products").Count()) > 0)
                    {
                        root.Element("viyar").Element("products").Remove();
                    }


                        root.Element("viyar").Add(new XElement("products"));
                        i = 0;
                    foreach (var detail in currentSupplier)
                    {
                        root.Element("viyar")
                        .Element("products")
                        .Add(new XElement("product",
                        new XAttribute("id", ++i),
                        new XAttribute("article", detail.Article),
                        new XAttribute("name", detail.Name),
                        new XAttribute("amount", detail.Quantity)));
                    }
                    doc.Save(pathProjectOut);
                }

                WriteLine("\n\t   Збереженнядеталей у файли *.txt...");
                WriteLine("\t======================================================================");
                // save suppliers details to *.txt files
                foreach (var supplier in suppliersList)
                {
                    if (supplier.DetailsCount() != 0 /*&& supplier.Name != "Viyar"*/)
                    {
                        string supplierName = supplier.Name;
                        WriteLine($"\t   Збереження {supplierName} деталей у {supplierName}.txt...");
                        using (StreamWriter sw = new StreamWriter($@"{rootFolder}Output\{supplierName}.txt"))
                        {
                            sw.WriteLine("Поз.\tАртикул\tНайменування\tКількість\tОдиниці виміру");
                            i = 0;
                            foreach (var detail in supplier)
                            {
                                sw.WriteLine($"{++i}\t{detail.Article}\t{detail.Name}\t{detail.Quantity}\t{detail.Units}");
                            }
                        }
                    }
                }           
            ConsoleKeyInfo ki = new ConsoleKeyInfo();
            Write($"\n\t > Натисніть клавішу для виходу: ");
            ki = ReadKey();
        }
    }
}
