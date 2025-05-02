// Copyright (c) 2025 Ash Hellwig <ahellwig.dev@gmail.com>
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

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

        var rootCommand = new RootCommand("Generate a private and public key pair.");
        rootCommand.AddOption(outputDirectory);

        rootCommand.SetHandler(
            (directory) =>
            {
                CreateDirIfNotExist(directory!);
            },
            outputDirectory
        );

        return await rootCommand.InvokeAsync(args);
    }

    /// <summary>
    /// Creates the directory specified if it does not exist.
    /// </summary>
    /// <param name="directory">Path to directory to create.</param>
    static void CreateDirIfNotExist(DirectoryInfo directory)
    {
        try
        {
            if (Directory.Exists(directory.FullName))
            {
                Console.WriteLine("Output directory already exists.");
                return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to create the directory: {0}", e.ToString());
        }
        finally { }
    }
}
