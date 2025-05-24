using System.Collections.Generic;
using System.Linq;

namespace Collabry
{
    public interface IMessageService<TMessage>
    {
        void AddMessage(TMessage message);
        List<TMessage> GetAllMessages();
        void DeleteMessageById(int messageId);
        void UpdateMessageById(int messageId, TMessage newMessage);
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

        public void DeleteMessageById(int messageId)
        {
            using (var db = new AppDbContext())
            {
                var msg = db.Messages.Find(messageId);
                if (msg != null)
                {
                    db.Messages.Remove(msg);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateMessageById(int messageId, Message newMessage)
        {
            using (var db = new AppDbContext())
            {
                var msg = db.Messages.Find(messageId);
                if (msg != null)
                {
                    msg.Text = newMessage.Text;
                    msg.IsEdited = true;
                    db.SaveChanges();
                }
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
                    .OrderBy(m => m.SendTime)
                    .ToList();
            }
        }

        public void DeleteMessageById(int messageId)
        {
            using (var db = new AppDbContext())
            {
                var msg = db.Messages_S.Find(messageId);
                if (msg != null)
                {
                    db.Messages_S.Remove(msg);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateMessageById(int messageId, Message_S newMessage)
        {
            using (var db = new AppDbContext())
            {
                var msg = db.Messages_S.Find(messageId);
                if (msg != null)
                {
                    msg.Text = newMessage.Text;
                    msg.IsEdited = true;
                    db.SaveChanges();
                }
            }
        }
    }
}
