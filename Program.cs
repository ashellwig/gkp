using System.CommandLine;

namespace gkp;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var outputDirectory = new Option<DirectoryInfo?>(
            name: "--directory",
            description: "Output directory for the key files."
        );
    }
}