using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using SmsGateway.DTOs.Response;
using SmsGateway.HttpHeader;
using SmsGateway.Service;

namespace SmsGateway.Controllers.Api
{
    [ApiAuthentication]
    public class SmsController : ApiController
    {
        private readonly MessageService _service = new MessageService();

        /// <summary>
        /// Returns all messages in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthentication]
        [Route("api/Message/All")]
        [ResponseType(typeof(List<Message>))]
        public IHttpActionResult GetAllMessages()
        {
            return Ok(_service.GetAllMessages());
        }

        /// <summary>
        /// Add new SMS from the UI
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Message/Add")]
        [ResponseType(typeof(List<bool>))]
        public IHttpActionResult AddMessage(Message message)
        {
            return Ok(_service.AddMessage(message));
        }

        /// <summary>
        /// Create a batch of messages to be sent
        /// </summary>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Message/ToSend/{batchSize}")]
        [ResponseType(typeof(BatchResponse))]
        public IHttpActionResult GetMessage(int batchSize)
        {
            return Ok(_service.GetBatchMessages(batchSize));
        }

        /// <summary>
        /// Set status of a message as Sent
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Message/Sent/{messageId}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult MessageSent(Guid messageId)
        {
            return Ok(_service.UpdateMessageStatus(messageId));
        }
    }
}