using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SonglistGenerator
{
    public class Songlist
    {
        private ConsoleLogger logger;

        public IEnumerable<Chapter> Chapters { get; private set; }

        public Songlist(ConsoleLogger logger)
        {
            this.logger = logger;
        }

        public void CreateListOfChapters(string[] folders)
        {
            var chapters = new List<Chapter>();
            foreach (var folder in folders)
            {
                if (!File.Exists(Path.Combine(folder, Program.ChapterMasterFile)))
                {
                    logger.WriteLine($"Folder {folder} does not cotain {Program.ChapterMasterFile}, ignoring");
                    continue;
                }

                var chapter = new Chapter(folder);
                chapters.Add(chapter);
            }

            this.Chapters = chapters.OrderBy(x => x.ChapterName, StringComparerProvider.PolishComparer);
            logger.WriteLine($"Found {chapters.Count} chapters.");
        }

        public void CreateListOfSongs()
        {
            foreach (var chapter in this.Chapters)
            {
                var latexFilesInsideChapter = Directory.GetFiles(chapter.FilePath, Program.LatexFileFilter);

                foreach (var latexFilePath in latexFilesInsideChapter)
                {
                    if (Path.GetFileName(latexFilePath) == Program.ChapterMasterFile)
                    {
                        // Ignore chapter master file
                        continue;
                    }

                    var song = new Song(latexFilePath);
                    chapter.Songs.Add(song);
                }

                logger.WriteLine($"Found {chapter.Songs.Count} songs in chapter {chapter.FilePath})");
            }
        }

        public void Initialize()
        {
            foreach (var chapter in this.Chapters)
            {
                chapter.Initialize();
                logger.WriteLine($"   Chapter \"{chapter.ChapterName}\" is located in folder \"{chapter.FolderName}\", UseArtists: {chapter.UseArtists}, contains {chapter.Songs.Count} songs");
                foreach (var song in chapter.Songs)
                {
                    song.Initialize();
                    logger.WriteLine($"      Song \"{song.Title}\", author \"{song.Author}\", artist \"{song.Artist}\"");
                }
            }
        }

        private string NewMainFile()
        {
            var listOfChapters = new List<string>();
            foreach (var chapter in this.Chapters)
            {
                var masterFile = chapter.FolderName != string.Empty ? $"{chapter.FolderName}/master" : "master";
                listOfChapters.Add($"\\include{{{masterFile}}}");
            }

            return string.Join(Environment.NewLine, listOfChapters);
        }

        public void ReplaceMainMasters(string folderToUpdate)
        {
            var fileCreator = new FilesUpdater(folderToUpdate);
            fileCreator.ReplaceMainFile(this.NewMainFile());
            foreach (var chapter in this.Chapters)
            {
                fileCreator.ReplaceMasterFile(chapter.FolderName, chapter.NewMasterFile());
            }
        }

        public void ConsolidateChapters(int minimumAllowedChapterSize)
        {
            var songsToOthersChapter = this.Chapters.Where(x => x.Songs.Count < minimumAllowedChapterSize).SelectMany(x => x.Songs).OrderBy(x=>x.Title, StringComparerProvider.PolishComparer);
            if (songsToOthersChapter.Any())
            {
                var newChaptersList = new List<Chapter>();
                newChaptersList.AddRange(this.Chapters.Where(x => x.Songs.Count >= minimumAllowedChapterSize));
                var othersChapter = new Chapter(null)
                {
                    ChapterName = "Pozostałe",
                    UseArtists = true,
                    FolderName = string.Empty,
                };
                othersChapter.Songs.AddRange(songsToOthersChapter);
                newChaptersList.Add(othersChapter);
                this.Chapters = newChaptersList;


                this.logger.WriteLine($"ConsolidateChapters: added {othersChapter.Songs.Count} songs to consolidated chapter.");
            }
            else
            {
                this.logger.WriteLine($"ConsolidateChapters: there are no chapters with less than {minimumAllowedChapterSize} songs.");
            }
        }

        public void WrapCarets()
        {
            var songFiles = new List<string>();
            var listsOfSongs = this.Chapters.Select(x => x.Songs);
            foreach (var list in listsOfSongs)
            {
                songFiles.AddRange(list.Select(x => x.FilePath));
            }

            foreach (var file in songFiles)
            {
                var content = File.ReadAllText(file);
                content = CaretsWrapper.WrapCarets(content);
                File.WriteAllText(file, content);
            }
        }
    }
}
