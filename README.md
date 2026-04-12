# Training Exercise: Encoder/Decoder

## Description

* Create a connector that simulates an Encoder or Decoder.\
The following parameters must be displayed.

### Encoder

* Status
  * Read/Write
  * Enabled/Disabled
* Current Compressed Bitrate
  * Read
  * Unit: Mbps
  * Decimals: 3
* Auto Chroma Weight
  * Read/Write
  * Enabled/Disabled
* Chroma Weight
  * Read/Write
  * Unit: %
  * Range
* Lossless Mode
  * Read/Write
  * Enabled/Disabled

### Decoder

* Status
  * Read/Write
  * Enabled/Disabled
* Current Compressed Bitrate
  * Read
  * Unit: Mbps
  * Decimals: 3
* Progression Order
  * Read/Write
  * Options:
    * 0: LCRP
    * 1: RLCP
    * 2: RPCL
    * 3: PCRL
    * 4: CPRL
* Code Block Width
  * Read
  * Unit: px
* Code Block Height
  * Read
  * Unit: px

## Requirements

* Use default values to set initial values.
* Only the encoder or decoder can be enabled at one point in time.
* Once disabled, display "Not Available" on the data parameters.
* Don't make use of any QActions! Only Protocol Triggers, Actions, Groups, ... are allowed.

---
---

# Training Exercise 2:  Encoder/Decoder through QActions

## Description

* Same exercise but this time make use of QActions to accomplish the same goal.

## Requirements

* Use QActions to set the default and exception values.
* Every second, we would like to see the value of the *Current Compressed Bitrate* change (as a way to simulate device updates).\
The chosen value can be completely random, range: 0-15 Mbps.

---
---

# Training Exercise 3:  Extra Alarm Requirements

### Requirement 1

The end user considers the encoder/decoder *Current Compressed Bitrate* parameter to be a critical KPI.\
A scenario was discovered where they notice on the system that this parameter generates frequent short alarms due to drops or spikes on the video stream.\
Example: 
* 10 Mbps → OK
* 6.8 Mbps → Critical Low
* 9.6 Mbps → OK again

Is there a way to configure this in such a way that the alarm is only raised when it remains below or above the configured thresholds for more than 3 seconds?
Which feature would you use? Try and apply it.

### Requirement 2

The *Current Compressed Bitrate* parameter is expected to be around 10 Mbps +- 10% by the end user.\
How would you apply a configuration that takes this percentage into account and raises
* a warning low alarm when it dips below 10 Mbps - 5%?
* a warning high alarm when it's more than 10 Mbps + 5%?
* a critical low alarm when it dips below 10 Mbps - 10%?
* a critical high alarm when it's more than 10 Mbps + 10%?

### Requirement 3

Since the device can only operate in 1 mode at a time (encoder/decooder), we don't want to have any monitoring enabled on the non-related parameters.
Can you find a way to configure this conditional behavior? Try and apply it.

### Requirement 4

When the *Status* parameter changes, the end user would like to have an information event raised.

### Requirement 5

Apply the configuration that the exception values defined on the protocol parameters show up by default as Normal alarms.

---
---

# Training Exercise 4:  Encoder/Decoder Layout enhancements

## Description

* The device can only operate in 1 mode at a time: either as encoder or as decoder.\
Therefor if it's currently operating as en encoder, we are not interested in the decoder parameters and vice versa.
1. Split up the encoder and decoder parameters onto 2 seperate pages
2. Investigate and apply a way to show/hide the correct page.

Note that the *Encoder Status* parameter and *Decoder Status* parameter can be removed, instead 1 different parameter can be foreseen:

* Operation Mode
  * Read/Write
  * Encoder/Decoder

## Requirements

* The *Operation Mode* parameter can be placed on a new page called *General* which should be set as the default page.
* Only the related parameters and/or pages are visible according to the chosen operational mode.
* The unrelated parameters are still set back to exception values even though they are not visible.
