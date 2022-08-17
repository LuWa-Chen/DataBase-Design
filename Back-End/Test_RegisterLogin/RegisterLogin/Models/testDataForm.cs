using Microsoft.AspNetCore.Http;
namespace testDataForm.Models
{
    public class testDataFormRequest
    {
        public IFormCollection files{ get; set; }
    }
    public class testDataFormResponse
    {
        public int result { get; set; }
    }
}
