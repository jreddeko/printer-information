using System;
using System.Linq;
using System.Text;
using System.Management;
using Fclp;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;

namespace GetPrinterInformation
{
    internal class ApplicationArguments
    {
        public string PrinterName { get; set; }
        public string OutputFile { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var asdf = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            var p = new FluentCommandLineParser<ApplicationArguments>();

            // specify which property the value will be assigned too.
            p.Setup(arg => arg.PrinterName)
                .As('n', "name")
                .WithDescription("Select name of printer, if no name set will query all printers.");

            p.Setup(arg => arg.OutputFile)
                .As('o', "output")
                .Required()
                .WithDescription("Select name of output file.");

            var result = p.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine(result.ErrorText);
            }
            else
            {
                string query = String.IsNullOrEmpty(p.Object.PrinterName) ? string.Format("SELECT * from Win32_Printer")
                    : string.Format("SELECT * from Win32_Printer where name = '{0}'", p.Object.PrinterName);

                var sb = new StringBuilder();
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                using (ManagementObjectCollection coll = searcher.Get())
                {
                    try
                    {
                        foreach (ManagementObject printer in coll)
                        {
                            foreach (PropertyData property in printer.Properties)
                            {
                                Console.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
                                sb.AppendLine(string.Format("{0}: {1}", property.Name, property.Value));

                                if (property.IsArray)
                                {
                                    if (property.Value != null)
                                    {
                                        var arr = ((System.Collections.IEnumerable)property.Value).Cast<object>()
                                         .Select(x => x.ToString())
                                         .ToArray();
                                        foreach (var item in arr)
                                        {
                                            Console.WriteLine(string.Format("\t{0}", item));
                                            sb.AppendLine(string.Format("\t{0}", item));
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("-----------------------------------------------------------");
                            sb.AppendLine("-----------------------------------------------------------");
                        }
                    }
                    catch (ManagementException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                File.WriteAllText(p.Object.OutputFile, sb.ToString());
            }
        }
    }
}
