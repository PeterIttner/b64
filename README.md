# b64

Base 64 decode/encode command line util.

*Can either read input from commandline arguments, from command line pipes and from files.*

## Goals of this project

Easy to use base 64 conversion tool to prevent using any online websites for base 64 conversion, which are potentially insecure to use.

## Contribution

As this is mainly a personal utility program do not expect many changes in the future.
Feel free to contribute via pull-requests or submit issues if any. Please note, that there might be a quite long response time as I don't know how much I will invest in this small tool in the future.

## Requirements

[dotnetcore 8.0 runtime/sdk](https://dotnet.microsoft.com/download/dotnet-core/8.0)

## Build

```bash
dotnet restore .
dotnet build . -c Release /property:Version=1.2.0.0
```

## Tests

```bash
./tests.sh && echo "TESTS OK" || echo "TESTS FAILED"
```

## Installation

Simply build the program on your machine or download the latest release and add the location of the executable to your PATH variable.

## Example usages

Display Help:

```bash
b64 --help
```

Encode a ASCII text as base 64 ASCII text:

```bash
b64 encode Hello
b64 encode -f hello.txt
b64 encode < hello.txt
cat hello.txt | b64 encode
```

Decode a base64 ASCII text to regular ASCII text:

```bash
b64 decode SGVsbG8=
b64 decode -f hello.txt
b64 decode < hello.txt
cat hello.txt | b64 decode
```