using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace RegexFileNameMatch
{
    public class RegexFileNameMatch
    {
        private readonly ILogger<RegexFileNameMatch> _logger;

        public RegexFileNameMatch(ILogger<RegexFileNameMatch> log)
        {
            _logger = log;
        }

        [FunctionName("RegexFileNameMatch")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("RegexFileNameMatch C# HTTP trigger function processed a request.");

        

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            //initialize an empty array object

            string[] files = new string[0];




            foreach (var file in data.files)
            {
                // If the type of file 'Folder', ignore else if the type of file is 'File' then check the file name is matching in the regex pattern data.pattern and return all matched files in the files array

                if (file.type == "Folder")
                {
                    continue;
                }
                else if (file.type == "File")
                {
                    
                    if (Regex.IsMatch(file.name.ToString(), data.pattern.ToString()))
                    {
                        // Add the matched file name to the files array
                        files = files.Concat(new string[] { file.name }).ToArray();

                    }
                }
                
                
            }

            // If the files array is empty, return the message 'No files found' else return the matched files array
            if (files.Length == 0)
            {
                return new BadRequestObjectResult("No files found");
            }
            else
            {
                return new OkObjectResult(files);
            }

        }
    }
}

