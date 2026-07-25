namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// Static register map for the Flamel Biocide board (Ecolab P/N 5300XXXX).
    ///
    /// All addresses are 0-based as used in the Radzio Modbus Master Simulator
    /// and the test specification.
    ///
    /// Modbus function codes used:
    ///   FC 04 — Read Input Registers  (sensor states, firmware, etc.)
    ///   FC 03 — Read Holding Registers (control register readback)
    ///   FC 06 — Write Single Register  (control register write)
    ///   FC 01 — Read Coils             (relay status readback)
    ///   FC 05 — Write Single Coil      (relay control)
    /// </summary>
    public static class FlamelRegisterMap
    {
        // ── Input Registers (FC 04) ──────────────────────────────────

        /// <summary>Firmware version — major (Reg 4, expected: 0)</summary>
        public const ushort FirmwareVersionMajor = 4;

        /// <summary>Firmware version — minor (Reg 5, expected: 0)</summary>
        public const ushort FirmwareVersionMinor = 5;

        /// <summary>Firmware version — build (Reg 6, expected: ≥ 21)</summary>
        public const ushort FirmwareVersionBuild = 6;

        /// <summary>Bootloader info start register (Reg 19)</summary>
        public const ushort BootloaderInfoStart = 19;

        /// <summary>Number of bootloader info registers (19–24)</summary>
        public const ushort BootloaderInfoCount = 6;

        // Expected bootloader values: [1, 0, 3, 0, 13398, 18]
        public static readonly ushort[] ExpectedBootloaderInfo;

        static FlamelRegisterMap()
        {
            ExpectedBootloaderInfo = new ushort[6];
            ExpectedBootloaderInfo[0] = 1;      // Reg 19
            ExpectedBootloaderInfo[1] = 0;      // Reg 20
            ExpectedBootloaderInfo[2] = 3;      // Reg 21
            ExpectedBootloaderInfo[3] = 0;      // Reg 22
            ExpectedBootloaderInfo[4] = 13398;  // Reg 23
            ExpectedBootloaderInfo[5] = 18;     // Reg 24
        }

        /// <summary>Delivery Empty State (Reg 106: 0=open, 1=closed)</summary>
        public const ushort DeliveryEmptyState = 106;

        /// <summary>Recirc Full State (Reg 109: 0=closed, 1=open)</summary>
        public const ushort RecircFullState = 109;

        /// <summary>Delivery Overflow State (Reg 112: 0=closed, 1=open)</summary>
        public const ushort DeliveryOverflowState = 112;

        /// <summary>Conductivity Probe Temperature (Reg 117, expected: 21)</summary>
        public const ushort ConductivityProbeTemp = 117;

        /// <summary>Product Level (Reg 119, expected: 50)</summary>
        public const ushort ProductLevel = 119;

        /// <summary>Conductivity Reading (Reg 129: 11002 @ 700Ω, 15284 @ 7KΩ)</summary>
        public const ushort ConductivityReading = 129;

        /// <summary>Board Type (Reg 141: 1=OX, 0=Non-OX, 2=No Product)</summary>
        public const ushort BoardTypeRegister = 141;

        // ── Coil Addresses (FC 01 / FC 05) ────────────────────────────

        /// <summary>Recirc Pump Relay (K2, DS2, J7 pin1)</summary>
        public const ushort CoilRecircPumpRelay = 101;

        /// <summary>Water Valve Relay (K4, DS4, J10A/J10B pin1)</summary>
        public const ushort CoilWaterValveRelay = 102;

        /// <summary>Spare Relay (K1, DS1, J22 pin1)</summary>
        public const ushort CoilSpareRelay = 103;

        /// <summary>Dump Valve Relay (K3, DS3, J7 pin2)</summary>
        public const ushort CoilDumpValveRelay = 104;

        // ── Holding Registers (FC 03 / FC 06) ─────────────────────────

        /// <summary>
        /// Control Register (Reg 185):
        ///   Write 2 → enter coil control mode (enable relay writes)
        ///   Write 3 → exit coil control mode
        /// </summary>
        public const ushort ControlRegister = 185;

        public const ushort ControlValueEnterCoilMode = 2;
        public const ushort ControlValueExitCoilMode = 3;

        // ── Expected Conductivity Values ──────────────────────────────

        /// <summary>Conductivity at 700Ω (tolerance ±50)</summary>
        public const int ConductivityAt700Ohm = 11002;

        /// <summary>Conductivity tolerance at 700Ω</summary>
        public const int ConductivityTolerance700 = 50;

        /// <summary>Conductivity at 7KΩ (tolerance ±70)</summary>
        public const int ConductivityAt7KOhm = 15284;

        /// <summary>Conductivity tolerance at 7KΩ</summary>
        public const int ConductivityTolerance7K = 70;

        // ── Product Level Expected Value ──────────────────────────────

        public const int ExpectedProductLevel = 50;
    }
}
