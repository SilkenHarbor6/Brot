

namespace Brot.Services
{
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    public static class RestClient
    {
        //private static string url = "http://brotproject.somee.com/api/"; //Last one
        private static string url = "https://brotmainapi.azurewebsites.net/api/"; //New One

        //private static string url = "http://192.168.22.127:61092/api/";

        private static HttpClient cliente = new HttpClient();

        public static bool isConnectedToInterned()  //Lo ideal seria mandar esto desde la app antes de usar el REST
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }
        public static async Task<Response> GetAll<T>(String Controller)
        {
            if (isConnectedToInterned())
            {
                try
                {
                    var response = await cliente.GetAsync(url + Controller);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync()).Message
                        };
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);

                    return new Response
                    {
                        IsSuccess = true,
                        Result = list
                    };
                }
                catch (Exception e)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = $"Error al cargar los datos {e}"
                    };
                }
            }
            return new Response()
            {
                IsSuccess = false,
                Message = "No es posible conectarse a internet"
            };
            
        }
        public static async Task<Response> Post<T>(string controller, T item)
        {
            if (isConnectedToInterned())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await cliente.PostAsync($"{url}{controller}", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response()
                        {
                            Message = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync()).Message,
                            IsSuccess = false
                        };
                    }

                    return new Response()
                    {
                        IsSuccess = true,
                        Result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())
                    };
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        IsSuccess = false,
                        Message = "Error Ex " + ex.Message
                    };
                }
            }
            return new Response()
            {
                IsSuccess = false,
                Message = "No es posible conectarse a internet"
            };
            
        }
        public static async Task<bool> Delete<T>(String controller, int id)
        {
            if (isConnectedToInterned())
            {
                try
                {
                    HttpResponseMessage response = await cliente.DeleteAsync(url + controller + id.ToString());
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
            
        }
        public static async Task<bool> Put<T>(String controller, int id, T item)
        {
            if (isConnectedToInterned())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await cliente.PutAsync(url + controller + id, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}