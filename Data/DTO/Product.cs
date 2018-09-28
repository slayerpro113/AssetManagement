using System.IO;
using System.Web;

namespace Data.Entities
{
    public partial class Product
    {
        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get
            {
                if (_imageBytes == null)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Image/Categories/" + Image);
                    _imageBytes = File.ReadAllBytes(path);
                }

                return _imageBytes;
            }
            set
            {
                _imageBytes = value;
            }
        }
    }
}