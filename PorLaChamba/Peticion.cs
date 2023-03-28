using RestSharp;

namespace PorLaChamba
{
    public class Peticion
    {
        public static RestRequest crearPeticion(string metodo, Method type)
        {
            try
            {
                RestRequest request = new RestRequest(metodo, type);
                return request;
            }
            catch (Exception EX)
            {
                return null;
            }
        }
    }
}
