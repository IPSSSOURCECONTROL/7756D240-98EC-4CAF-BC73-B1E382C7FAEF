﻿using System;
using System.Collections.Generic;

namespace Kbit.ControlCentre.ToastrWrapper
{

    public enum ToastType
    {
        Error,
        Info,
        Success,
        Warning
    }
    [Serializable]
    public class ToastMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ToastType ToastType { get; set; }
        public bool IsSticky { get; set; }
    }
    [Serializable]
    public class Toastr
    {
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public bool ProgressBar { get; set; }
        public List<ToastMessage> ToastMessages { get; set; }

        public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
        {
            var toast = new ToastMessage()
            {
                Title = title,
                Message = message,
                ToastType = toastType
            };
            this.ToastMessages.Add(toast);
            return toast;
        }

        public Toastr()
        {
            this.ToastMessages = new List<ToastMessage>();
            this.ShowNewestOnTop = false;
            this.ShowCloseButton = false;
            this.ProgressBar = true;
        }
    }
}