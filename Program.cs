// Copyright (c) 2025 Ash Hellwig <ahellwig.dev@gmail.com>
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using System.CommandLine;
using System.IO;

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
    /// Creates the directory specified if it does not exist at
    /// (<paramref name="directory" />).
    /// </summary>
    /// <param name="directory">Path to directory to create.</param>
    static void CreateDirIfNotExist(DirectoryInfo directory)
    {
        try
        {
            bool deleteDirectory = false;
            string Key = directory.FullName;

            if (Directory.Exists(directory.FullName))
            {
                do
                {
                    Console.WriteLine("Output directory already exists.");

                    ConsoleKey response;
                    do
                    {
                        Console.Write("Would you like to recreate this directory?");
                        response = Console.ReadKey(false).Key;
                        if (response != ConsoleKey.Enter)
                        {
                            Console.WriteLine();
                        }
                    } while (response != ConsoleKey.Y && response != ConsoleKey.N);

                    deleteDirectory = response == ConsoleKey.Y;
                } while (!deleteDirectory);

                if (deleteDirectory)
                {
                    Console.WriteLine("You selected to delete the directory.");
                    Directory.Delete(directory.FullName);
                    Console.WriteLine("Directory deleted.");
                }
            }

            DirectoryInfo di = Directory.CreateDirectory(directory.FullName);
            Console.WriteLine(
                "The directory was created successfully at {0}.",
                Directory.GetCreationTime(directory.FullName)
            );
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to create the directory: {0}", e.ToString());
        }
        finally { }
    }
}
