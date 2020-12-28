These instrument drivers are a new form of native C# drivers.

If you wish to still use the plain SCPI commands, have a look at the folder RsInstrument.
It contains a generic C# VISA interface with all the functionalities you might need for this task. See the "Readme.txt" inside. 

Preconditions: Installed R&S VISA or NI VISA

Currently supported CMW subsystems:
- Base: RsCmwBase
- Global Purpose RF: RsCmwGprfGen, RsCmwGprfMeas
- Bluetooth: RsCmwBluetoothSig, RsCmwBluetoothMeas
- LTE: RsCmwLteSig, RsCmwLteMeas
- Wcdma Signaling: RsCmwWcdmaSig
- GSM Signaling: RsCmwGsmSig
- WLAN: RsCmwWlanSig, RscmwWlanMeas

Each driver contains file Usage.cs where all the driver functions are used. In the comments you find the corresponding SCPI commands.
This way you can pair a SCPI command and a method/property of the driver.

Each driver assembly comes with the intellisense information store in the external xml file.
Make sure you keep this file together with the assembly to be able to access the Intellisense information in Visual Studio.

Please check out the examples in the Examples directory to see how to use multiple drivers with one CMW
Before you start the examples, update the driver references to fit your folder structure.
Remember to adjust the resourceName string to fit your instrument.

In case you require support for more subsystems, please contact our customer support on customersupport@rohde-schwarz.com with the topic "Auto-generated C# drivers" in the email subject. This will speed up the response process


--------------------------------------------------------------------------------

Version history (for the whole group):

Version 3.7.xx4
- Fixed several interface names
- New release for CMW Base 3.7.90
- New release for CMW Bluetooth 3.7.90

Version 3.7.xx3
- Second release of the CMW C# drivers packet
- New core component RsInstrument 1.0.0.30
- Previously, the groups starting with CATalog: e.g. 'CATalog:SIGNaling:TOPology:PLMN' were reordered to 'SIGNaling:TOPology:PLMN:CATALOG' give more contextual meaning to the method/property name
    This is now reverted back, since it was hard to find the desired functionality.
- Reorganized Utilities interface to sub-groups
- Changed behaviour where non-defined enum type responses from the instrumet caused exception. Now, for special values e.g. "NAN" "OFL", "NAV", the driver returns integer numbers.
- Fixed issue with Reliability indicator lookup table exception. Now if the code is not found, the exception is suppressed and the Message/LastMessage property gets value "<UNKNOWN>"
- Added Write/Query With Opc Event
- Added locking for multithreading safety
- Added segmented read / write events


- Fixed some misspeling errors
- Changed enum and repCap types names
- All the assemblies are signed with Rohde & Schwarz signature

Version 1.0.0.0
- First released version