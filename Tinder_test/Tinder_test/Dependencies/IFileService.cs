using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tinder_test.Dependencies{

    public interface IFileService {
        string ReadFileFromAssets(string fileName);
        string ReadFile(string fileName, System.Environment.SpecialFolder filePath);
        void SaveFile(string fileName, string content);
    }
}
