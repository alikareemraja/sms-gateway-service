using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmsGateway.DTOs;
using SmsGateway.Models;
using WebGrease.Css.Extensions;

namespace SmsGateway.Repositories
{
    public class BatchRepository
    {
        public Guid CreateBatch(IEnumerable<Guid> messages)
        {
            try
            {
                using (var dbContext = ApplicationDbContext.Create())
                {
                    var batch = new Batch();
                    dbContext.Batches.Add(batch);
                    var dbBatchMessages = new List<BatchMessage>();
                    messages.ForEach(x => 
                    dbBatchMessages.Add(new BatchMessage
                    {
                        MessageId = x,
                        BatchId = batch.ID,
                        Status = (int) Status.InProgress
                    })
                    );
                    dbContext.BatchMessages.AddRange(dbBatchMessages);
                    dbContext.SaveChanges();
                    return batch.ID;
                }
                
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public List<Guid> GetBatchMessages(Guid batchId)
        {
            try
            {
                using (var dbContext = ApplicationDbContext.Create())
                {
                    return dbContext.BatchMessages.Where(x => x.BatchId == batchId).Select(x => x.MessageId).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}