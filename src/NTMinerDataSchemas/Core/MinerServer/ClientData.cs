﻿using LiteDB;
using Newtonsoft.Json;
using NTMiner.Core.MinerClient;
using NTMiner.Report;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NTMiner.Core.MinerServer {
    public class ClientData : SpeedData, IClientData {
        private static readonly Dictionary<string, PropertyInfo> _reflectionUpdateProperties = new Dictionary<string, PropertyInfo>();

        public static bool TryGetReflectionUpdateProperty(string propertyName, out PropertyInfo propertyInfo) {
            return _reflectionUpdateProperties.TryGetValue(propertyName, out propertyInfo);
        }

        static ClientData() {
            Type type = typeof(ClientData);
            // 这算是一个安全措施，因为propertyName是来自客户端传入的，所以需要白名单。
            HashSet<string> propertyNames = new HashSet<string> {
                nameof(WorkerName),
                nameof(GroupId),
                nameof(WindowsLoginName),
                nameof(WindowsPassword)
            };
            foreach (var propertyName in propertyNames) {
                _reflectionUpdateProperties.Add(propertyName, type.GetProperty(propertyName));
            }
        }

        public ClientData() : base() {
        }

        public static ClientData Create(IMinerData data) {
            return new ClientData() {
                Id = data.Id,
                MineContextId = Guid.Empty,
                ClientId = data.ClientId,
                MACAddress = data.MACAddress,
                LocalIp = data.LocalIp,
                MinerIp = data.MinerIp,
                MinerName = data.MinerName,
                WorkerName = data.WorkerName,
                CreatedOn = data.CreatedOn,
                GroupId = data.GroupId,
                WorkId = data.WorkId,
                WindowsLoginName = data.WindowsLoginName,
                WindowsPassword = data.WindowsPassword,
                MineWorkId = Guid.Empty,
                MineWorkName = string.Empty,
                IsAutoBoot = false,
                IsAutoStart = false,
                AutoStartDelaySeconds = 15,
                IsAutoRestartKernel = false,
                AutoRestartKernelTimes = 10,
                IsNoShareRestartKernel = false,
                NoShareRestartKernelMinutes = 0,
                IsNoShareRestartComputer = false,
                NoShareRestartComputerMinutes = 0,
                IsPeriodicRestartKernel = false,
                PeriodicRestartKernelHours = 0,
                IsPeriodicRestartComputer = false,
                PeriodicRestartComputerHours = 0,
                PeriodicRestartKernelMinutes = 10,
                PeriodicRestartComputerMinutes = 10,
                IsAutoStartByCpu = false,
                IsAutoStopByCpu = false,
                CpuGETemperatureSeconds = 60,
                CpuLETemperatureSeconds = 60,
                CpuStartTemperature = 40,
                CpuStopTemperature = 65,
                CpuPerformance = 0,
                CpuTemperature = 0,
                IsRaiseHighCpuEvent = false,
                HighCpuPercent = 80,
                HighCpuSeconds = 10,
                GpuDriver = string.Empty,
                GpuType = GpuType.Empty,
                OSName = string.Empty,
                OSVirtualMemoryMb = 0,
                GpuInfo = string.Empty,
                Version = string.Empty,
                IsMining = false,
                BootOn = DateTime.MinValue,
                MineStartedOn = DateTime.MinValue,
                MinerActiveOn = DateTime.MinValue,
                MainCoinCode = string.Empty,
                MainCoinTotalShare = 0,
                MainCoinRejectShare = 0,
                MainCoinSpeed = 0,
                MainCoinPool = string.Empty,
                MainCoinWallet = string.Empty,
                Kernel = string.Empty,
                IsDualCoinEnabled = false,
                DualCoinPool = string.Empty,
                DualCoinWallet = string.Empty,
                DualCoinCode = string.Empty,
                DualCoinTotalShare = 0,
                DualCoinRejectShare = 0,
                DualCoinSpeed = 0,
                KernelCommandLine = string.Empty,
                MainCoinPoolDelay = string.Empty,
                DualCoinPoolDelay = string.Empty,
                DiskSpace = string.Empty,
                IsFoundOneGpuShare = false,
                IsRejectOneGpuShare = false,
                IsGotOneIncorrectGpuShare = false,
                KernelSelfRestartCount = 0,
                LoginName = data.LoginName,
                IsOuterUserEnabled = data.IsOuterUserEnabled,
                OuterUserId = data.OuterUserId,
                ReportOuterUserId = data.OuterUserId,
                TotalPhysicalMemoryMb = 0,
                LocalServerMessageTimestamp = Timestamp.UnixBaseTime,
                NetActiveOn = DateTime.MinValue,
                IsOnline = false,
                AESPassword = string.Empty,
                AESPasswordOn = DateTime.MinValue,
                IsAutoDisableWindowsFirewall = true,
                IsDisableAntiSpyware = true,
                IsDisableUAC = true,
                IsDisableWAU = true,
                MainCoinSpeedOn = DateTime.MinValue,
                DualCoinSpeedOn = DateTime.MinValue,
                GpuTable = new GpuSpeedData[0],
                DualCoinPoolDelayNumber = 0,
                MainCoinPoolDelayNumber = 0,
                MainCoinRejectPercent = 0,
                DualCoinRejectPercent = 0
            };
        }

        public static ClientData Clone(ClientData data) {
            return new ClientData() {
                Id = data.Id,
                MineContextId = data.MineContextId,
                MinerName = data.MinerName,
                MinerIp = data.MinerIp,
                CreatedOn = data.CreatedOn,
                MinerActiveOn = data.MinerActiveOn,
                GroupId = data.GroupId,
                WorkId = data.WorkId,
                WindowsLoginName = data.WindowsLoginName,
                WindowsPassword = data.WindowsPassword,
                MACAddress = data.MACAddress,
                LocalIp = data.LocalIp,
                ClientId = data.ClientId,
                IsAutoBoot = data.IsAutoBoot,
                IsAutoStart = data.IsAutoStart,
                AutoStartDelaySeconds = data.AutoStartDelaySeconds,
                IsAutoRestartKernel = data.IsAutoRestartKernel,
                AutoRestartKernelTimes = data.AutoRestartKernelTimes,
                IsNoShareRestartKernel = data.IsNoShareRestartKernel,
                NoShareRestartKernelMinutes = data.NoShareRestartKernelMinutes,
                IsNoShareRestartComputer = data.IsNoShareRestartComputer,
                NoShareRestartComputerMinutes = data.NoShareRestartComputerMinutes,
                IsPeriodicRestartKernel = data.IsPeriodicRestartKernel,
                PeriodicRestartKernelHours = data.PeriodicRestartKernelHours,
                IsPeriodicRestartComputer = data.IsPeriodicRestartComputer,
                PeriodicRestartComputerHours = data.PeriodicRestartComputerHours,
                PeriodicRestartComputerMinutes = data.PeriodicRestartComputerMinutes,
                PeriodicRestartKernelMinutes = data.PeriodicRestartKernelMinutes,
                IsAutoStopByCpu = data.IsAutoStopByCpu,
                IsAutoStartByCpu = data.IsAutoStartByCpu,
                CpuStopTemperature = data.CpuStopTemperature,
                CpuStartTemperature = data.CpuStartTemperature,
                CpuLETemperatureSeconds = data.CpuLETemperatureSeconds,
                CpuGETemperatureSeconds = data.CpuGETemperatureSeconds,
                CpuTemperature = data.CpuTemperature,
                CpuPerformance = data.CpuPerformance,
                IsRaiseHighCpuEvent = data.IsRaiseHighCpuEvent,
                HighCpuPercent = data.HighCpuPercent,
                HighCpuSeconds = data.HighCpuSeconds,
                GpuDriver = data.GpuDriver,
                GpuType = data.GpuType,
                OSName = data.OSName,
                OSVirtualMemoryMb = data.OSVirtualMemoryMb,
                TotalPhysicalMemoryMb = data.TotalPhysicalMemoryMb,
                GpuInfo = data.GpuInfo,
                Version = data.Version,
                IsMining = data.IsMining,
                BootOn = data.BootOn,
                MineStartedOn = data.MineStartedOn,
                MainCoinCode = data.MainCoinCode,
                MainCoinTotalShare = data.MainCoinTotalShare,
                MainCoinRejectShare = data.MainCoinRejectShare,
                MainCoinSpeed = data.MainCoinSpeed,
                MainCoinPool = data.MainCoinPool,
                MainCoinWallet = data.MainCoinWallet,
                Kernel = data.Kernel,
                IsDualCoinEnabled = data.IsDualCoinEnabled,
                DualCoinPool = data.DualCoinPool,
                DualCoinWallet = data.DualCoinWallet,
                DualCoinCode = data.DualCoinCode,
                DualCoinTotalShare = data.DualCoinTotalShare,
                DualCoinRejectShare = data.DualCoinRejectShare,
                DualCoinSpeed = data.DualCoinSpeed,
                KernelCommandLine = data.KernelCommandLine,
                GpuTable = data.GpuTable,
                MineWorkId = data.MineWorkId,
                MineWorkName = data.MineWorkName,
                WorkerName = data.WorkerName,
                DiskSpace = data.DiskSpace,
                MainCoinPoolDelay = data.MainCoinPoolDelay,
                DualCoinPoolDelay = data.DualCoinPoolDelay,
                IsFoundOneGpuShare = data.IsFoundOneGpuShare,
                IsRejectOneGpuShare = data.IsRejectOneGpuShare,
                IsGotOneIncorrectGpuShare = data.IsGotOneIncorrectGpuShare,
                KernelSelfRestartCount = data.KernelSelfRestartCount,
                LocalServerMessageTimestamp = data.LocalServerMessageTimestamp,
                LoginName = data.LoginName,
                IsOuterUserEnabled = data.IsOuterUserEnabled,
                OuterUserId = data.OuterUserId,
                ReportOuterUserId = data.ReportOuterUserId,
                NetActiveOn = data.NetActiveOn,
                IsOnline = data.IsOnline,
                IsDisableWAU = data.IsDisableWAU,
                IsDisableUAC = data.IsDisableUAC,
                AESPassword = data.AESPassword,
                AESPasswordOn = data.AESPasswordOn,
                IsDisableAntiSpyware = data.IsDisableAntiSpyware,
                IsAutoDisableWindowsFirewall = data.IsAutoDisableWindowsFirewall,
                MainCoinSpeedOn = data.MainCoinSpeedOn,
                DualCoinSpeedOn = data.DualCoinSpeedOn,
                DualCoinRejectPercent = data.DualCoinRejectPercent,
                MainCoinRejectPercent = data.MainCoinRejectPercent,
                MainCoinPoolDelayNumber = data.MainCoinPoolDelayNumber,
                DualCoinPoolDelayNumber = data.DualCoinPoolDelayNumber
            };
        }

        public static ClientData Create(ReportState state, string minerIp) {
            return new ClientData {
                Id = ObjectId.NewObjectId().ToString(),
                ClientId = state.ClientId,
                IsMining = state.IsMining,
                CreatedOn = DateTime.Now,
                MinerActiveOn = DateTime.Now,
                MinerIp = minerIp
            };
        }

        /// <summary>
        /// 从给定的speedData中提取出主币矿池延时，辅币矿池延时，主币拒绝率，辅币拒绝率。
        /// </summary>
        private static void Extract(
            ISpeedData speedData, 
            out int mainCoinPoolDelayNumber, 
            out int dualCoinPoolDelayNumber, 
            out double mainCoinRejectPercent, 
            out double dualCoinRejectPercent) {

            mainCoinPoolDelayNumber = 0;
            dualCoinPoolDelayNumber = 0;
            mainCoinRejectPercent = 0.0;
            dualCoinRejectPercent = 0.0;
            if (!string.IsNullOrEmpty(speedData.MainCoinPoolDelay)) {
                string text = speedData.MainCoinPoolDelay.Trim();
                int count = 0;
                for (int i = 0; i < text.Length; i++) {
                    if (!char.IsNumber(text[i])) {
                        count = i;
                        break;
                    }
                }
                if (count != 0) {
                    mainCoinPoolDelayNumber = int.Parse(text.Substring(0, count));
                }
            }
            if (!string.IsNullOrEmpty(speedData.DualCoinPoolDelay)) {
                string text = speedData.DualCoinPoolDelay.Trim();
                int count = 0;
                for (int i = 0; i < text.Length; i++) {
                    if (!char.IsNumber(text[i])) {
                        count = i;
                        break;
                    }
                }
                if (count != 0) {
                    dualCoinPoolDelayNumber = int.Parse(text.Substring(0, count));
                }
            }
            if (speedData.MainCoinTotalShare != 0) {
                mainCoinRejectPercent = (speedData.MainCoinRejectShare * 100.0) / speedData.MainCoinTotalShare;
            }
            if (speedData.DualCoinTotalShare != 0) {
                dualCoinRejectPercent = (speedData.DualCoinRejectShare * 100.0) / speedData.DualCoinTotalShare;
            }
        }

        public static ClientData Create(ISpeedData speedData, string minerIp) {
            Extract(
                speedData, 
                out int mainCoinPoolDelayNumber, 
                out int dualCoinPoolDelayNumber, 
                out double mainCoinRejectPercent, 
                out double dualCoinRejectPercent);
            return new ClientData() {
                Id = ObjectId.NewObjectId().ToString(),
                MineContextId = speedData.MineContextId,
                MinerName = speedData.MinerName,
                MinerIp = minerIp,
                CreatedOn = DateTime.Now,
                MinerActiveOn = DateTime.Now,
                GroupId = Guid.Empty,
                WorkId = Guid.Empty,// 这是服务端指定的作业，不受客户端的影响
                WindowsLoginName = string.Empty,
                WindowsPassword = string.Empty,
                MACAddress = speedData.MACAddress,
                LocalIp = speedData.LocalIp,
                ClientId = speedData.ClientId,
                IsAutoBoot = speedData.IsAutoBoot,
                IsAutoStart = speedData.IsAutoStart,
                AutoStartDelaySeconds = speedData.AutoStartDelaySeconds,
                IsAutoRestartKernel = speedData.IsAutoRestartKernel,
                AutoRestartKernelTimes = speedData.AutoRestartKernelTimes,
                IsNoShareRestartKernel = speedData.IsNoShareRestartKernel,
                NoShareRestartKernelMinutes = speedData.NoShareRestartKernelMinutes,
                IsNoShareRestartComputer = speedData.IsNoShareRestartComputer,
                NoShareRestartComputerMinutes = speedData.NoShareRestartComputerMinutes,
                IsPeriodicRestartKernel = speedData.IsPeriodicRestartKernel,
                PeriodicRestartKernelHours = speedData.PeriodicRestartKernelHours,
                IsPeriodicRestartComputer = speedData.IsPeriodicRestartComputer,
                PeriodicRestartComputerHours = speedData.PeriodicRestartComputerHours,
                PeriodicRestartComputerMinutes = speedData.PeriodicRestartComputerMinutes,
                PeriodicRestartKernelMinutes = speedData.PeriodicRestartKernelMinutes,
                IsAutoStopByCpu = speedData.IsAutoStopByCpu,
                IsAutoStartByCpu = speedData.IsAutoStartByCpu,
                CpuStopTemperature = speedData.CpuStopTemperature,
                CpuStartTemperature = speedData.CpuStartTemperature,
                CpuLETemperatureSeconds = speedData.CpuLETemperatureSeconds,
                CpuGETemperatureSeconds = speedData.CpuGETemperatureSeconds,
                CpuTemperature = speedData.CpuTemperature,
                CpuPerformance = speedData.CpuPerformance,
                IsRaiseHighCpuEvent = speedData.IsRaiseHighCpuEvent,
                HighCpuPercent = speedData.HighCpuPercent,
                HighCpuSeconds = speedData.HighCpuSeconds,
                GpuDriver = speedData.GpuDriver,
                GpuType = speedData.GpuType,
                OSName = speedData.OSName,
                OSVirtualMemoryMb = speedData.OSVirtualMemoryMb,
                TotalPhysicalMemoryMb = speedData.TotalPhysicalMemoryMb,
                GpuInfo = speedData.GpuInfo,
                Version = speedData.Version,
                IsMining = speedData.IsMining,
                BootOn = speedData.BootOn,
                MineStartedOn = speedData.MineStartedOn,
                MainCoinCode = speedData.MainCoinCode,
                MainCoinTotalShare = speedData.MainCoinTotalShare,
                MainCoinRejectShare = speedData.MainCoinRejectShare,
                MainCoinSpeed = speedData.MainCoinSpeed,
                MainCoinPool = speedData.MainCoinPool,
                MainCoinWallet = speedData.MainCoinWallet,
                Kernel = speedData.Kernel,
                IsDualCoinEnabled = speedData.IsDualCoinEnabled,
                DualCoinPool = speedData.DualCoinPool,
                DualCoinWallet = speedData.DualCoinWallet,
                DualCoinCode = speedData.DualCoinCode,
                DualCoinTotalShare = speedData.DualCoinTotalShare,
                DualCoinRejectShare = speedData.DualCoinRejectShare,
                DualCoinSpeed = speedData.DualCoinSpeed,
                KernelCommandLine = speedData.KernelCommandLine,
                MainCoinSpeedOn = speedData.MainCoinSpeedOn,
                DualCoinSpeedOn = speedData.DualCoinSpeedOn,
                GpuTable = speedData.GpuTable,
                MineWorkId = speedData.MineWorkId,
                MineWorkName = speedData.MineWorkName,
                DiskSpace = speedData.DiskSpace,
                MainCoinPoolDelay = speedData.MainCoinPoolDelay,
                DualCoinPoolDelay = speedData.DualCoinPoolDelay,
                IsFoundOneGpuShare = speedData.IsFoundOneGpuShare,
                IsRejectOneGpuShare = speedData.IsRejectOneGpuShare,
                IsGotOneIncorrectGpuShare = speedData.IsGotOneIncorrectGpuShare,
                KernelSelfRestartCount = speedData.KernelSelfRestartCount - 1,// 需要减1
                LocalServerMessageTimestamp = speedData.LocalServerMessageTimestamp,
                AESPassword = string.Empty,
                AESPasswordOn = DateTime.MinValue,
                IsAutoDisableWindowsFirewall = speedData.IsAutoDisableWindowsFirewall,
                IsDisableAntiSpyware = speedData.IsDisableAntiSpyware,
                IsDisableUAC = speedData.IsDisableUAC,
                IsDisableWAU = speedData.IsDisableWAU,
                IsOnline = false,
                NetActiveOn = DateTime.MinValue,
                LoginName = string.Empty,
                IsOuterUserEnabled = speedData.IsOuterUserEnabled,
                OuterUserId = string.Empty,
                ReportOuterUserId = speedData.ReportOuterUserId,
                WorkerName = string.Empty,
                DualCoinPoolDelayNumber = dualCoinPoolDelayNumber,
                MainCoinPoolDelayNumber = mainCoinPoolDelayNumber,
                MainCoinRejectPercent = mainCoinRejectPercent,
                DualCoinRejectPercent = dualCoinRejectPercent
            };
        }

        public void Update(MinerSign minerSign, out bool isChanged) {
            this.LoginName = minerSign.LoginName;
            isChanged = false;
            if (!isChanged) {
                isChanged = this.ClientId != minerSign.ClientId;
            }
            this.ClientId = minerSign.ClientId;
            if (!isChanged) {
                isChanged = this.OuterUserId != minerSign.OuterUserId;
            }
            this.OuterUserId = minerSign.OuterUserId;
            if (!isChanged) {
                isChanged = this.AESPassword != minerSign.AESPassword;
            }
            this.AESPassword = minerSign.AESPassword;
            if (!isChanged) {
                isChanged = this.AESPasswordOn != minerSign.AESPasswordOn;
            }
            this.AESPasswordOn = minerSign.AESPasswordOn;
        }

        public SpeedData ToSpeedData() {
            return new SpeedData {
                AutoRestartKernelTimes = this.AutoRestartKernelTimes,
                AutoStartDelaySeconds = this.AutoStartDelaySeconds,
                BootOn = this.BootOn,
                ClientId = this.ClientId,
                CpuGETemperatureSeconds = this.CpuGETemperatureSeconds,
                CpuLETemperatureSeconds = this.CpuLETemperatureSeconds,
                CpuPerformance = this.CpuPerformance,
                CpuStartTemperature = this.CpuStartTemperature,
                CpuStopTemperature = this.CpuStopTemperature,
                CpuTemperature = this.CpuTemperature,
                DiskSpace = this.DiskSpace,
                DualCoinCode = this.DualCoinCode,
                DualCoinPool = this.DualCoinPool,
                DualCoinPoolDelay = this.DualCoinPoolDelay,
                DualCoinRejectShare = this.DualCoinRejectShare,
                DualCoinSpeed = this.DualCoinSpeed,
                DualCoinSpeedOn = this.DualCoinSpeedOn,
                DualCoinTotalShare = this.DualCoinTotalShare,
                DualCoinWallet = this.DualCoinWallet,
                GpuDriver = this.GpuDriver,
                GpuInfo = this.GpuInfo,
                GpuTable = this.GpuTable,
                GpuType = this.GpuType,
                HighCpuPercent = this.HighCpuPercent,
                HighCpuSeconds = this.HighCpuSeconds,
                IsAutoBoot = this.IsAutoBoot,
                IsAutoDisableWindowsFirewall = this.IsAutoDisableWindowsFirewall,
                IsAutoRestartKernel = this.IsAutoRestartKernel,
                IsAutoStart = this.IsAutoStart,
                IsAutoStartByCpu = this.IsAutoStartByCpu,
                IsAutoStopByCpu = this.IsAutoStopByCpu,
                IsDisableAntiSpyware = this.IsDisableAntiSpyware,
                IsDisableUAC = this.IsDisableUAC,
                IsDisableWAU = this.IsDisableWAU,
                IsDualCoinEnabled = this.IsDualCoinEnabled,
                IsFoundOneGpuShare = this.IsFoundOneGpuShare,
                IsGotOneIncorrectGpuShare = this.IsGotOneIncorrectGpuShare,
                IsMining = this.IsMining,
                IsNoShareRestartComputer = this.IsNoShareRestartComputer,
                IsNoShareRestartKernel = this.IsNoShareRestartKernel,
                IsOuterUserEnabled = this.IsOuterUserEnabled,
                ReportOuterUserId = this.ReportOuterUserId,
                IsPeriodicRestartComputer = this.IsPeriodicRestartComputer,
                IsPeriodicRestartKernel = this.IsPeriodicRestartKernel,
                IsRaiseHighCpuEvent = this.IsRaiseHighCpuEvent,
                IsRejectOneGpuShare = this.IsRejectOneGpuShare,
                Kernel = this.Kernel,
                KernelCommandLine = this.KernelCommandLine,
                KernelSelfRestartCount = this.KernelSelfRestartCount,
                LocalIp = this.LocalIp,
                LocalServerMessageTimestamp = this.LocalServerMessageTimestamp,
                MACAddress = this.MACAddress,
                MainCoinCode = this.MainCoinCode,
                MainCoinPool = this.MainCoinPool,
                MainCoinPoolDelay = this.MainCoinPoolDelay,
                MainCoinRejectShare = this.MainCoinRejectShare,
                MainCoinSpeed = this.MainCoinSpeed,
                MainCoinSpeedOn = this.MainCoinSpeedOn,
                MainCoinTotalShare = this.MainCoinTotalShare,
                MainCoinWallet = this.MainCoinWallet,
                MineContextId = this.MineContextId,
                MinerIp = this.MinerIp,
                MinerName = this.MinerName,
                MineStartedOn = this.MineStartedOn,
                MineWorkId = this.MineWorkId,
                MineWorkName = this.MineWorkName,
                NoShareRestartComputerMinutes = this.NoShareRestartComputerMinutes,
                NoShareRestartKernelMinutes = this.NoShareRestartKernelMinutes,
                OSName = this.OSName,
                OSVirtualMemoryMb = this.OSVirtualMemoryMb,
                PeriodicRestartComputerHours = this.PeriodicRestartComputerHours,
                PeriodicRestartComputerMinutes = this.PeriodicRestartComputerMinutes,
                PeriodicRestartKernelHours = this.PeriodicRestartKernelHours,
                PeriodicRestartKernelMinutes = this.PeriodicRestartKernelMinutes,
                TotalPhysicalMemoryMb = this.TotalPhysicalMemoryMb,
                Version = this.Version
            };
        }

        /// <summary>
        /// 上报算力时。
        /// 因为只有MinerData具有的成员发生了变化时才需要持久化所以该非法输出isMinerDataChanged参数以表示MinerData的成员是否发生了变化。
        /// </summary>
        /// <param name="speedData"></param>
        /// <param name="minerIp"></param>
        /// <param name="isMinerDataChanged"></param>
        public void Update(ISpeedData speedData, string minerIp, out bool isMinerDataChanged) {
            Update(speedData, out isMinerDataChanged);
            if (!isMinerDataChanged && minerIp != this.MinerIp) {
                isMinerDataChanged = true;
            }
            this.MinerIp = minerIp;
        }

        /// <summary>
        /// 因为只有MinerData具有的成员发生了变化时才需要持久化所以该非法输出isMinerDataChanged参数以表示MinerData的成员是否发生了变化。
        /// </summary>
        /// <param name="state"></param>
        /// <param name="minerIp"></param>
        /// <param name="isMinerDataChanged"></param>
        public void Update(ReportState state, string minerIp, out bool isMinerDataChanged) {
            isMinerDataChanged = false;
            this.IsMining = state.IsMining;
            this.MinerActiveOn = DateTime.Now;
            if (!isMinerDataChanged && minerIp != this.MinerIp) {
                isMinerDataChanged = true;
            }
            this.MinerIp = minerIp;
        }

        private DateTime _preUpdateOn = DateTime.Now;
        private int _preMainCoinShare = 0;
        private int _preDualCoinShare = 0;
        private int _preMainCoinRejectShare = 0;
        private int _preDualCoinRejectShare = 0;
        private string _preMainCoin;
        private string _preDualCoin;
        /// <summary>
        /// 上报算力时和拉取算力时。
        /// 因为只有MinerData具有的成员发生了变化时才需要持久化所以该非法输出isMinerDataChanged参数以表示MinerData的成员是否发生了变化。
        /// </summary>
        /// <param name="speedData"></param>
        /// <param name="isMinerDataChanged"></param>
        public void Update(ISpeedData speedData, out bool isMinerDataChanged) {
            isMinerDataChanged = false;
            if (speedData == null) {
                return;
            }
            _preUpdateOn = DateTime.Now;
            if (_preMainCoin != this.MainCoinCode) {
                _preMainCoinShare = 0;
                _preMainCoinRejectShare = 0;
            }
            else {
                _preMainCoinShare = this.MainCoinTotalShare;
                _preMainCoinRejectShare = this.MainCoinRejectShare;
            }
            _preMainCoin = this.MainCoinCode;
            if (_preDualCoin != this.DualCoinCode) {
                _preDualCoinShare = 0;
                _preDualCoinRejectShare = 0;
            }
            else {
                _preDualCoinShare = this.DualCoinTotalShare;
                _preDualCoinRejectShare = this.DualCoinRejectShare;
            }
            _preDualCoin = this.DualCoinCode;
            #region MinerData
            if (!isMinerDataChanged) {
                isMinerDataChanged = this.ClientId != speedData.ClientId;
            }
            this.ClientId = speedData.ClientId;
            if (!isMinerDataChanged) {
                isMinerDataChanged = this.MACAddress != speedData.MACAddress;
            }
            this.MACAddress = speedData.MACAddress;
            if (!isMinerDataChanged) {
                isMinerDataChanged = this.LocalIp != speedData.LocalIp;
            }
            this.LocalIp = speedData.LocalIp;
            if (!isMinerDataChanged) {
                isMinerDataChanged = this.MinerName != speedData.MinerName;
            }
            this.MinerName = speedData.MinerName;
            if (!isMinerDataChanged) {
                isMinerDataChanged = this.IsOuterUserEnabled != speedData.IsOuterUserEnabled;
            }
            this.IsOuterUserEnabled = speedData.IsOuterUserEnabled;
            this.ReportOuterUserId = speedData.ReportOuterUserId;
            #endregion
            this.MineContextId = speedData.MineContextId;
            this.IsAutoBoot = speedData.IsAutoBoot;
            this.IsAutoStart = speedData.IsAutoStart;
            this.AutoStartDelaySeconds = speedData.AutoStartDelaySeconds;
            this.IsAutoRestartKernel = speedData.IsAutoRestartKernel;
            this.AutoRestartKernelTimes = speedData.AutoRestartKernelTimes;
            this.IsNoShareRestartKernel = speedData.IsNoShareRestartKernel;
            this.NoShareRestartKernelMinutes = speedData.NoShareRestartKernelMinutes;
            this.IsNoShareRestartComputer = speedData.IsNoShareRestartComputer;
            this.NoShareRestartComputerMinutes = speedData.NoShareRestartComputerMinutes;
            this.IsPeriodicRestartKernel = speedData.IsPeriodicRestartKernel;
            this.PeriodicRestartKernelHours = speedData.PeriodicRestartKernelHours;
            this.IsPeriodicRestartComputer = speedData.IsPeriodicRestartComputer;
            this.PeriodicRestartComputerHours = speedData.PeriodicRestartComputerHours;
            this.PeriodicRestartComputerMinutes = speedData.PeriodicRestartComputerMinutes;
            this.PeriodicRestartKernelMinutes = speedData.PeriodicRestartKernelMinutes;
            this.IsAutoStopByCpu = speedData.IsAutoStopByCpu;
            this.IsAutoStartByCpu = speedData.IsAutoStartByCpu;
            this.CpuStopTemperature = speedData.CpuStopTemperature;
            this.CpuStartTemperature = speedData.CpuStartTemperature;
            this.CpuLETemperatureSeconds = speedData.CpuLETemperatureSeconds;
            this.CpuGETemperatureSeconds = speedData.CpuGETemperatureSeconds;
            this.CpuPerformance = speedData.CpuPerformance;
            this.CpuTemperature = speedData.CpuTemperature;
            this.IsRaiseHighCpuEvent = speedData.IsRaiseHighCpuEvent;
            this.HighCpuPercent = speedData.HighCpuPercent;
            this.HighCpuSeconds = speedData.HighCpuSeconds;
            this.GpuDriver = speedData.GpuDriver;
            this.GpuType = speedData.GpuType;
            this.OSName = speedData.OSName;
            this.OSVirtualMemoryMb = speedData.OSVirtualMemoryMb;
            this.GpuInfo = speedData.GpuInfo;
            this.Version = speedData.Version;
            this.IsMining = speedData.IsMining;
            this.BootOn = speedData.BootOn;
            this.MineStartedOn = speedData.MineStartedOn;
            this.DiskSpace = speedData.DiskSpace;
            this.MainCoinCode = speedData.MainCoinCode;
            this.MainCoinTotalShare = speedData.MainCoinTotalShare;
            this.MainCoinRejectShare = speedData.MainCoinRejectShare;
            this.MainCoinSpeed = speedData.MainCoinSpeed;
            this.MainCoinPool = speedData.MainCoinPool;
            this.MainCoinWallet = speedData.MainCoinWallet;
            this.Kernel = speedData.Kernel;
            this.IsDualCoinEnabled = speedData.IsDualCoinEnabled;
            this.DualCoinPool = speedData.DualCoinPool;
            this.DualCoinWallet = speedData.DualCoinWallet;
            this.DualCoinCode = speedData.DualCoinCode;
            this.DualCoinTotalShare = speedData.DualCoinTotalShare;
            this.DualCoinRejectShare = speedData.DualCoinRejectShare;
            this.DualCoinSpeed = speedData.DualCoinSpeed;
            this.KernelCommandLine = speedData.KernelCommandLine;
            this.MainCoinSpeedOn = speedData.MainCoinSpeedOn;
            this.DualCoinSpeedOn = speedData.DualCoinSpeedOn;
            this.GpuTable = speedData.GpuTable;
            this.MainCoinPoolDelay = speedData.MainCoinPoolDelay;
            this.DualCoinPoolDelay = speedData.DualCoinPoolDelay;
            this.IsFoundOneGpuShare = speedData.IsFoundOneGpuShare;
            this.IsRejectOneGpuShare = speedData.IsRejectOneGpuShare;
            this.IsGotOneIncorrectGpuShare = speedData.IsGotOneIncorrectGpuShare;
            this.MineWorkId = speedData.MineWorkId;
            this.MineWorkName = speedData.MineWorkName;
            this.KernelSelfRestartCount = speedData.KernelSelfRestartCount - 1;// 需要减1
            this.LocalServerMessageTimestamp = speedData.LocalServerMessageTimestamp;
            this.TotalPhysicalMemoryMb = speedData.TotalPhysicalMemoryMb;
            this.MinerActiveOn = DateTime.Now;// 现在时间
            this.IsAutoDisableWindowsFirewall = speedData.IsAutoDisableWindowsFirewall;
            this.IsDisableAntiSpyware = speedData.IsDisableAntiSpyware;
            this.IsDisableUAC = speedData.IsDisableUAC;
            this.IsDisableWAU = speedData.IsDisableWAU;
            Extract(
                speedData, 
                out int mainCoinPoolDelayNumber, 
                out int dualCoinPoolDelayNumber, 
                out double mainCoinRejectPercent, 
                out double dualCoinRejectPercent);
            this.MainCoinPoolDelayNumber = mainCoinPoolDelayNumber;
            this.DualCoinPoolDelayNumber = dualCoinPoolDelayNumber;
            this.MainCoinRejectPercent = mainCoinRejectPercent;
            this.DualCoinRejectPercent = dualCoinRejectPercent;
        }

        public int GetMainCoinShareDelta(bool isPull) {
            if (_preMainCoinShare == 0) {
                return 0;
            }
            if (this.IsMining == false || string.IsNullOrEmpty(this.MainCoinCode)) {
                return 0;
            }
            if (isPull) {
                if (this._preUpdateOn.AddSeconds(5) > DateTime.Now) {
                    return 0;
                }
            }
            else if (this._preUpdateOn.AddSeconds(115) > DateTime.Now) {
                return 0;
            }

            int delta = this.MainCoinTotalShare - _preMainCoinShare;
            if (delta < 0) {
                delta = 0;
            }
            return delta;
        }

        public int GetDualCoinShareDelta(bool isPull) {
            if (_preDualCoinShare == 0) {
                return 0;
            }
            if (this.IsMining == false || string.IsNullOrEmpty(this.DualCoinCode)) {
                return 0;
            }
            if (isPull) {
                if (this._preUpdateOn.AddSeconds(5) > DateTime.Now) {
                    return 0;
                }
            }
            else if (this._preUpdateOn.AddSeconds(115) > DateTime.Now) {
                return 0;
            }

            int delta = this.DualCoinTotalShare - _preDualCoinShare;
            if (delta < 0) {
                delta = 0;
            }
            return delta;
        }

        public int GetMainCoinRejectShareDelta(bool isPull) {
            if (_preMainCoinRejectShare == 0) {
                return 0;
            }
            if (this.IsMining == false || string.IsNullOrEmpty(this.MainCoinCode)) {
                return 0;
            }
            if (isPull && this._preUpdateOn.AddSeconds(5) > DateTime.Now) {
                return 0;
            }
            else if (this._preUpdateOn.AddSeconds(115) > DateTime.Now) {
                return 0;
            }

            int delta = this.MainCoinRejectShare - _preMainCoinRejectShare;
            if (delta < 0) {
                delta = 0;
            }
            return delta;
        }

        public int GetDualCoinRejectShareDelta(bool isPull) {
            if (_preDualCoinRejectShare == 0) {
                return 0;
            }
            if (this.IsMining == false || string.IsNullOrEmpty(this.DualCoinCode)) {
                return 0;
            }
            if (isPull) {
                if (this._preUpdateOn.AddSeconds(5) > DateTime.Now) {
                    return 0;
                }
            }
            else if (this._preUpdateOn.AddSeconds(115) > DateTime.Now) {
                return 0;
            }

            int delta = this.DualCoinRejectShare - _preDualCoinRejectShare;
            if (delta < 0) {
                delta = 0;
            }
            return delta;
        }

        public string Id { get; set; }

        /// <summary>
        /// 服务的指定的作业
        /// </summary>
        public Guid WorkId { get; set; }

        public string WorkerName { get; set; }

        public string WindowsLoginName { get; set; }

        private string _windowsPassword;
        public string WindowsPassword {
            get {
                return _windowsPassword;
            }
            set {
                if (!Base64Util.IsBase64OrEmpty(value)) {
                    value = string.Empty;
                }
                _windowsPassword = value;
            }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime MinerActiveOn { get; set; }

        public Guid GroupId { get; set; }

        public DateTime NetActiveOn { get; set; }

        public bool IsOnline { get; set; }

        public string LoginName { get; set; }

        public string OuterUserId { get; set; }

        [JsonIgnore]
        public string AESPassword { get; set; }

        public DateTime AESPasswordOn { get; set; }

        // 因为服务端需要根据如下几个字段排序所以会有这几个字段
        public int MainCoinPoolDelayNumber { get; set; }
        public int DualCoinPoolDelayNumber { get; set; }
        public double MainCoinRejectPercent { get; set; }
        public double DualCoinRejectPercent { get; set; }
    }
}
