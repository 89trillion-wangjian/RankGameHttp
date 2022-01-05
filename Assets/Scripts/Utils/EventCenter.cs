using System;
using System.Collections.Generic;

namespace Utils
{
    public static class EventCenter
    {
        public delegate void EventHandle();

        public delegate void EventHandle<in T>(T value);

        public delegate void EventHandle<in T1, in T2>(T1 value1, T2 value2);

        public delegate void EventHandle<in T1, in T2, in T3>(T1 value1, T2 value2, T3 value3);

        private static Dictionary<string, Delegate> eventHandles;

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        public static void PostEvent(string eventName)
        {
            if (eventHandles == null)
            {
                return;
            }
            Delegate d;
            if (eventHandles.TryGetValue(eventName, out d))
            {
                if (d == null)
                {
                    return;
                }

                EventHandle call = d as EventHandle;
                if (call != null)
                {
                    call();
                }
                else if (d is EventHandle<object>)
                {
                    EventHandle<object> call2 = d as EventHandle<object>;
                    call2(null);
                }
                else
                {
                    throw new Exception($"事件{eventName}包含着不同类型的委托");
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void PostEvent<T>(string eventName, T value)
        {
            if (eventHandles == null)
            {
                return;
            }
            Delegate d;
            if (eventHandles.TryGetValue(eventName, out d))
            {
                if (d == null)
                {
                    return;
                }

                EventHandle<T> call = d as EventHandle<T>;
                if (call != null)
                {
                    call(value);
                }
                else
                {
                    throw new Exception($"事件{eventName}包含着不同类型的委托{d.GetType()}");
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public static void PostEvent<T1, T2>(string eventName, T1 value1, T2 value2)
        {
            if (eventHandles == null)
            {
                return;
            }

            Delegate d;
            if (eventHandles.TryGetValue(eventName, out d))
            {
                if (d == null)
                {
                    return;
                }

                EventHandle<T1, T2> call = d as EventHandle<T1, T2>;
                if (call != null)
                {
                    call(value1, value2);
                }
                else
                {
                    throw new Exception($"事件{eventName}包含着不同类型的委托{d.GetType()}");
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public static void PostEvent<T1, T2, T3>(string eventName, T1 value1, T2 value2, T3 value3)
        {
            if (eventHandles == null)
            {
                return;
            }
            Delegate d;
            if (eventHandles.TryGetValue(eventName, out d))
            {
                if (d == null)
                {
                    return;
                }

                EventHandle<T1, T2, T3> call = d as EventHandle<T1, T2, T3>;
                if (call != null)
                {
                    call(value1, value2, value3);
                }
                else
                {
                    throw new Exception($"事件{eventName}包含着不同类型的委托{d.GetType()}");
                }
            }
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handle">回调</param>
        public static void AddListener(string eventName, EventHandle handle)
        {
            AddListened(eventName, handle);
            eventHandles[eventName] = (EventHandle) eventHandles[eventName] + handle;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handle">回调</param>
        public static void AddListener(string eventName, EventHandle<object> handle)
        {
            AddListened(eventName, handle);
            eventHandles[eventName] = (EventHandle<object>) eventHandles[eventName] + handle;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddListener<T>(string eventName, EventHandle<T> handle)
        {
            AddListened(eventName, handle);
            eventHandles[eventName] = (EventHandle<T>) eventHandles[eventName] + handle;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public static void AddListener<T1, T2>(string eventName, EventHandle<T1, T2> handle)
        {
            AddListened(eventName, handle);
            eventHandles[eventName] = (EventHandle<T1, T2>) eventHandles[eventName] + handle;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public static void AddListener<T1, T2, T3>(string eventName, EventHandle<T1, T2, T3> handle)
        {
            AddListened(eventName, handle);
            eventHandles[eventName] = (EventHandle<T1, T2, T3>) eventHandles[eventName] + handle;
        }


        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handle">回调</param>
        public static void RemoveListener(string eventName, EventHandle handle)
        {
            if (eventHandles == null)
            {
                return;
            }

            if (!eventHandles.ContainsKey(eventName))
            {
                return;
            }

            RemoveListened(eventName, handle);
            eventHandles[eventName] = (EventHandle) eventHandles[eventName] - handle;
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handle">回调</param>
        public static void RemoveListener(string eventName, EventHandle<object> handle)
        {
            RemoveListener<object>(eventName, handle);
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handle"></param>
        public static void RemoveListener<T>(string eventName, EventHandle<T> handle)
        {
            if (eventHandles == null)
            {
                return;
            }

            if (!eventHandles.ContainsKey(eventName))
            {
                return;
            }

            RemoveListened(eventName, handle);
            eventHandles[eventName] = (EventHandle<T>) eventHandles[eventName] - handle;
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handle"></param>
        public static void RemoveListener<T1, T2>(string eventName, EventHandle<T1, T2> handle)
        {
            if (eventHandles == null)
            {
                return;
            }

            if (!eventHandles.ContainsKey(eventName))
            {
                return;
            }

            RemoveListened(eventName, handle);
            eventHandles[eventName] = (EventHandle<T1, T2>) eventHandles[eventName] - handle;
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handle"></param>
        public static void RemoveListener<T1, T2, T3>(string eventName, EventHandle<T1, T2, T3> handle)
        {
            if (eventHandles == null)
            {
                return;
            }

            if (!eventHandles.ContainsKey(eventName))
            {
                return;
            }

            RemoveListened(eventName, handle);
            eventHandles[eventName] = (EventHandle<T1, T2, T3>) eventHandles[eventName] - handle;
        }

        static void AddListened(string eventName, Delegate callback)
        {
            if (eventHandles == null)
            {
                eventHandles = new Dictionary<string, Delegate>();
            }
            if (!eventHandles.ContainsKey(eventName))
            {
                eventHandles.Add(eventName, null);
            }

            Delegate d = eventHandles[eventName];
            if (d != null && d.GetType() != callback.GetType())
            {
                throw new Exception($"尝试添加两种不同类型的委托,委托1为{d.GetType()}，委托2为{callback.GetType()}");
            }
        }

        static void RemoveListened(string eventName, Delegate callback)
        {
            if (eventHandles.ContainsKey(eventName))
            {
                Delegate d = eventHandles[eventName];
                if (d != null && d.GetType() != callback.GetType())
                {
                    throw new Exception($"尝试移除不同类型的事件，事件名{eventName},已存储的委托类型{d.GetType()},当前事件委托{callback.GetType()}");
                }
            }
            else
            {
                throw new Exception($"没有事件名{eventName}");
            }
        }
    }
}