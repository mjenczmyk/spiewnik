using System.Collections.Generic;
using System.IO;

namespace SonglistGenerator
{
    public class Generator
    {
        private readonly Songlist songlist;
        private readonly string workingDirectory;

        public IEnumerable<Chapter> Chapters => this.songlist.Chapters;

        public int MinimumAllowedChapterSize { get; set; }

        public Generator(ConsoleLogger logger, string workingDirectory)
        {
            this.songlist = new Songlist(logger);
            this.workingDirectory = workingDirectory;
        }

        public void Initialize()
        {
            var folders = Directory.GetDirectories(this.workingDirectory);
            songlist.CreateListOfChapters(folders);
            songlist.CreateListOfSongs();
            songlist.Initialize();
        }

        public void Generate()
        {
            songlist.WrapCarets();
            songlist.ConsolidateChapters(this.MinimumAllowedChapterSize);
            songlist.ReplaceMainMasters(this.workingDirectory);
        }
    }
}
