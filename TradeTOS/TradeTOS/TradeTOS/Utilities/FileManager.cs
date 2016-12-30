using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Utilities
{
    public class FileManager
    {
        private const string ASSEMBLY_NAME = "TradeTOS";
        private AssemblyName an;
        private Assembly assembly = null;
        public FileManager()
        {
            an = new AssemblyName();
            an.Name = ASSEMBLY_NAME;
            assembly = Assembly.Load(an);
        }

        public string Load(string fileName)
        {
            try
            {
                Stream stream = assembly.GetManifestResourceStream(fileName);
                string fileContent = string.Empty;
                if (stream != null)
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }

                return fileContent;
            }
            catch (Exception ex)
            {
                // Log exception
                return string.Empty;
            }
        }

        public bool CheckFileHasContent(string fileName)
        {
            try
            {
                Stream stream = assembly.GetManifestResourceStream(fileName);
                if (stream != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
