using System.Data;
using System.IO;
using System.Text;

namespace DotNet.Utilities.NPOI
{
    public interface ITransferData
    {
        Encoding Encoding
        {
            get; set;
        }

        Stream GetStream(DataTable table);
        DataTable GetData(Stream stream);
    }
}
