using System.Collections.Generic;
using System.Threading.Tasks;
using Oqtane.Survey.Models;

namespace Oqtane.Survey.Services
{
    public interface ISurveyAnswersService
    {
        Task CreateSurveyAnswersAsync(Models.Survey Survey);
    }
}