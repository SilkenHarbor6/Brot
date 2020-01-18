using DLL.Models;
using System.Collections.Generic;

namespace DLL.ResponseModels
{
    public class ResponseUserProfile
    {
        public userModel UserProfile { get; set; }
        public int cantSeguidores { get; set; }
        public int cantSeguidos { get; set; }
        public bool isFollowed { get; set; }
        public List<ResponsePublicacionFeed> publicacionesUser {get;set;}
        public List<ResponsePublicacionFeed> publicacionesGuardadas {get;set; }

    }
}
