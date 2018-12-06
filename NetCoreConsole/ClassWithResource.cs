using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetCoreConsole
{
    public class ClassWithResource : IDisposable
    {
        private readonly string _filePath;    
        private readonly FileStream _fs;

        public ClassWithResource()
        {
            _filePath = @"c:\temp\temp.txt";
            _fs = new FileStream(_filePath, FileMode.OpenOrCreate);
        }

        public void DoStuff()
        {
            _fs.Seek(0, SeekOrigin.End);
            _fs.WriteByte(1);
        }

        
        public void Dispose()
        {
            _fs.Flush();
            _fs.Dispose();
        }

    }
}
