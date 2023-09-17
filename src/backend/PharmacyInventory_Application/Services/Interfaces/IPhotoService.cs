using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IPhotoService
    {
        string AddPhoto(IFormFile file);
    }
}
