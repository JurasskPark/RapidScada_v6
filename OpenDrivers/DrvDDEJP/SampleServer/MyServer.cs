namespace SampleServer
{
    using System;
    using System.Timers;
    using DdeNet.Server;

    public class MyServer : DdeServer
    {
        private readonly Timer _timer = new Timer();

        public MyServer(string service) : base(service)
        {
            _timer.Elapsed += OnTimerElapsed;
            _timer.Interval = 1000;
            _timer.SynchronizingObject = Context;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            Advise("*", "*");
        }

        public override void Register()
        {
            base.Register();
            _timer.Start();
            FileLog.Write($"Register service='{Service}'.");
        }

        public override void Unregister()
        {
            _timer.Stop();
            base.Unregister();
            FileLog.Write($"Unregister service='{Service}'.");
        }

        protected override bool OnBeforeConnect(string topic)
        {
            string msg = "OnBeforeConnect:".PadRight(16)
                + " Service='" + Service + "'"
                + " Topic='" + topic + "'";
            Console.WriteLine(msg);
            FileLog.Write(msg);
            return true;
        }

        protected override void OnAfterConnect(DdeConversation conversation)
        {
            string msg = "OnAfterConnect:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle;
            Console.WriteLine(msg);
            FileLog.Write(msg);
        }

        protected override void OnDisconnect(DdeConversation conversation)
        {
            string msg = "OnDisconnect:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle;
            Console.WriteLine(msg);
            FileLog.Write(msg);
        }

        protected override bool OnStartAdvise(DdeConversation conversation, string item, int format)
        {
            string msg = "OnStartAdvise:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle
                + " Item='" + item + "'"
                + " Format=" + format;
            Console.WriteLine(msg);
            FileLog.Write(msg);
            return format == 1;
        }

        protected override void OnStopAdvise(DdeConversation conversation, string item)
        {
            string msg = "OnStopAdvise:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle
                + " Item='" + item + "'";
            Console.WriteLine(msg);
            FileLog.Write(msg);
        }

        protected override ExecuteResult OnExecute(DdeConversation conversation, string command)
        {
            string msg = "OnExecute:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle
                + " Command='" + command + "'";
            Console.WriteLine(msg);
            FileLog.Write(msg);
            return ExecuteResult.Processed;
        }

        protected override PokeResult OnPoke(DdeConversation conversation, string item, byte[] data, int format)
        {
            string msg = "OnPoke:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle
                + " Item='" + item + "'"
                + " Data=" + data.Length
                + " Format=" + format;
            Console.WriteLine(msg);
            FileLog.Write(msg);
            return PokeResult.Processed;
        }

        protected override RequestResult OnRequest(DdeConversation conversation, string item, int format)
        {
            string msg = "OnRequest:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle
                + " Item='" + item + "'"
                + " Format=" + format;
            Console.WriteLine(msg);
            FileLog.Write(msg);

            if (format == 1)
            {
                return new RequestResult(System.Text.Encoding.ASCII.GetBytes("Time=" + DateTime.Now + "\0"));
            }

            return DdeServer.RequestResult.NotProcessed;
        }

        protected override byte[] OnAdvise(string topic, string item, int format)
        {
            string msg = "OnAdvise:".PadRight(16)
                + " Service='" + Service + "'"
                + " Topic='" + topic + "'"
                + " Item='" + item + "'"
                + " Format=" + format;
            Console.WriteLine(msg);
            FileLog.Write(msg);

            if (format == 1)
            {
                return System.Text.Encoding.ASCII.GetBytes("Time=" + DateTime.Now + "\0");
            }

            return null;
        }
    }
}
