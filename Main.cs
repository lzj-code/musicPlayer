using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace where_and_how
{
    public partial class Main : Form
    {
        NAudio.Wave.WaveOutEvent player;
        NAudio.Wave.AudioFileReader reader;
        Library library;
        TimeSpan t;
        public Main()
        {
            InitializeComponent();
            library = new Library();
            splitContainer1.Panel2.Controls.Add(pictureBox1);

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            String keyword= textBox1.Text.Trim();
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("请先勾选搜索项！");
                return;
            }
            String type = checkedListBox1.CheckedItems[0].ToString();
            if (type == "专辑")
            {
                SearchContextMenuStripOpening();
                SearchContextMenuStrip.Show(button1, new Point(0, button1.Width));
            }
            if (type == "歌手")
            {
                SearchContextMenuStrip2Opening();
                SearchContextMenuStrip2.Show(button1, new Point(0, button1.Width));
            }
            if (type == "音乐名")
            {
                SearchContextMenuStrip3Opening();
                SearchContextMenuStrip3.Show(button1, new Point(0, button1.Width));
            }
            if (type == "其他")
                MessageBox.Show("暂未开发！");
            MessageBox.Show(type+" "+keyword);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Music music = new Music();
            String musicName=nameTextBox.Text.Trim();
            if (musicName == "")
                musicName = "未命名音乐";
            String musicAuthor=singerName.Text.Trim();
            if (musicAuthor == "")
                musicAuthor = "未知作者";
            String musicDescription=MusicDescription.Text.Trim();
            if (musicDescription == "")
                musicDescription = "无音乐描述";
            int albumId=(int)AlbumID.Value;
            music.setAlbumID(albumId);
            MessageBox.Show(musicName + musicAuthor+ musicDescription+"专辑编号："+albumId);
            openFileDialog1=new OpenFileDialog();
            openFileDialog1.ShowDialog(this);
            String path= openFileDialog1.FileName;
            if (path.Contains(".mp3"))
            {
                DialogResult d = MessageBox.Show("感谢上传，下面你要听一下吗？", "危险", MessageBoxButtons.YesNoCancel);
                if (d == DialogResult.Yes)
                {
                    if (player != null) { player.Stop(); player.Dispose(); }
                    if (reader != null) reader.Dispose();
                    reader = new NAudio.Wave.AudioFileReader(path);
                    player = new NAudio.Wave.WaveOutEvent();
                    player.Init(reader);
                    player.Play();
                }
            }
            else
            {
                MessageBox.Show("请上传mp3格式的文件！");
                return;
            }
            music.setMusicName(musicName);
            music.setMusicAuthor(musicAuthor);
            music.setMusicDescription(musicDescription);
            music.setAlbumName(library.getAlbumNameByAlbumID(albumId));
            music.setMp3Path(path);
            library.storeMusic(music,albumId);
            MessageBox.Show(library.GetMusics()[0].getMusicName());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Album album = new Album();
            String albumName = albumNameTextBox.Text.Trim();
            if (albumName == "")
                albumName = "未命名专辑";
            String albumDescription = albumDescriptionTextBox.Text.Trim();
            if (albumDescription == "")
                albumDescription = "无相关描述";
            int sno=(int)singerNumericUpDown.Value;
            album.setSingerID(sno);
            album.setAlbumName(albumName);
            album.setAlbumDescription(albumDescription);
            album.setSingerName(library.getSingerNameById(sno));
            DialogResult d = MessageBox.Show("是否需要上传图片？","迷茫",MessageBoxButtons.YesNoCancel);
            String path = "";
            if (d == DialogResult.Yes)
            {
                openFileDialog1 = new OpenFileDialog();
                openFileDialog1.ShowDialog(this);
                path= openFileDialog1.FileName;
            }
            album.setImagePath(path);
            albumDescriptionTextBox.Text = "";
            albumNameTextBox.Text = "";
            library.storeAlbum(album,sno);
            MessageBox.Show("专辑名："+album.getAlbumName()+"专辑描述："+album.getAlbumDescription()+"专辑图片地址："+(path==""?"未上传地址":path));
        }

        private void singerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void MusicDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

            
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            contextMenuStrip2Opening();
            contextMenuStrip2.Show(playBtn,new Point(0,playBtn.Width));
            
        }
        private void contextMenuStrip2Opening()
        {
            contextMenuStrip2.Items.Clear();
            List<Album> albums= library.GetAlbums();
            List<String> paths = new List<String>();
            foreach (Album album in albums)
            {
                contextMenuStrip2.Items.Add(album.getAlbumName());
                paths.Add(album.getImagePath());
            }
            for (int i = 0; i < contextMenuStrip2.Items.Count; i++)
            {
                int idx = i;
                contextMenuStrip2.Items[idx].Click += (s, ev) =>
                {
                    contextMenuStrip1Opening(library.getMusicsByAlbumId(idx));
                    contextMenuStrip1.Show(playBtn, new Point(0, playBtn.Width));
                    if(pictureBox1.Image!=null) { pictureBox1.Image.Dispose(); pictureBox1.Image = null; }
                    pictureBox1.Image=Image.FromFile(paths[idx]);

                };
            }
        }
        private void contextMenuStrip1Opening(List<Music> musics)
        {
            contextMenuStrip1.Items.Clear();
            List<String> paths = new List<String>();
            foreach (Music music in musics)
            {
                contextMenuStrip1.Items.Add(music.getMusicName());
                paths.Add(music.getMp3PAth());
            }
          
            for (int i= 0; i < contextMenuStrip1.Items.Count; i++)
            {
                int idx = i;
                contextMenuStrip1.Items[idx].Click += (s, ev) =>
                {
                    DialogResult d=MessageBox.Show("即将播放音乐:" + contextMenuStrip1.Items[idx].Text,"警告",MessageBoxButtons.YesNoCancel);
                    if (d == DialogResult.Yes)
                    {
                        if (player != null) { player.Stop(); player.Dispose(); }
                        if (reader != null) reader.Dispose();
                        reader = new NAudio.Wave.AudioFileReader(paths[idx]);
                        player = new NAudio.Wave.WaveOutEvent();
                        player.Init(reader);
                        player.Play();
                    }  
                };
            }
        }

        private void SearchContextMenuStripOpening()//专辑名
        {
            SearchContextMenuStrip.Items.Clear();
            List<Album> albums=library.GetAlbums();
            List<int> ids=new List<int>();
            
            foreach (Album album in albums) {
                SearchContextMenuStrip.Items.Add(album.getAlbumName());
                ids.Add(album.getAlbumId());
            }
            for (int i = 0; i < SearchContextMenuStrip.Items.Count; i++)
            {
                int idx = i;
                SearchContextMenuStrip.Items[idx].Click += (a, b) =>
                {
                    DialogResult d=MessageBox.Show("要查看" + albums[ids[idx]].getAlbumName() + "的详情吗？", "彳   亍", MessageBoxButtons.YesNoCancel);
                    if (d == DialogResult.Yes)
                    {
                        MessageBox.Show("专辑ID：" + albums[ids[idx]].getAlbumId() + "\n" +
                                        "专辑名：" + albums[ids[idx]].getAlbumName() + "\n" +
                                        "专辑描述：" + albums[ids[idx]].getAlbumDescription() + "\n" +
                                        "专辑图片地址：" + albums[ids[idx]].getImagePath()
                            );
                    }
                    else
                    {
                        d = MessageBox.Show("要删除专辑" + albums[ids[idx]].getAlbumName() + "吗？", "毁灭性的", MessageBoxButtons.YesNoCancel);
                        if (d == DialogResult.Yes)
                        {
                            library.removeAlbumsById(albums[ids[idx]].getAlbumId());
                        }
                    }
                };
                
            }
        }
        private void SearchContextMenuStrip2Opening()//歌手
        {
            SearchContextMenuStrip2.Items.Clear();
            List<Singer> singers= library.GetSingers();
            List<int> ids = new List<int>();

            foreach (Singer singer in singers)
            {
                SearchContextMenuStrip2.Items.Add(singer.getName());
                ids.Add(singer.getSingerId());
            }
            for (int i = 0; i < SearchContextMenuStrip2.Items.Count; i++)
            {
                int idx = i;
                SearchContextMenuStrip2.Items[idx].Click += (a, b) =>
                {
                    DialogResult d = MessageBox.Show("要查看" + singers[ids[idx]].getName() + "的详情吗？", "彳   亍", MessageBoxButtons.YesNoCancel);
                    if (d == DialogResult.Yes)
                    {
                        MessageBox.Show("歌手ID：" + singers[ids[idx]].getSingerId() + "\n" +
                                        "歌手名：" + singers[ids[idx]].getName() + "\n" +
                                        "歌手描述：" + singers[ids[idx]].getDescription() + "\n"
                            );
                    }
                    else
                    {
                        d = MessageBox.Show("要删除歌手" + singers[ids[idx]].getName() + "吗？", "毁灭性的", MessageBoxButtons.YesNoCancel);
                        if (d == DialogResult.Yes)
                        {
                            library.removeSingerById(singers[ids[idx]].getSingerId());
                        }
                    }
                };
            }
        }
        private void SearchContextMenuStrip3Opening()//音乐名
        {
            SearchContextMenuStrip3.Items.Clear();
            List<Music> musics = library.GetMusics();
            List<int> ids = new List<int>();

            foreach (Music music in musics)
            {
                SearchContextMenuStrip3.Items.Add(music.getMusicName());
                ids.Add(music.getMusicId());
            }
            for (int i = 0; i < SearchContextMenuStrip3.Items.Count; i++)
            {
                int idx = i;
                SearchContextMenuStrip3.Items[idx].Click += (a, b) =>
                {
                    DialogResult d = MessageBox.Show("要查看" + musics[ids[idx]].getMusicName() + "的详情吗？", "彳   亍", MessageBoxButtons.YesNoCancel);
                    if (d == DialogResult.Yes)
                    {
                        MessageBox.Show("音乐ID：" + musics[ids[idx]].getMusicId() + "\n" +
                                        "音乐名：" + musics[ids[idx]].getMusicName() + "\n" +
                                        "音乐描述：" + musics[ids[idx]].getMusicDescription() + "\n" +
                                        "歌手：" + musics[ids[idx]].getMusicAuthor() + "\n" +
                                        "音乐路径：" + musics[ids[idx]].getMp3PAth()
                            );
                    }
                    else
                    {
                        d = MessageBox.Show("要删除音乐" + musics[ids[idx]].getMusicName() + "吗？", "毁灭性的", MessageBoxButtons.YesNoCancel);
                        if (d == DialogResult.Yes)
                        {
                            library.removeMusicById(musics[ids[idx]].getMusicId());
                        }
                    }
                };
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
           DialogResult d= MessageBox.Show("Question:where and how\n" +
                "Are you happy in this modern world\n" +
                "Aren't you tired trying fill that void!"
                ,"欸",MessageBoxButtons.YesNoCancel);
            if (!(d == DialogResult.Yes))
            {
                MessageBox.Show("译文：只有音乐最安全！");
            }
        }

        private void singerBtn_Click(object sender, EventArgs e)
        {
            Singer singer=new Singer();
            String name=singerNameTextBox.Text.Trim();
            String description=singerDescriptionTextBox.Text.Trim();
            singer.setName(name);
            singer.setDescription(description);
            library.storeSinger(singer);
            MessageBox.Show("歌手名："+singer.getName()+"\n"+"歌手描述："+singer.getDescription());
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (stopBtn.Text.Trim() == "暂停")
            {
                if (player != null) { player.Stop(); }
                if (reader != null)
                {t = reader.CurrentTime;}
                stopBtn.Text = "继续";
            }
            else
            {
                reader.CurrentTime = t;
                player.Play();
                stopBtn.Text = "暂停";
            }

        }

        private void switchBtn_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            List<Music> musics = library.GetMusics();
            List<Album> albums = library.GetAlbums();
            int d = random.Next(0,albums.Count());
            if (albums[d].getImagePath() != null)
            {
               if(pictureBox1.Image!=null) { pictureBox1.Image.Dispose(); pictureBox1.Image = null; }
                pictureBox1.Image = Image.FromFile(albums[d].getImagePath());
            }
            if (albums[d].getMusics().Count == 0)
                return;
            int dm= random.Next(0, albums[d].getMusics().Count);
            if (player != null) { player.Stop(); player.Dispose(); }
            if (reader != null) reader.Dispose();
            reader = new NAudio.Wave.AudioFileReader(albums[d].getMusics()[dm].getMp3PAth());
            player = new NAudio.Wave.WaveOutEvent();
            player.Init(reader);
            player.Play();

        }

       
    }
}
