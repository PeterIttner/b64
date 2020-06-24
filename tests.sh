#!/bin/bash

set -e

########################################################################
# Test encoding decoding of a simple string
########################################################################
test_name="Test 1"
value=test
expectedEncoded=dGVzdA==

encoded=`b64 encode $value`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
decoded=`b64 decode $encoded`
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
encoded=`b64 encode -f test.txt`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
b64 encode -f test.txt > test_encoded.txt
decoded=`b64 decode -f test_encoded.txt`
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
encoded=`b64 encode < test.txt`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
b64 encode -f test.txt > test_encoded.txt
decoded=`b64 decode < test_encoded.txt`
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
encoded=`cat test.txt | b64 encode`
if [ "$encoded" != "$expectedEncoded" ]; then
	echo "$test_name: Expected encoded value: $expectedEncoded, but have $encoded"
	exit 1
fi
b64 encode -f test.txt > test_encoded.txt
decoded=`cat test_encoded.txt | b64 decode`
if [ "$decoded" != "$value" ]; then
	echo "$test_name: Expected decoded value: $expected, but have $decoded"
	exit 1
fi

rm test.txt
rm test_encoded.txt