using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace AdvancedMusic
{
    internal class AlbumsDAO
    {
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=music;";
        public AlbumsDAO()
        {

        }
        public List<Album> getAllAlbums()
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();
            //connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("SELECT * FROM ALBUMS", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album album = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5),
                    };
                    album.Tracks = getTracksForAlbum(album.ID);
                    returnThese.Add(album);
                }
                connection.Close();

                return returnThese;
            }
        }
        public List<Album> searchTitles(String searchTerm)
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();
            //connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            String searchWildPhrase = "%" + searchTerm + "%";
            //define sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM ALBUMS WHERE ALBUM_TITLE LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album album = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5),
                    };
                    returnThese.Add(album);
                }
                connection.Close();

                return returnThese;
            }
        }
        internal int addOneAlbum(Album album)
        {
            //connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("INSERT INTO albums (ALBUM_TITLE,ARTIST,YEAR,IMAGE_NAME,DESCRIPTION) VALUES (@albumtitle,@artist,@year,@imageurl,@description)", connection);

            command.Parameters.AddWithValue("@albumtitle", album.AlbumName);
            command.Parameters.AddWithValue("@artist", album.ArtistName);
            command.Parameters.AddWithValue("@year", album.Year);
            command.Parameters.AddWithValue("@imageURL", album.ImageURL);
            command.Parameters.AddWithValue("@description", album.Description);

            int newrows = command.ExecuteNonQuery();

            connection.Close();

            return newrows;
        }
        private List<Tracks> getTracksForAlbum(int albumID)
        {
            //start with an empty list
            List<Tracks> returnThese = new List<Tracks>();
            //connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM TRACKS WHERE album_ID =@albumid";
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tracks t = new Tracks
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Number = reader.GetInt32(2),
                        VideoURL = reader.GetString(3),
                        Lyrics = reader.GetString(4),
                        AlbumID = reader.GetInt32(5),
                    };
                    returnThese.Add(t);
                }
                connection.Close();

                return returnThese;
            }
        }
        internal int deleteTrack(int trackID)
        {
            //connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("DELETE FROM TRACKS WHERE TRACKS.ID=@trackID", connection);

            command.Parameters.AddWithValue("@trackID", trackID);

            int result = command.ExecuteNonQuery();

            connection.Close();

            return result;
        }

        internal int addOneTrack(Tracks track)
        {
            //connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("INSERT INTO tracks (TRACK_TITLE,NUMBER,VIDEO_URL,lyrics,album_ID) VALUES (@title,@no,@url,@lyrics,@alid)", connection);

            command.Parameters.AddWithValue("@title", track.Name);
            command.Parameters.AddWithValue("@no", track.Number);
            command.Parameters.AddWithValue("@url", track.VideoURL);
            command.Parameters.AddWithValue("@lyrics", track.Lyrics);
            command.Parameters.AddWithValue("@alid", track.AlbumID);

            int newrows = command.ExecuteNonQuery();

            connection.Close();

            return newrows;
        }

        internal List<Tracks> getAlltitles()
        {
            //start with an empty list
            List<Tracks> returnThese = new List<Tracks>();
            //connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("SELECT * FROM TRACKS", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tracks track = new Tracks
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Number = reader.GetInt32(2),
                        VideoURL = reader.GetString(3),
                        Lyrics = reader.GetString(4),
                        AlbumID = reader.GetInt32(5),
                    };
                    returnThese.Add(track);
                }
                connection.Close();

                return returnThese;
            }
        }
    }
}