using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Hangfire;
using SmsGateway.DTOs;
using SmsGateway.DTOs.Response;
using SmsGateway.Repositories;

namespace SmsGateway.Service
{
    public class MessageService
    {
        #region Private Variables
        private readonly MessageRepository _messageRepository = new MessageRepository();
        private readonly BatchRepository _batchRepository = new BatchRepository();
        #endregion Private Variables

        public List<Message> GetAllMessages()
        {
            return _messageRepository.GetAll().Select(x => new Message
            {
                MessageId = x.ID,
                Content = x.Content,
                Date = x.Date,
                Status = x.Status,
                To = x.To
            }).ToList();
        }

        public bool UpdateStatusOnTimeOut(Guid batch)
        {
            var messages = _batchRepository.GetBatchMessages(batch);
            return _messageRepository.UpdateMessageOnTimeOut(messages);
        }

        public bool UpdateMessageStatus(Guid messageId)
        {
            return _messageRepository.ChangeMessageStatus(messageId);
        }

        public BatchResponse GetBatchMessages(int batchSize)
        {
            var messages = _messageRepository.GetMessagesToSend(batchSize);
            var batch = _batchRepository.CreateBatch(messages.Select(x => x.ID));
            var batchResponse = new BatchResponse
            {
                BatchId = batch,
                Messages = messages.Select(x => new Message
                {
                    Status = x.Status,
                    To = x.To,
                    Date = x.Date,
                    Content = x.Content,
                    MessageId = x.ID
                })
            };
            //Run Job after 2 minutes to set in progress messages to 'to send'
            BackgroundJob.Schedule(() => UpdateStatusOnTimeOut(batch), TimeSpan.FromMinutes(2));
            return batchResponse;
        }

        public bool AddMessage(Message message)
        {
            return _messageRepository.AddMessage(new Models.Message
            {
                Content = message.Content,
                Date = DateTime.Now,
                To  = message.To,
                Status = (int) Status.ToSend
            });
        }
    }
}