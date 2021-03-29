using news.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace news.Infrastructure.Models
{

        public class ResultObject<T>
        {
            private ResultCode _code;

            /// <summary>
            /// Constructor
            /// </summary>
            public ResultObject()
            {
                this._code = ResultCode.Ok;
                Message = new ResultMessage(this._code, PopupOption.Default);
            }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="code"></param>
            /// <param name="message"></param>
            /// <param name="data"></param>
            public ResultObject(ResultCode code, ResultMessage message, T data)
            {
                this._code = code;
                this.Message = message;
                this.Data = data;
            }

            /// <summary>
            /// 
            /// </summary>
            public ResultCode Code
            {
                set
                {
                    this._code = value;
                }
                get
                {
                    if (this.Message != null)
                    {
                        try
                        {
                            if (this.Message.Popup.Equals(PopupOption.Default))
                            {
                                switch ((int)this._code)
                                {
                                    // system error
                                    case -120:
                                        this.Message.Popup = PopupOption.Hide;
                                        break;
                                    // warning
                                    case int n when (n >= -399 && n <= -300):
                                        this.Message.Popup = PopupOption.Show;
                                        break;
                                    // error
                                    case int n when (n >= -199 && n <= -100):
                                        this.Message.Popup = PopupOption.Show;
                                        break;
                                    // success
                                    case 200:
                                        this.Message.Popup = PopupOption.Hide;
                                        break;
                                    default:
                                        break;
                                }
                                this.Message.Title = this._code.GetDescription();
                            }
                        }
                        catch
                        {
                            Message = new ResultMessage(ResultCode.ErrorException, PopupOption.Show);
                        }
                    }
                    return this._code;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public ResultMessage Message { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public T Data { set; get; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ResultMessage
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="resultCode"></param>
            /// <param name="popup"></param>
            public ResultMessage(ResultCode resultCode = ResultCode.Ok, PopupOption popup = PopupOption.Default)
            {
                Title = resultCode.GetDescription();
                Message = resultCode.GetDescription();
                Popup = popup;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Title { set; get; }

            /// <summary>
            /// 
            /// </summary>
            public string Message { set; get; }

            /// <summary>
            /// 
            /// </summary>
            public string ExMessage { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public PopupOption Popup { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class NullDataType
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static class ResultObject
        {
            /// <summary>
            /// Fail
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="message"></param>
            /// <param name="title"></param>
            /// <param name="data"></param>
            /// <param name="popup"></param>
            /// <returns></returns>
            public static ResultObject<T> Fail<T>(
                string message = "Lỗi",
                string title = "",
                T data = default,
                PopupOption popup = PopupOption.Default)
            {
                var msg = new ResultMessage()
                {
                    Title = string.IsNullOrWhiteSpace(title) ? ResultCode.ErrorFail.GetDescription() : title,
                    Message = message,
                    Popup = popup
                };
                return new ResultObject<T>(ResultCode.ErrorFail, msg, data);
            }

            /// <summary>
            /// Error
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="exMessage"></param>
            /// <param name="message"></param>
            /// <param name="data"></param>
            /// <param name="popup"></param>
            /// <returns></returns>
            public static ResultObject<T> Error<T>(
                string exMessage,
                string message = "Đã xảy ra lỗi trong quá trình xử lý.",
                T data = default,
                PopupOption popup = PopupOption.Default)
            {
                var msg = new ResultMessage()
                {
                    Title = ResultCode.ErrorException.GetDescription(),
                    Message = message,
                    ExMessage = exMessage,
                    Popup = popup
                };
                return new ResultObject<T>(ResultCode.ErrorException, msg, data);
            }

            /// <summary>
            /// Ok
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <param name="message"></param>
            /// <param name="popup"></param>
            /// <returns></returns>
            public static ResultObject<T> Ok<T>(T data, string message = "Thành công", PopupOption popup = PopupOption.Default)
            {
                var msg = new ResultMessage()
                {
                    Title = ResultCode.Ok.GetDescription(),
                    Message = message,
                    Popup = popup
                };
                return new ResultObject<T>(ResultCode.Ok, msg, data);
            }

            /// <summary>
            /// Warning
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <param name="message"></param>
            /// <param name="popup"></param>
            /// <returns></returns>
            public static ResultObject<T> Warning<T>(T data, string message, PopupOption popup = PopupOption.Default)
            {
                var msg = new ResultMessage()
                {
                    Title = ResultCode.Warning.GetDescription(),
                    Message = message,
                    Popup = popup
                };
                return new ResultObject<T>(ResultCode.Warning, msg, data);
            }

            /// <summary>
            /// Info
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <param name="message"></param>
            /// <param name="popup"></param>
            /// <returns></returns>
            public static ResultObject<T> Info<T>(T data, string message = "Thành công", PopupOption popup = PopupOption.Default)
            {
                var msg = new ResultMessage()
                {
                    Title = ResultCode.ErrorNoContent.GetDescription(),
                    Message = message,
                    Popup = popup
                };
                return new ResultObject<T>(ResultCode.ErrorNoContent, msg, data);
            }

            /// <summary>
            /// Fail
            /// </summary>
            /// <param name="message"></param>
            /// <param name="popup"></param>
            /// <param name="code"></param>
            /// <returns></returns>
            public static ResultObject<NullDataType> Fail(
                string message = "Lỗi",
                PopupOption popup = PopupOption.Default,
                ResultCode code = ResultCode.ErrorFail)
            {
                var msg = new ResultMessage()
                {
                    Title = code.GetDescription(),
                    Message = message,
                    Popup = popup
                };
                return new ResultObject<NullDataType>(code, msg, default);
            }

            /// <summary>
            /// Ok
            /// </summary>
            /// <param name="message"></param>
            /// <param name="popup"></param>
            /// <returns></returns>
            public static ResultObject<NullDataType> Ok(string message = "Thành công",
                PopupOption popup = PopupOption.Default)
            {
                var msg = new ResultMessage()
                {
                    Title = ResultCode.Ok.GetDescription(),
                    Message = message,
                    Popup = popup
                };
                return new ResultObject<NullDataType>(ResultCode.Ok, msg, default);
            }

            /// <summary>
            /// Error
            /// </summary>
            /// <param name="message"></param>
            /// <param name="exMessage"></param>
            /// <param name="popup"></param>
            /// <param name="code"></param>
            /// <returns></returns>
            public static ResultObject<NullDataType> Error(string message, string exMessage,
                PopupOption popup = PopupOption.Default,
                ResultCode code = ResultCode.ErrorException)
            {
                var msg = new ResultMessage()
                {
                    Title = code.GetDescription(),
                    Message = message,
                    ExMessage = exMessage,
                    Popup = popup
                };
                return new ResultObject<NullDataType>(code, msg, default);
            }
        }
    }

