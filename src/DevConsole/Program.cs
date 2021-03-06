﻿using NTMiner.NoDevFee;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NTMiner {
    internal unsafe class Program {
        private static volatile bool s_running = true;
        private static string s_poolIp;
        private static string s_keyword;
        private static bool s_ranOnce = false;

        // keyword=eth_submitLogin
        private static void Main(string[] args) {
            Console.CancelKeyPress += delegate { s_running = false; };

            if (args.Length >= 1) {
                if (args[0].StartsWith("keyword=")) {
                    s_keyword = args[0].Substring("keyword=".Length);
                }
                else {
                    s_poolIp = args[0];
                }
            }
            else {
                NTMinerConsole.UserError("ERROR: No poolIp argument was found.");
                NTMinerConsole.UserInfo("按任意键退出");
                Console.ReadKey();
                return;
            }
            if (args.Length >= 2) {
                Console.Title = args[1] + "开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
            }
            else {
                Console.Title = "开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
            }

            WinDivertExtract.Extract();

            string filter;
            if (string.IsNullOrEmpty(s_keyword)) {
                filter = $"ip && (ip.DstAddr = {s_poolIp} || ip.SrcAddr = {s_poolIp}) && tcp && tcp.PayloadLength > 100";
            }
            else {
                filter = $"ip && tcp && tcp.PayloadLength > 100";
            }
            NTMinerConsole.UserInfo(filter);
            var divertHandle = SafeNativeMethods.WinDivertOpen(filter, WINDIVERT_LAYER.WINDIVERT_LAYER_NETWORK, 0, 0);

            try {
                if (divertHandle != IntPtr.Zero) {
                    Parallel.ForEach(Enumerable.Range(0, Environment.ProcessorCount), x => RunDiversion(divertHandle, ref s_ranOnce, ref s_poolIp));
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message, e.StackTrace);
            }
            finally {
                SafeNativeMethods.WinDivertClose(divertHandle);
            }
        }

        private static void RunDiversion(IntPtr handle, ref bool ranOnce, ref string poolIp) {
            byte[] packet = new byte[65535];
            try {
                while (s_running) {
                    uint readLength = 0;
                    WINDIVERT_IPHDR* ipv4Header = null;
                    WINDIVERT_TCPHDR* tcpHdr = null;
                    WINDIVERT_ADDRESS addr = new WINDIVERT_ADDRESS();

                    if (!SafeNativeMethods.WinDivertRecv(handle, packet, (uint)packet.Length, ref addr, ref readLength)) {
                        continue;
                    }

                    if (!ranOnce && readLength > 1) {
                        ranOnce = true;
                        NTMinerConsole.UserInfo("Diversion running..");
                    }

                    fixed (byte* inBuf = packet) {
                        byte* payload = null;
                        SafeNativeMethods.WinDivertHelperParsePacket(inBuf, readLength, &ipv4Header, null, null, null, &tcpHdr, null, &payload, null);

                        if (ipv4Header != null && tcpHdr != null && payload != null) {
                            string text = Marshal.PtrToStringAnsi((IntPtr)payload);
                            if (!string.IsNullOrEmpty(s_keyword)) {
                                if (text.Contains(s_keyword)) {
                                    NTMinerConsole.UserInfo(text);
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    Logger.InfoDebugLine(text);
                                }
                            }
                            else {
                                string dstIp = ipv4Header->DstAddr.ToString();
                                var dstPort = tcpHdr->DstPort;
                                string arrow = $"->{dstIp}:{dstPort.ToString()}";
                                if (dstIp == poolIp) {
                                    arrow = $"{dstIp}:{dstPort.ToString()}<-";
                                    NTMinerConsole.UserInfo($"<-<-<-<-<-<-<-<-<-<-<-<-<-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-");
                                }
                                else {
                                    NTMinerConsole.UserInfo($"->->->->->->->->->->->->->{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}->->->->->->->->->->->->->->->");
                                }
                                NTMinerConsole.UserInfo(arrow + text);
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                        }
                    }

                    SafeNativeMethods.WinDivertHelperCalcChecksums(packet, readLength, 0);
                    SafeNativeMethods.WinDivertSendEx(handle, packet, readLength, 0, ref addr, IntPtr.Zero, IntPtr.Zero);
                }

            }
            catch (Exception e) {
                NTMinerConsole.UserInfo(e.ToString());
                NTMinerConsole.UserInfo("按任意键退出");
                Console.ReadKey();
                return;
            }
        }
    }
}
