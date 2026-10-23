using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace where_and_how
{
    internal class Album
    {
        static int globalAlbumId = 0;
        private int albumId;
        private int SingerId;
        private String albumName;
        private String albumImagePath;
        private String albumDescription;
        private String singerName;
        List<Music> musics;
        public Album(String albumName="", String albumDescription = "", String albumImagePath = "", String singerName= "",int SingerId=0)
        {
            this.albumId = globalAlbumId++;
            this.albumName =albumName;
            this.albumDescription =albumDescription;
            this.musics = new List<Music>();
            this.singerName=singerName;
            this.SingerId = SingerId;
        }
        public void appendMusic(Music music)
        {
            musics.Add(music);
        }
        public void RemoveMusic(Music music)
        {
            if(musics.Contains(music))
                musics.Remove(music);
        }
        public String getAlbumName() { return albumName; }
        public String getImagePath() { return albumImagePath; }
        public String getAlbumDescription() { return albumDescription; }
        public String getSingerName()
        {
            return singerName;
        }
        public int getAlbumId() { return albumId; }
        public int getSingerId() { return SingerId; }
        public List<Music> getMusics() { return musics; }
        public void setAlbumName(String albumName) { this.albumName = albumName; }
        public void setImagePath(String path) { this.albumImagePath = path; }
        public void setAlbumDescription(String description) { this.albumDescription = description; }
        public void addMusic(Music music) {musics.Add(music);}
        public void setSingerName(String singerName) { this.singerName = singerName; }
        public Boolean removeMusic(Music music) { 
            if(!musics.Contains(music)) return false;
            musics.Remove(music);
            return true;
        }
        public void setAlbumId(int id)
        {
            this.albumId = id;    
            globalAlbumId = id+1;
        }
        public void setSingerID(int id)
        {
            this.SingerId = id;
        }
    }
}
