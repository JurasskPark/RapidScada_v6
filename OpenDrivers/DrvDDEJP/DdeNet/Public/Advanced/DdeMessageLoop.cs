#region Copyright (c) 2005 by Brian Gideon (briangideon@yahoo.com)
/* Shared Source License for DdeNet
 *
 * This license governs use of the accompanying software ('Software'), and your use of the Software constitutes acceptance of this license.
 *
 * You may use the Software for any commercial or noncommercial purpose, including distributing derivative works.
 * 
 * In return, we simply require that you agree:
 *  1. Not to remove any copyright or other notices from the Software. 
 *  2. That if you distribute the Software in source code form you do so only under this license (i.e. you must include a complete copy of this
 *     license with your distribution), and if you distribute the Software solely in object form you only do so under a license that complies with
 *     this license.
 *  3. That the Software comes "as is", with no warranties.  None whatsoever.  This means no express, implied or statutory warranty, including
 *     without limitation, warranties of merchantability or fitness for a particular purpose or any warranty of title or non-infringement.  Also,
 *     you must pass this disclaimer on whenever you distribute the Software or derivative works.
 *  4. That no contributor to the Software will be liable for any of those types of damages known as indirect, special, consequential, or incidental
 *     related to the Software or this license, to the maximum extent the law permits, no matter what legal theory it’s based on.  Also, you must
 *     pass this limitation of liability on whenever you distribute the Software or derivative works.
 *  5. That if you sue anyone over patents that you think may apply to the Software for a person's use of the Software, your license to the Software
 *     ends automatically.
 *  6. That the patent rights, if any, granted in this license only apply to the Software, not to any derivative works you make.
 *  7. That the Software is subject to U.S. export jurisdiction at the time it is licensed to you, and it may be subject to additional export or
 *     import laws in other places.  You agree to comply with all such laws and regulations that may apply to the Software after delivery of the
 *     software to you.
 *  8. That if you are an agency of the U.S. Government, (i) Software provided pursuant to a solicitation issued on or after December 1, 1995, is
 *     provided with the commercial license rights set forth in this license, and (ii) Software provided pursuant to a solicitation issued prior to
 *     December 1, 1995, is provided with “Restricted Rights” as set forth in FAR, 48 C.F.R. 52.227-14 (June 1987) or DFAR, 48 C.F.R. 252.227-7013 
 *     (Oct 1988), as applicable.
 *  9. That your rights under this License end automatically if you breach it in any way.
 * 10. That all rights not expressly granted to you in this license are reserved.
 */
#endregion
namespace DdeNet.Advanced
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This is a synchronizing object that can run a message loop on any thread.
    /// </summary>
    /// <threadsafety static="true" instance="false" />
    public sealed class DdeMessageLoop : IDisposable, ISynchronizeInvoke
    {
        private const uint WM_APP = 0x8000;
        private const uint WM_INVOKE = WM_APP + 1;

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern bool PostThreadMessage(int idThread, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        private static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        private static extern bool TranslateMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage(ref MSG lpMsg);

        private readonly BlockingCollection<WorkItem> workItems;
        private readonly ManualResetEvent initialized;
        private readonly Thread thread;
        private int threadId;
        private bool disposed;

        public DdeMessageLoop()
        {
            workItems = new BlockingCollection<WorkItem>(new ConcurrentQueue<WorkItem>());
            initialized = new ManualResetEvent(false);
            thread = new Thread(RunLoop)
            {
                Name = "DdeMessageLoop",
                IsBackground = true
            };
            thread.SetApartmentState(ApartmentState.STA);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            EnsureStarted();
            PostThreadMessage(threadId, 0x0012, IntPtr.Zero, IntPtr.Zero);
            if (Thread.CurrentThread != thread)
            {
                thread.Join();
            }
            initialized.Dispose();
            workItems.Dispose();
        }

        IAsyncResult ISynchronizeInvoke.BeginInvoke(Delegate method, object[] args)
        {
            return Enqueue(method, args, synchronous: false);
        }

        object ISynchronizeInvoke.EndInvoke(IAsyncResult asyncResult)
        {
            if (asyncResult is not Task<object> task)
            {
                throw new ArgumentException("Invalid async result.", nameof(asyncResult));
            }

            return task.GetAwaiter().GetResult();
        }

        object ISynchronizeInvoke.Invoke(Delegate method, object[] args)
        {
            if (!((ISynchronizeInvoke)this).InvokeRequired)
            {
                return method.DynamicInvoke(args);
            }

            Task<object> task = Enqueue(method, args, synchronous: true);
            return task.GetAwaiter().GetResult();
        }

        bool ISynchronizeInvoke.InvokeRequired => Thread.VolatileRead(ref threadId) != GetCurrentThreadId();

        public void Run()
        {
            EnsureStarted();
            thread.Join();
        }

        private Task<object> Enqueue(Delegate method, object[] args, bool synchronous)
        {
            EnsureStarted();
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
            workItems.Add(new WorkItem(method, args, tcs));
            if (!PostThreadMessage(threadId, WM_INVOKE, IntPtr.Zero, IntPtr.Zero))
            {
                throw new InvalidOperationException("Unable to post invoke message.");
            }

            return (Task<object>)tcs.Task;
        }

        private void EnsureStarted()
        {
            if ((thread.ThreadState & ThreadState.Unstarted) != 0)
            {
                thread.Start();
            }

            initialized.WaitOne();
        }

        private void RunLoop()
        {
            threadId = GetCurrentThreadId();
            PeekMessage(out MSG _, IntPtr.Zero, 0, 0, 0);
            initialized.Set();

            while (GetMessage(out MSG msg, IntPtr.Zero, 0, 0) > 0)
            {
                if (msg.message == WM_INVOKE)
                {
                    while (workItems.TryTake(out WorkItem workItem))
                    {
                        Execute(workItem);
                    }

                    continue;
                }

                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        }

        private static void Execute(WorkItem workItem)
        {
            try
            {
                object result = workItem.Method.DynamicInvoke(workItem.Args);
                workItem.TaskSource.SetResult(result);
            }
            catch (Exception ex)
            {
                workItem.TaskSource.SetException(ex is TargetInvocationException tie && tie.InnerException != null
                    ? tie.InnerException
                    : ex);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        private sealed class WorkItem
        {
            public WorkItem(Delegate method, object[] args, TaskCompletionSource<object> taskSource)
            {
                Method = method;
                Args = args;
                TaskSource = taskSource;
            }

            public Delegate Method { get; }
            public object[] Args { get; }
            public TaskCompletionSource<object> TaskSource { get; }
        }
    }
}

