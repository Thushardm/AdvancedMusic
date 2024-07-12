using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace AdvancedMusic
{
    public partial class Form1 : Form
    {
        BindingSource AlbumBindingSource = new BindingSource();
        BindingSource TrackBindingSource = new BindingSource();

        List<Album> albums = new List<Album>();
        List<Tracks> tracks = new List<Tracks>();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            //connect the list to the grid view control
            albums = albumsDAO.getAllAlbums();

            AlbumBindingSource.DataSource = albums;

            dataGridView1.DataSource = AlbumBindingSource;

            string url = "https://upload.wikimedia.org/wikipedia/en/thumb/3/39/8milecover.jpg/220px-8milecover.jpg"; // Replace with your actual URL

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "WindowsMusicApp/1.0 (your-email@example.com)");

                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        Image image = Image.FromStream(stream);
                        pictureBox1.Image = image;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            //connect the list to the grid view control
            AlbumBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);
            dataGridView1.DataSource = AlbumBindingSource;
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("Clicked");
            DataGridView dataGridView = (DataGridView)sender;
            //Get the row number clicked
            int rowClicked = dataGridView.CurrentRow.Index;
            //MessageBox.Show("You clicked row " + rowClicked);

            String imageURL = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();
            //MessageBox.Show("URL= " + imageURL);
            string url = imageURL; // Replace with your actual URL
            //pictureBox1.Load(url);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "WindowsMusicApp/1.0 (your-email@example.com)");

                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        Image image = Image.FromStream(stream);
                        pictureBox1.Image = image;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }

            }
            TrackBindingSource.DataSource = albums[rowClicked].Tracks;

            dataGridView2.DataSource = TrackBindingSource;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //add a new item to database
            Album album = new Album
            {
                AlbumName = txt_name.Text,
                ArtistName = txt_artist.Text,
                Year = Int32.Parse(txt_year.Text),
                ImageURL = txt_url.Text,
                Description = txt_desc.Text
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneAlbum(album);
            MessageBox.Show(result + "new row(s) inserted");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Get the row number clicked
            int rowClicked = dataGridView2.CurrentRow.Index;
            int trackID = (int)dataGridView2.Rows[rowClicked].Cells[0].Value;
            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.deleteTrack(trackID);
            MessageBox.Show(result + " Row/s Deleted");
            dataGridView2.DataSource = null;
            albums = albumsDAO.getAllAlbums();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //add a new item to database
            Tracks t = new Tracks
            {
                Name = txt_tname.Text,
                Number = Int32.Parse(txt_tno.Text),
                VideoURL = txt_turl.Text,
                Lyrics = txt_tlyrics.Text,
                AlbumID = Int32.Parse(txt_alid.Text),
            };
            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneTrack(t);
            MessageBox.Show(result + "new row(s) inserted");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            int rowClicked = dataGridView.CurrentRow.Index;
            string videoID = dataGridView.Rows[rowClicked].Cells[3].Value.ToString();
            webView2.Source = new Uri(videoID);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            //connect the list to the grid view control
            tracks = albumsDAO.getAlltitles();

            TrackBindingSource.DataSource = tracks;

            dataGridView2.DataSource = TrackBindingSource;
        }
    }
}