#region Copyright (c) 2002-2008, Bas Geertsema, Xih Solutions (http://www.xihsolutions.net), Thiago.Sayao, Pang Wu, Ethem Evlice
/*
Copyright (c) 2002-2008, Bas Geertsema, Xih Solutions
(http://www.xihsolutions.net), Thiago.Sayao, Pang Wu, Ethem Evlice.
All rights reserved. http://code.google.com/p/msnp-sharp/

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice,
  this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.
* Neither the names of Bas Geertsema or Xih Solutions nor the names of its
  contributors may be used to endorse or promote products derived from this
  software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 'AS IS'
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
THE POSSIBILITY OF SUCH DAMAGE. 
*/
#endregion

using System;
using System.Collections;
using System.Diagnostics;

namespace MSNPSharp
{
    using MSNPSharp.Core;
    using MSNPSharp.DataTransfer;

    public class NSMessageProcessor : SocketMessageProcessor
    {
        int transactionID = 1;

        public event EventHandler<ExceptionEventArgs> HandlerException;

        private NSMessageProcessor()
        {
            MessagePool = new NSMessagePool();
        }

        public int TransactionID
        {
            get
            {
                return transactionID;
            }
            set
            {
                transactionID = value;
            }
        }

        protected int IncreaseTransactionID()
        {
            transactionID++;
            return transactionID;
        }

        protected override void OnMessageReceived(byte[] data)
        {
            base.OnMessageReceived(data);

            NSMessage message = new NSMessage();

            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Parsing incoming NS command...", GetType().Name);

            // parse the incoming data
            message.ParseBytes(data);

            // trace and dispatch the message
            Trace.WriteLineIf(Settings.TraceSwitch.TraceInfo, "Dispatching incoming NS command: " + System.Text.Encoding.UTF8.GetString(data)/*message.ToDebugString()*/, GetType().Name);
            DispatchMessage(message);
        }

        public override void SendMessage(NetworkMessage message)
        {
            MSNMessage NSMessage = null;

            NSMessage = (MSNMessage)message;
            NSMessage.TransactionID = IncreaseTransactionID();
            message.PrepareMessage();

            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Outgoing message:\r\n" + message.ToDebugString(), GetType().Name);

            // convert to bytes and send it over the socket
            SendSocketData(message.GetBytes());
        }

        public override void Disconnect()
        {
            SendMessage(new NSMessage("OUT", new string[] { }));
            System.Threading.Thread.CurrentThread.Join(100);
            base.Disconnect();
        }

        protected virtual void DispatchMessage(NetworkMessage message)
        {
            // copy the messageHandlers array because the collection can be 
            // modified during the message handling. (Handlers are registered/unregistered)
            IMessageHandler[] handlers = MessageHandlers.ToArray();

            // now give the handlers the opportunity to handle the message
            foreach (IMessageHandler handler in handlers)
            {
                try
                {
                    handler.HandleMessage(this, message);
                }
                catch (Exception e)
                {
                    if (HandlerException != null)
                        HandlerException(this, new ExceptionEventArgs(new MSNPSharpException("An exception occured while handling a nameserver message. See inner exception for more details.", e)));
                }
            }
        }
    }
};