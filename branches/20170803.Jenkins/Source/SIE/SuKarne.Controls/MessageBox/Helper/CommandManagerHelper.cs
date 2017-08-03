using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SuKarne.Controls.MessageBox.Helper
{
    public static class CommandManagerHelper
    {
        public static Action<List<WeakReference>> CallWeakReferenceHandlers = x =>
            {
                if (x != null)
                {
                    var callers = new EventHandler[x.Count];
                    int count = 0;

                    for (int i = x.Count - 1; i >= 0; i--)
                    {
                        WeakReference reference = x[i];
                        var handler = reference.Target as EventHandler;
                        if (handler == null)
                        {
                            // Clean up old handlers that have been collected
                            x.RemoveAt(i);
                        }
                        else
                        {
                            callers[count] = handler;
                            count++;
                        }
                    }

                    // Call the handlers that we snapshotted
                    for (int i = 0; i < count; i++)
                    {
                        EventHandler handler = callers[i];
                        handler(null, EventArgs.Empty);
                    }
                }
            };

        public static Action<List<WeakReference>> AddHandlersToRequerySuggested = x =>
            {
                if (x != null)
                {
                    x.ForEach(y =>
                        {
                            var handler = y.Target as EventHandler;
                            if (handler != null) CommandManager.RequerySuggested += handler;
                        });
                }
            };

        public static Action<List<WeakReference>> RemoveHandlersFromRequerySuggested = x =>
            {
                if (x != null)
                {
                    x.ForEach(y =>
                        {
                            var handler = y.Target as EventHandler;
                            if (handler != null) CommandManager.RequerySuggested -= handler;
                        });
                }
            };

        public static Action<List<WeakReference>, EventHandler> RemoveWeakReferenceHandler = (x, y) =>
        {
            if (x != null)
            {
                for (int i = x.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = x[i];
                    var existingHandler = reference.Target as EventHandler;
                    if ((existingHandler == null) || (existingHandler == y))
                    {
                        x.RemoveAt(i);
                    }
                }
            }
        };

        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
        {
            AddWeakReferenceHandler(ref handlers, handler, -1);
        }

        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler,
                                                     int defaultListSize)
        {
            if (handlers == null)
            {
                handlers = (defaultListSize > 0 ? new List<WeakReference>(defaultListSize) : new List<WeakReference>());
            }

            handlers.Add(new WeakReference(handler));
        }
    }
}