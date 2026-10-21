using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace where_and_how
{
    internal class Singer
    {
        static int globalSingerId=0;
        private int singerId;
        private String name;
        private String description;
        private List<Album> albums;
        public Singer(String name= "", String description= "")
        {
            this.singerId = globalSingerId++;
            this.name = name;
            this.description = description;
            this.albums = new List<Album>();
        }
        public String getName() { return name; }
        public String getDescription() { return description; }
        public List<Album> getAlbums() { return albums; }
        public int getSingerId()
        {
            return singerId;
        }
        public void addAlbum(Album album) { albums.Add(album); }
        public void removeAlbum(Album album) 
        {
            if(albums.Contains(album))
                albums.Remove(album); 
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public void setDescription(String description) { this.description = description; }
        public void setSingerId(int id)
        {
            this.singerId = id;
            globalSingerId = id+1;
        }
    }
}
