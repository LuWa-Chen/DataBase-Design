using System.Collections.Generic;
namespace Chat.Models
{
    public class getChatHistoryRequest
    {
        public string id_A { get; set; }
        public string id_B { get; set; }
    }
    public class getChatHistoryResponse
    {
        public int result { get; set; }
        public string message { get; set; }
        public List<ChatHistory> chat_history { get; set; } = new List<ChatHistory> { };
    }
    public class ChatHistory
    {
        public string date { get; set; }
        public ChatText text { get; set; }
        public bool mine { get; set; }
    }
    public class ChatText
    {
        public string text { get; set; }
    }
    public class postChatHistoryRequest
    {
        public string id_A { get; set; }
        public string id_B { get; set; }
        public string text { get; set; }
    }
    public class postChatHistoryResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
