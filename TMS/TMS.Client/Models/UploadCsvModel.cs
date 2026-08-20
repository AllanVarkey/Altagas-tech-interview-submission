using Microsoft.AspNetCore.Components.Forms;

namespace TMS.Client.Models
{
    public class UploadCsvModel
    {
        public IBrowserFile? File { get; set; }
        public bool HasFile { get; set; }
    }
}
