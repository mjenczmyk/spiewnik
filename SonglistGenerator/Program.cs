using System.IO;

namespace SonglistGenerator
{
    class Program
    {
        const string latexFileExtension = ".tex";
        public const string ChapterMasterFile = "master" + latexFileExtension;
        public const string SongbookMainFile = "main" + latexFileExtension;
        public const string LatexFileFilter = "*" + latexFileExtension;

        static void Main(string[] args)
        {            
            var logger = new ConsoleLogger();
            logger.WriteLine("Hello World!");

            if (args.Length != 3)
            {
                logger.WriteLine("Program should be executed with three arguments: " +
                    "input folder with song repository (containing subfolders with artists), " +
                    "output folder where generated songbook would be saved," +
                    "and minimum allowed chapter size (smaller chapters would be consolidated).");                
                return;
            }

            var songRepositoryFolder = args[0];
            logger.WriteLine($"Program will generate list of songs from {songRepositoryFolder}");

            var outputPath = args[1];
            logger.WriteLine($"Folder with full songbook (including new main and master files) would be saved at {outputPath}");

            var minimumAllowedChapterSize = int.Parse(args[2]);
            logger.WriteLine($"Minimum allowed chapter size: {minimumAllowedChapterSize}. Smaller chapters would be consolidated into one.");

            Utilities.CopyAll(new DirectoryInfo(songRepositoryFolder), new DirectoryInfo(outputPath));

            var generator = new Generator(logger, outputPath) { MinimumAllowedChapterSize = minimumAllowedChapterSize };
            generator.Initialize();
            generator.Generate();
        }
    }
}
