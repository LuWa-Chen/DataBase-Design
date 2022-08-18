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
        public string message { get; set; }
    }
    public class testDataPath
    {
        public string MainPicSrc { get; set; }
        public string AssPicSrc { get; set; }
        public string BacPicSrc { get; set; }
        public string ConPicSrc { get; set; }
        public string LogoPicSrc { get; set; }
        public string ProgramSrc { get; set; }
        public double size { get; set; }
    }
}
