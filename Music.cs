using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace where_and_how
{
    internal class Music
    {
        static int globalMId=0;//主键
        private int mId;
        private int AlbumId;
        private String musicName;
        private String musicAuthor;
        private String mp3Path;
        private String musicDescription;
        private String albumName;
        public Music(String musicName="", String musicAuthor = "", String musicDescription = "", String albumName="", string mp3Path = "",int AlbumId=0)
        {
            this.mId = globalMId++;
            this.musicName = musicName;
            this.musicAuthor = musicAuthor;
            this.musicDescription = musicDescription;
            this.albumName = albumName;
            this.mp3Path = mp3Path;
            this.AlbumId = AlbumId;
        }
        public String getMusicName() { return musicName; }
        public String getMusicAuthor() { return musicAuthor; }
        public String getMusicDescription() { return musicDescription; }
        public String getAlbumName() { return albumName; }
        public String getMp3PAth() { return mp3Path; }
        public int getMusicId()
        {
            return mId;
        }
        public int getAlbumId()
        {
            return this.AlbumId;
        }
        public void setMusicName(String musicName) { this.musicName = musicName; }
        public void setMusicAuthor(String musicAuthor) { this.musicAuthor = musicAuthor; }
        public void setMusicDescription(String musicDescription) { this.musicDescription = musicDescription; }
        public void setAlbumName(String albumName) { this.albumName = albumName; }
        public void setMp3Path(String path) { mp3Path = path; }
        public void setMID(int id)
        {
              this.mId = id;       
              globalMId = id+1;  
        }
        public void setAlbumID(int id)
        {
            this.AlbumId = id;
        }
    }

}
