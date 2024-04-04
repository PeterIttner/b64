#!/bin/bash

set -e

dotnet build -c Debug
exe="./bin/Debug/net8.0/b64.exe"

########################################################################
# Test encoding decoding of a simple string
########################################################################
test_name="Test 1"
value=test
expectedEncoded=dGVzdA==

encoded=`$exe encode $value`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
decoded=`$exe decode $encoded`
if [ "$decoded" != "$value" ]; then
	echo "$test_name: Expected decoded value: $expected, but have $decoded"
	exit 1
fi

########################################################################
# Test encoding decoding from a file with -f
########################################################################
test_name="Test 2"
value="hello world"
expectedEncoded=aGVsbG8gd29ybGQ=
echo -n $value > test.txt
encoded=`$exe encode -f test.txt`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
`$exe encode -f test.txt > test_encoded.txt`
decoded=`$exe decode -f test_encoded.txt`
if [ "$decoded" != "$value" ]; then
	echo "$test_name: Expected decoded value: $expected, but have $decoded"
	exit 1
fi

rm test.txt
rm test_encoded.txt

########################################################################
# Test encoding decoding from a file with <
########################################################################
test_name="Test 3"
value="hello world"
expectedEncoded=aGVsbG8gd29ybGQ=
echo -n $value > test.txt
encoded=`$exe encode < test.txt`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
`$exe encode -f test.txt > test_encoded.txt`
decoded=`$exe decode < test_encoded.txt`
if [ "$decoded" != "$value" ]; then
	echo "$test_name: Expected decoded value: $expected, but have $decoded"
	exit 1
fi

rm test.txt
rm test_encoded.txt

########################################################################
# Test encoding decoding from a file with pipe
########################################################################
test_name="Test 4"
value="hello world"
expectedEncoded=aGVsbG8gd29ybGQ=
echo -n $value > test.txt
encoded=`cat test.txt | $exe encode`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
`$exe encode -f test.txt > test_encoded.txt`
decoded=`cat test_encoded.txt | $exe decode`
if [ "$decoded" != "$value" ]; then
	echo "$test_name: Expected decoded value: $expected, but have $decoded"
	exit 1
fi

rm test.txt
rm test_encoded.txt

########################################################################
# Test url encoding
########################################################################
test_name="Test 5"
value="https://Hello?a=c&c=d_arg"
expectedEncoded=aHR0cHM6Ly9IZWxsbz9hPWMmYz1kX2FyZw
echo -n $value > test.txt
encoded=`cat test.txt | $exe encode --url`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
`$exe encode -f test.txt > test_encoded.txt`
decoded=`cat test_encoded.txt | $exe decode --url`
if [ "$decoded" != "$value" ]; then
	echo "$test_name: Expected decoded value: $expected, but have $decoded"
	exit 1
fi

rm test.txt
rm test_encoded.txt

########################################################################
# Test url encoding back and forth with pipe
########################################################################
test_name="Test 6"
value="https://Hello?a=c&c=d_arg"
expected=`$exe encode --url $value | $exe decode --url`

if [ "$expected" != "$value" ]; then
	echo "$test_name: Expected encoded value: $expected, but have $value"
	exit 1
fi


echo "Tests passed!"