﻿namespace Brot.Models.ResponseApi
{
    using Models;
    using System.Collections.Generic;

    public class ResponseUserProfile
    {
        public userModel UserProfile { get; set; }
        public int cantSeguidores { get; set; }
        public int cantSeguidos { get; set; }
        public bool isFollowed { get; set; }
        public List<ResponsePublicacionFeed> publicacionesUser {get;set;}
        public List<ResponsePublicacionGuardada> publicacionesGuardadas {get;set; }

    }
}
