
namespace sl.web.ui
{
    public class JsonTip
    {
        public string state { get; set; }
        public string message { get; set; }

        public JsonTip(string state, string message)
        {
            this.state = state;
            this.message = message;
        }
    }
}
