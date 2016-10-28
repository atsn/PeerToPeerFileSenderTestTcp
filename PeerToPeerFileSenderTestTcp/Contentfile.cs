namespace SoapService
{
    public class ContentFile
    {
      
        public string FileName { get; private set; }
     
        public string IpAddress { get; private set; }
       
        public int Port { get; private set; }

        public ContentFile(string fileName, string ipAddress, int port)
        {
            FileName = fileName;
            IpAddress = ipAddress;
            Port = port;
        }
    }
}