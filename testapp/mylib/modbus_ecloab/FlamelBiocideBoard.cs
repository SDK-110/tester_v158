using System;
using System.Collections.Generic;

namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// High-level interface to the Flamel Biocide board over Modbus RTU.
    ///
    /// This class wraps the raw Modbus master with board-specific register
    /// names and value semantics, so test code reads like the test spec.
    /// </summary>
    public class FlamelBiocideBoard
    {
        private readonly ModbusRtuMaster _master;

        /// <summary>
        /// Modbus slave ID. Default 22 (OX board per test spec section 3.2).
        /// </summary>
        public byte SlaveId { get; set; } = 22;

        public FlamelBiocideBoard(ModbusRtuMaster master)
        {
            _master = master ?? throw new ArgumentNullException(nameof(master));
        }

        // ── Firmware & Bootloader ─────────────────────────────────────

        public (ushort Major, ushort Minor, ushort Build) ReadFirmwareVersion()
        {
            ushort[] regs = _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.FirmwareVersionMajor,
                3);
            return (regs[0], regs[1], regs[2]);
        }

        public ushort[] ReadBootloaderInfo()
        {
            return _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.BootloaderInfoStart,
                FlamelRegisterMap.BootloaderInfoCount);
        }

        // ── Digital Input States ───────────────────────────────────────

        public ushort ReadDeliveryOverflowState()
        {
            return _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.DeliveryOverflowState,
                1)[0];
        }

        public ushort ReadDeliveryEmptyState()
        {
            return _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.DeliveryEmptyState,
                1)[0];
        }

        public ushort ReadRecircFullState()
        {
            return _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.RecircFullState,
                1)[0];
        }

        // ── Analog / Sensor Readings ──────────────────────────────────

        public ushort ReadConductivityProbeTemp()
        {
            return _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.ConductivityProbeTemp,
                1)[0];
        }

        public ushort ReadConductivity()
        {
            return _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.ConductivityReading,
                1)[0];
        }

        public ushort ReadProductLevel()
        {
            return _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.ProductLevel,
                1)[0];
        }

        // ── Board Type ─────────────────────────────────────────────────

        public BoardType ReadBoardType()
        {
            ushort val = _master.ReadInputRegisters(
                SlaveId,
                FlamelRegisterMap.BoardTypeRegister,
                1)[0];
            return (BoardType)val;
        }

        // ── Relay Control ──────────────────────────────────────────────

        public void EnterCoilControlMode()
        {
            _master.WriteSingleRegister(
                SlaveId,
                FlamelRegisterMap.ControlRegister,
                FlamelRegisterMap.ControlValueEnterCoilMode);
        }

        public void ExitCoilControlMode()
        {
            _master.WriteSingleRegister(
                SlaveId,
                FlamelRegisterMap.ControlRegister,
                FlamelRegisterMap.ControlValueExitCoilMode);
        }

        public void SetSpareRelay(bool on)
        {
            _master.WriteSingleCoil(SlaveId, FlamelRegisterMap.CoilSpareRelay, on);
        }

        public void SetRecircPumpRelay(bool on)
        {
            _master.WriteSingleCoil(SlaveId, FlamelRegisterMap.CoilRecircPumpRelay, on);
        }

        public void SetDumpValveRelay(bool on)
        {
            _master.WriteSingleCoil(SlaveId, FlamelRegisterMap.CoilDumpValveRelay, on);
        }

        public void SetWaterValveRelay(bool on)
        {
            _master.WriteSingleCoil(SlaveId, FlamelRegisterMap.CoilWaterValveRelay, on);
        }

        public Dictionary<string, bool> ReadAllRelayStates()
        {
            bool[] coils = _master.ReadCoils(SlaveId, FlamelRegisterMap.CoilRecircPumpRelay, 4);
            return new Dictionary<string, bool>
            {
                { "Recirc Pump (K2/DS2)",  coils[0] },
                { "Water Valve (K4/DS4)",  coils[1] },
                { "Spare Relay (K1/DS1)",  coils[2] },
                { "Dump Valve (K3/DS3)",   coils[3] }
            };
        }
    }
}
