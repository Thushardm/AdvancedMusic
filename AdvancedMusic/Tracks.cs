namespace AdvancedMusic
{
    public class Tracks
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }

        public string VideoURL { get; set; } = string.Empty;
        public string Lyrics { get; set; } = string.Empty;
        public int AlbumID { get; set; }
    }
}