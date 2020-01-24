

namespace Brot.Services
{
    using Microsoft.AppCenter.Crashes;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    public static class RestClient
    {
        //private static string url = "http://brotproject.somee.com/api/"; //Last one
        //private static string url = "http://brotnewapi.azurewebsites.net/api/"; //New One

        private static string url = "http://brotmainapi.azurewebsites.net/api/"; //New One
       // private static string url = "http://192.168.42.106:61092/api/";

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
                    Crashes.TrackError(e, new Dictionary<string, string>()
                    {
                        {"RestClient", "GetAll" }
                    });
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
                    Crashes.TrackError(ex, new Dictionary<string, string>()
                    {
                        {"RestClient", "Post" }
                    });
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
                    HttpResponseMessage response = await cliente.DeleteAsync(url + controller + "/" + id.ToString());
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Crashes.TrackError(e, new Dictionary<string, string>()
                    {
                        {"RestClient", "Delete" }
                    });
                    return false;
                }
            }
            return false;

        }
        public static async Task<bool> Put<T>(String controller, object id, T item)
        {
            if (isConnectedToInterned())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await cliente.PutAsync(url + controller + "/" + id.ToString(), content);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Crashes.TrackError(e, new Dictionary<string, string>()
                    {
                        {"RestClient", "Put" }
                    });
                    return false;
                }
            }
            return false;
        }
        public static async Task<Response> Post4Reg<T>(string controller, T item)
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
                        Result = await response.Content.ReadAsStringAsync()
                    };
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex, new Dictionary<string, string>()
                    {
                        {"RestClient", "Post4Reg" }
                    });
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
        public static async Task<Response> GetAllS<T>(String Controller)
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

                    return new Response
                    {
                        IsSuccess = true,
                        Result = result
                    };
                }
                catch (Exception e)
                {
                    Crashes.TrackError(e, new Dictionary<string, string>()
                    {
                        {"RestClient", "GetAll_S" }
                    });
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
    }
}