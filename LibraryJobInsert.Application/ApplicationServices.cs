using LibraryJobInsert.Domain.Interfaces;
using LibraryJobInsert.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryJobInsert.Application
{
    public class ApplicationServices : IApplicationServices
    {
        private readonly ILogger<ApplicationServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _rep;
        private string url { get; set; }

        public ApplicationServices(ILogger<ApplicationServices> logger, IConfiguration configuration, ICustomerRepository rep)
        {
            _logger = logger;
            _configuration = configuration;
            _rep = rep;
            url = _configuration["ApiUrl"];
        }

        public async Task<IEnumerable<Message>> GET(string enpoint)
        {
            IEnumerable<Message> messages = null;
            try
            {
                using (var http = new HttpClient())
                {
                    url = url + enpoint;

                    var response = await http.GetAsync(url);

                    var result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode)
                    {
                        messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);
            }
            return messages;
        }

        public async Task POST(IEnumerable<Message> messages, string endpoint)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(messages), Encoding.UTF8, "application/json");

                    var response = await http.PostAsync(url + endpoint, content);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Message>> InsertCustomer(IEnumerable<Message> messages)
        {
            List<Message> executedMessages = new List<Message>();

            try
            {
                foreach (var message in messages)
                {
                    var customer = JsonConvert.DeserializeObject<Customer>(message.Content);

                    var wasInserted = await _rep.Create(customer);

                    if (wasInserted)
                    {
                        message.Executed = true;

                        executedMessages.Add(message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);
            }
            return executedMessages;
        }
    }
}
