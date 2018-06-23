using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Messaging
{
    public class MessageItem
    {
        public string Type { get; set; }
        public string Body { get; set; }

        public MessageItem(string type, string body)
        {
            Type = type;
            Body = body;
        }
    }
}
