

namespace Brot.Services
{
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Models.ResponseApi;
    using System.Net.Http;
    using Xamarin.Essentials;
    public static class RestAPI
    {
        //private static string urlBase = "http://192.168.22.127:61092/api/";
        //private static string urlBase = "cibomarket.somee.com/api/"; 
        //private static string urlBase = "http://brotproject.somee.com/api/";  //Last one
        private static string urlBase = "http://brotmainapi.azurewebsites.net/api/"; //New One
        public static bool isConnectedToInterned()  //Lo ideal seria mandar esto desde la app antes de usar el REST
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }




        #region GETS


        //[Route("api/comentarios/{idComentario}")]
        public static async Task<ResponseComentarios> comentariosGetbyid(int idComentario)
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.comentariost}/{idComentario}";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseComentarios>(json);
                    }
                }
            }
            return null;
        }

        public static async Task<ResponseLikes> GetLikesbyIDComentario(int id)
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.like_comentariot}/{id}";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseLikes>(json);
                    }
                }
            }

            return null;
        }


        //[Route("api/like_post/{idPost}")]
        public static async Task<ResponseLikes> GetLikesbyIDPost(int id)
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.like_postt}/{id}";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseLikes>(json);
                    }
                }
            }

            return null;
        }


        //[Route("api/publicacion_guardada/{idUser}")]
        public static async Task<List<ResponsePublicacionGuardada>> GetPublicacionesGuardadas(int idUser)//id del usuario
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.publicacion_guardadat}/{idUser}";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResponsePublicacionGuardada>>(json);
                    }
                }
            }

            return null;
        }



        #region PublicacionesController





        //[Route("publicaciones/all/{idUser}")]
        public static async Task<List<ResponsePublicacionFeed>> GetPublicacionesALL(int idUser)//id del usuario
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"{urlBase}{TableName.publicacionest}/all/{idUser}");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResponsePublicacionFeed>>(json);
                    }
                }
            }

            return null;
        }

        //[Route("publicaciones/all/{idUser}")]
        public static async Task<List<ResponsePublicacionFeed>> GetPublicacionesFeed(int idUser)//id del usuario
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.publicacionest}/feed/{idUser}";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResponsePublicacionFeed>>(json);
                    }
                }
            }

            return null;
        }

        //[Route("publicaciones/{idPost}/user/{idUser}")]
        public static async Task<ResponsePublicacion> Getpublicacion(int idPost, int idUser)//id del usuario
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.publicacionest}/{idPost}/user/{idUser}";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponsePublicacion>(json);
                    }
                }
            }

            return null;
        }

        #endregion





        #region UsersController





        //[Route("UsernameDisponible/{username}")]
        public static async Task<bool> validandoUsername(string username)
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/UsernameDisponible /{ username}";
                    var response = await client.GetAsync(urlComplete);
                    return response.IsSuccessStatusCode;
                }
            }
            return false;
        }


        public static async Task<userModel> login(string pass, string userOrEmail)
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/login";
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(new userModel() { username = userOrEmail, pass = pass });
                    var content = new StringContent(json, Encoding.UTF8, TableName.appJSonString);
                    var response = await client.PostAsync(urlComplete, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<userModel>(jsonResult);
                    }
                }
            }

            return null;
        }

        public static async Task<List<ResponseUsuariosFiltro>> getBrotTen()
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/brotten";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResponseUsuariosFiltro>>(json);
                    }
                }
            }
            return null;
        }


        ////Perfil de otro  cliente o de vendedor
        //[Route("{idUserProfile}/profile/{idVisitante}")]
        public static async Task<ResponseUserProfile> GetOtherUserrofile(int idUserProfile, int idVisitante)
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/{idUserProfile}/profile/{idVisitante}";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseUserProfile>(json);
                    }
                }
            }
            return null;
        }


        //[Route("{idUser}/profile")]
        public static async Task<ResponseUserProfile> userprofile(int idUser)//Cuando un usuario ve su propio perfil
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/{idUser}/profile";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseUserProfile>(json);
                    }
                }
            }
            return null;
        }

        //[Route("vendors")]
        public static async Task<List<userModel>> getVendedores()//idUser es el seguido
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/vendors";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<userModel>>(json);
                    }
                }
            }
            return null;
        }

        //[Route("{idUser}/seguidores")]
        public static async Task<List<userModel>> getSeguidores(int idUser)//idUser es el seguido
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/{idUser}/seguidores";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<userModel>>(json);
                    }
                }
            }
            return null;
        }



        // [Route("{idUser}/seguidos")]
        public static async Task<List<userModel>> getSeguidos(int idUser)//idUser es el seguidor
        {
            if (isConnectedToInterned())
            {
                using (HttpClient client = new HttpClient())
                {
                    string urlComplete = $"{urlBase}{TableName.userst}/{idUser}/seguidos";
                    var response = await client.GetAsync(urlComplete);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<userModel>>(json);
                    }
                }
            }
            return null;
        }
        #endregion

        #endregion




        #region PUT - DELETE - POST

        public static async Task<bool> Put<T>(int id, T item, string tabla)
        {
            if (isConnectedToInterned())
            {
                string url = $"{urlBase}{tabla}/{id}";
                using (HttpClient client = new HttpClient())
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync(url, content);
                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public static async Task<bool> Delete(int id, string tabla)
        {
            if (isConnectedToInterned())
            {
                string url = $"{urlBase}{tabla}/{id}";
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static async Task<bool> Post<T>(T item, string tabla)
        {
            if (isConnectedToInterned())
            {
                string url = $"{urlBase}{tabla}";
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

    }

}
