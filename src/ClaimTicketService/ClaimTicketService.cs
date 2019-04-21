using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;

namespace ClaimTicketService
{

  public static class ClaimTicketServiceFunction 
  {
public static async Task<List<string>> Run(DurableOrchestrationContext context)
{
    var outputs = new List<string>();
     
    // validate queue 

    // use cognitive services to access damage 

    // crop and store image 

    // assiged and update ticket status 
    
    // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
    return outputs;
}
  }
}