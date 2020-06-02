using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SmsGateway.DTOs;
using SmsGateway.Models;
using WebGrease.Css.Extensions;

namespace SmsGateway.Repositories
{
    public class MessageRepository
    {
        public List<Message> GetAll()
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                return dbContext.Messages.ToList();
            }
        }

        public List<Message> GetMessagesToSend(int batchSize)
        {
            List<Message> messages;
            using (var dbContext = ApplicationDbContext.Create())
            {
                messages =  dbContext.Messages.Where(x => x.Status == (int) Status.ToSend).Take(batchSize).ToList();
                messages.ForEach(x => x.Status = (int)Status.InProgress);
                dbContext.SaveChanges();
                
            }
            return messages;

        }

        /// <summary>
        /// Add new message to database
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddMessage(Message message)
        {
            try
            {
                using (var dbContext = ApplicationDbContext.Create())
                {
                    dbContext.Messages.Add(message);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Update message in DB
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateMessage(Message message)
        {
            try
            {
                using (var dbContext = ApplicationDbContext.Create())
                {
                    dbContext.Messages.Attach(message);
                    dbContext.Entry(message).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMessageOnTimeOut(List<Guid> messages)
        {
            try
            {
                using (var dbContext = ApplicationDbContext.Create())
                {
                    var updatedMessages = dbContext.Messages.Where(x => messages.Any(e => e == x.ID));
                    
                    foreach (var message in updatedMessages)
                    {
                        if (message.Status != (int) Status.Sent)
                        {
                            message.Status = (int) Status.ToSend;
                        }
                        
                    }
                    
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Update status of message in DB
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool ChangeMessageStatus(Guid messageId)
        {
            try
            {
                using (var dbContext = ApplicationDbContext.Create())
                {
                    var message = dbContext.Messages.Single(x => x.ID == messageId);
                    message.Status = (int) Status.Sent;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}