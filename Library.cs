using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace where_and_how
{
    internal class Library
    {
        List<Album> albums;
        List<Singer> singers;
        List<Music> musics;
        StreamWriter sw1 ;
        StreamWriter sw2 ;
        StreamWriter sw3 ;
        StreamWriter sw4 ;
        StreamWriter sw5 ;
        StreamReader sr1 ;
        StreamReader sr2;
        StreamReader sr3;
        StreamReader sr4;
        StreamReader sr5;
        public Library()
        {
            albums = new List<Album>();
            singers = new List<Singer>();
            musics = new List<Music>();
            Album album = new Album();
            album.setAlbumDescription("用于存储默认专辑编号的音乐");
            album.setAlbumName("佚名");
            albums.Add(album);//0号专辑用于存储未指定专辑编号的音乐
            Singer singer = new Singer();
            singer.setName("无名歌手");
            singer.setDescription("0号歌手用于存储默认专辑");
            singers.Add(singer);
            
            sw1 = new StreamWriter("singerData.txt", true);
            sw2 = new StreamWriter("albumData.txt", true);
            sw3 = new StreamWriter("musicData.txt", true);
            sw4 = new StreamWriter("album_singer.txt", true);
            sw5 = new StreamWriter("music_album.txt", true);
            sw1.Close();
            sw2.Close();
            sw3.Close();
            sw4.Close();
            sw5.Close();
            loadData();
            sw1 = new StreamWriter("singerData.txt", true);
            sw2 = new StreamWriter("albumData.txt", true);
            sw3 = new StreamWriter("musicData.txt", true);
            sw4 = new StreamWriter("album_singer.txt", true);
            sw5 = new StreamWriter("music_album.txt", true);
        }
        public void loadData()
        {
            String line;
            sr1 = new StreamReader("singerData.txt");
            sr2 = new StreamReader("albumData.txt");
            sr3 = new StreamReader("musicData.txt");
            sr4 = new StreamReader("album_singer.txt");
            sr5 = new StreamReader("music_album.txt");
            while ((line = sr1.ReadLine() )!= null)
            {
                if(line.Trim()== "") continue;
                String[] singerData = line.Split('|');
                Singer singer=new Singer();
                singer.setSingerId(int.Parse(singerData[0]));
                singer.setName(singerData[1]);
                singer.setDescription(singerData[2]);
                singers.Add(singer);
            }
            while ((line = sr2.ReadLine()) != null)
            {
                if (line.Trim() == "") continue;
                String[] albumData = line.Split('|');
                Album album=new Album();
                album.setAlbumId(int.Parse(albumData[0]));
                album.setAlbumName(albumData[1]);
                album.setAlbumDescription(albumData[2]);
                album.setImagePath(albumData[3]);
                album.setSingerName(albumData[4]);
                albums.Add(album);
            }
            while ((line = sr3.ReadLine()) != null)
            {
                if (line.Trim() == "") continue;
                Music music = new Music();
                String[] musicData = line.Split('|');
                music.setMID(int.Parse(musicData[0]));
                music.setMusicName(musicData[1]);
                music.setMusicAuthor(musicData[2]);
                music.setMusicDescription(musicData[3]);
                music.setMp3Path(musicData[4]);
                musics.Add(music);
            }
            while ((line = sr4.ReadLine()) != null)
            {
                if(line.Trim()=="") continue;
                String[] albumSingerData = line.Split('|');
                int singerId = int.Parse(albumSingerData[0]);
                Album album = new Album();
                album.setAlbumId(int.Parse(albumSingerData[1]));
                album.setAlbumName(albumSingerData[2]);
                album.setAlbumDescription(albumSingerData[3]);
                album.setImagePath(albumSingerData[4]);
                album.setSingerName(albumSingerData[5]);
                singers[singerId].addAlbum(album);
            }
            while ((line = sr5.ReadLine()) != null)
            {
                if (line.Trim() == "") continue;
                String[] albumMusicData = line.Split('|');
                int albumId = int.Parse(albumMusicData[0]);
                Music music = new Music();
                music.setMID(int.Parse(albumMusicData[1]));
                music.setMusicName(albumMusicData[2]);
                music.setMusicAuthor(albumMusicData[3]);
                music.setMusicDescription(albumMusicData[4]);
                music.setMp3Path(albumMusicData[5]);
                albums[albumId].addMusic(music);
            }
            sr1.Close();
            sr2.Close();
            sr3.Close();
            sr4.Close();
            sr5.Close();
        }
        public void storeMusic(Music music, int id =0)
        {
            if (id > albums.Count-1)
            {
                MessageBox.Show("不存在该专辑，重选或新建");
                return;
            }
            musics.Add(music);
            albums[id].addMusic(music);
            String musicInfo = music.getMusicId() + "|" + music.getMusicName() + "|" + music.getMusicAuthor() + "|" + music.getMusicDescription() + "|" + music.getMp3PAth();
            sw5.WriteLine(id+"|" +musicInfo);
            sw3.WriteLine(musicInfo);
            sw5.Flush();
            sw3.Flush();
        }
        public String getAlbumNameByAlbumID(int id)
        {
            for (int i = 0; i < albums.Count; i++)
            {
                if (albums[i].getAlbumId() == id)
                    return albums[i].getAlbumName();
            }
            return "";
        }
        public List<Album> GetAlbums()
        {
            return albums;
        }
        public List<Singer> GetSingers()
        {
            return singers;
        }
        public List<Music> GetMusics()
        {
            return musics;
        }
        public void createAlbums(Album album)
        {
            albums.Add(album);
        }
        public bool removeAlbumsById(int id)
        {
            for (int i = 0; i < albums.Count; i++)
            {
                if (albums[i].getAlbumId() == id)
                {
                    singers[albums[i].getSingerId()].removeAlbum(albums[i]);
                    albums.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void addMusic(Music music)
        {
            musics.Add(music);
        }
        public bool removeMusicById(int id)
        {
            for (int i = 0; i < musics.Count; i++)
            {
                if (musics[i].getMusicId() == id)
                {
                    albums[musics[i].getAlbumId()].removeMusic(musics[i]);
                    musics.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void addSinger(Singer singer)
        {
            singers.Add(singer);
        }
        public bool removeSingerById(int id)
        {
            for (int i = 0; i < singers.Count; i++)
            {
                if (singers[i].getSingerId() == id)
                {
                    singers.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public String getSingerNameById(int id)
        {
            return singers[id].getName();
        }
        public void storeAlbum(Album album,int singerId=0)
        {
            if (singerId > singers.Count - 1)
            {
                MessageBox.Show("不存在该歌手，重选或新注册1");
                return;
            }
            singers[singerId].addAlbum(album);
            albums.Add(album);
            String albumInfo = album.getAlbumId() + "|" + album.getAlbumName() + "|" + album.getAlbumDescription() + "|" + album.getImagePath()+"|"+album.getSingerName();
            sw4.WriteLine(singerId+ "|" + albumInfo);
            sw2.WriteLine(albumInfo);
            sw4.Flush();
            sw2.Flush();
        }
        public void storeSinger(Singer singer)
        {
            singers.Add(singer);
            sw1.WriteLine(singer.getSingerId()+"|"+singer.getName()+ "|" + singer.getDescription());
            sw1.Flush();
        }
        public List<Music> getMusicsByAlbumId(int id)
        {
            return albums[id].getMusics();
        }
    }
}
