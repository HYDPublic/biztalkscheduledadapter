using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Transactions;

using Microsoft.BizTalk.Adapter.Common;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;

namespace ScheduledTaskAdapter
{
    public class ReceiveTxnBatch : TxnBatch
    {
        private Batch innerBatch;
        private int innerBatchCount;
        private bool suspendMessage = false;

        public ReceiveTxnBatch(IBTTransportProxy transportProxy, ControlledTermination control, CommittableTransaction transaction, ManualResetEvent orderedEvent, bool suspendMessage)
            : base(transportProxy, control, transaction, orderedEvent, false)
        {
            this.suspendMessage = suspendMessage;
        }

        protected override void EndProcessFailures()
        {
            if (this.innerBatch != null)
            {
                if (this.innerBatchCount > 0)
                {                    
                    try
                    {
                        this.innerBatch.Done();
                        base.SetPendingWork();
                    }
                    catch (COMException exception)
                    {                        
                        base.SetAbort();
                    }
                }
                else
                {                    
                    base.SetAbort();
                }
            }
        }

        protected override void StartProcessFailures()
        {
            if (!this.OverallSuccess)
            {
                this.innerBatch = new AbortOnFailureReceiveTxnBatch(base.TransportProxy, base.control, base.comTxn, base.transaction, base.orderedEvent, null, base.CommitConfirm);
                this.innerBatchCount = 0;
            }
        }

        protected override void SubmitFailure(IBaseMessage message, int hrStatus, object userData)
        {            
            Stream stream = ((StreamAndUserData)userData).Stream;
            if (this.innerBatch != null)
            {
                stream.Seek(0L, SeekOrigin.Begin);
                message.BodyPart.Data = stream;
                try
                {
                    if (suspendMessage)
                    {
                        this.innerBatch.MoveToSuspendQ(message);
                        this.innerBatchCount++;
                    }
                    else
                    {
                        this.innerBatch = null;
                        base.SetAbort();
                    }
                }
                catch (COMException exception)
                {                    
                    this.innerBatch = null;
                    base.SetAbort();
                }
            }
        }
    }
}