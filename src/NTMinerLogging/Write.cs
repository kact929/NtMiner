﻿using System;

namespace NTMiner {
    public static class Write {
        public static Action<string, ConsoleColor, bool> WriteUserLineMethod;
        public static Action<string, ConsoleColor> WriteDevLineMethod;

        static Write() {
            WriteUserLineMethod = (line, color, isNotice) => {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(line, isNotice);
                Console.ForegroundColor = oldColor;
            };
            WriteDevLineMethod = (line, color) => {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(line);
                Console.ForegroundColor = oldColor;
            };
        }

        public static void UserLine(string text, MessageType messageType = MessageType.Default) {
            UserLine(text, messageType.ToConsoleColor());
        }

        public static void UserError(string text) {
            UserLine(text, MessageType.Error);
        }

        public static void UserInfo(string text) {
            UserLine(text, MessageType.Info);
        }

        public static void UserOk(string text) {
            UserLine(text, MessageType.Ok);
        }

        public static void UserWarn(string text) {
            UserLine(text, MessageType.Warn);
        }

        public static void UserFail(string text) {
            UserLine(text, MessageType.Fail);
        }

        public static void UserFatal(string text) {
            UserLine(text, MessageType.Fatal);
        }

        public static void UserLine(string text, ConsoleColor foreground, bool isNotice = true) {
            WriteUserLineMethod?.Invoke(text, foreground, isNotice);
        }

        public static void DevLine(string text, MessageType messageType = MessageType.Default) {
            if (!DevMode.IsDevMode) {
                return;
            }
            text = $"{DateTime.Now.ToString("HH:mm:ss fff")}  {text}";
            WriteDevLineMethod?.Invoke(text, messageType.ToConsoleColor());
        }

        public static void DevError(string text) {
            DevLine(text, MessageType.Error);
        }

        public static void DevInfo(string text) {
            DevLine(text, MessageType.Info);
        }

        public static void DevOk(string text) {
            DevLine(text, MessageType.Ok);
        }

        public static void DevWarn(string text) {
            DevLine(text, MessageType.Warn);
        }

        public static void DevFail(string text) {
            DevLine(text, MessageType.Fail);
        }

        public static void DevFatal(string text) {
            DevLine(text, MessageType.Fatal);
        }
    }
}
