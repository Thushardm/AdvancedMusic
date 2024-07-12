using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Collections.Generic;

namespace AdvancedMusic
{
    internal class Album
    {
        public int ID { get; set; }
        public String AlbumName { get; set; } = String.Empty;
        public String ArtistName { get; set; } = String.Empty; // To make String nullable
        public int Year { get; set; }
        public String ImageURL { get; set; } = String.Empty;
        public String Description { get; set; } = String.Empty;
        
        public List<Tracks> Tracks { get; set; }
    }
}