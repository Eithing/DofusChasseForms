using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DofusChasseForms.ApiHandler.Models;
using Refit;

namespace DofusChasseForms.ApiHandler.Services
{
    [Headers("Content-Type: application/json")]
    public interface IDofusMap
    {
        [Get("/huntTool/getData.php?x={x}&y={y}&direction={direction}&world=0&language=fr")]
        Task<IndicesRef> GetIndicesReferences(string x, string y, string direction);
    }
}
