using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public interface IMessageService<TMessage>
    {
        void AddMessage(TMessage message);
        List<TMessage> GetAllMessages();
    }
    
    public class MessageService : IMessageService<Message>
    {
        public void AddMessage(Message message)
        {
            using (var db = new AppDbContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        public List<Message> GetAllMessages()
        {
            using (var db = new AppDbContext())
            {
                return db.Messages
                    .Include("Sender")
                    .OrderBy(m => m.SendTime)
                    .ToList();
            }
        }
    }

    public class Message_SService : IMessageService<Message_S>
    {
        public void AddMessage(Message_S message_S)
        {
            using (var db = new AppDbContext())
            {
                db.Messages_S.Add(message_S);
                db.SaveChanges();
            }
        }

        public List<Message_S> GetAllMessages()
        {
            using (var db = new AppDbContext())
            {
                return db.Messages_S
                    .Include("Sender")
                    .OrderBy(m => m.SendTime)
                    .ToList();
            }
        }
    }
}
