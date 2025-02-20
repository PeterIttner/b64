#!/bin/bash

set -e
#set -o xtrace

dotnet build -c Debug

unameOut="$(uname -s)"
case "${unameOut}" in
    Linux*)     machine=Linux;;
    Darwin*)    machine=Mac;;
    CYGWIN*)    machine=Cygwin;;
    MINGW*)     machine=MinGw;;
    MSYS_NT*)   machine=Git;;
    *)          machine="UNKNOWN:${unameOut}"
esac

echo "Running current tests on $machine"
dotnetversion="net9.0"
exe="./bin/Debug/$dotnetversion/b64.exe"
if [ "$machine" == "Linux" ] || [ "$machine" == "Mac" ]; then
	exe="./bin/Debug/$dotnetversion/b64"
else
	exe="./bin/Debug/$dotnetversion/b64.exe"
fi

function assert() {

	testname=$1
	expected=$2
	actual=$3

	if [ "$expected" != "$actual" ]; then

		echo "$testname: Expected value: $expected, but have $actual"

		echo " _            _          __       _ _          _ ";
		echo "| |_ ___  ___| |_ ___   / _| __ _(_) | ___  __| |";
		echo "| __/ _ \/ __| __/ __| | |_ / _\` | | |/ _ \/ _\` |";
		echo "| ||  __/\__ \ |_\__ \ |  _| (_| | | |  __/ (_| |";
		echo " \__\___||___/\__|___/ |_|  \__,_|_|_|\___|\__,_|";
		echo "                                                 ";

		
		exit 1
	else
		echo "$testname succeeded"
	fi
}

function success() {
	echo "All tests passed"
	echo
	echo " _            _                                         _          _ ";
	echo "| |_ ___  ___| |_ ___   ___ _   _  ___ ___ ___  ___  __| | ___  __| |";
	echo "| __/ _ \/ __| __/ __| / __| | | |/ __/ __/ _ \/ _ \/ _\` |/ _ \/ _\` |";
	echo "| ||  __/\__ \ |_\__ \ \__ \ |_| | (_| (_|  __/  __/ (_| |  __/ (_| |";
	echo " \__\___||___/\__|___/ |___/\__,_|\___\___\___|\___|\__,_|\___|\__,_|";
	echo "                                                                     ";
}


########################################################################
# Test encoding decoding of a simple string
########################################################################
test_name="Test 1"
value=test_value
expectedEncoded=dGVzdF92YWx1ZQ==

encoded=`$exe encode $value`
assert "$test_name" "$encoded" "$expectedEncoded"

decoded=`$exe decode $encoded`
assert "$test_name" "$decoded" "$value"

########################################################################
# Test encoding decoding from a file with -f
########################################################################
test_name="Test 2"
value="hello world"
expectedEncoded=aGVsbG8gd29ybGQ=
echo -n $value > test_input.txt
encoded=`$exe encode -f test_input.txt`
assert "$test_name" "$encoded" "$expectedEncoded"

`$exe encode -f test_input.txt > test_encoded.txt`
decoded=`$exe decode -f test_encoded.txt`
assert "$test_name" "$decoded" "$value"

rm test_input.txt
rm test_encoded.txt

########################################################################
# Test encoding decoding from a file with <
########################################################################
test_name="Test 3"
value="hello world"
expectedEncoded=aGVsbG8gd29ybGQ=
echo -n $value > test_input.txt
encoded=`$exe encode < test_input.txt`
assert "$test_name" "$encoded" "$expectedEncoded"

`$exe encode -f test_input.txt > test_encoded.txt`
decoded=`$exe decode < test_encoded.txt`
assert "$test_name" "$decoded" "$value"
rm test_input.txt
rm test_encoded.txt

########################################################################
# Test encoding decoding from a file with pipe
########################################################################
test_name="Test 4"
value="hello world"
expectedEncoded=aGVsbG8gd29ybGQ=
echo -n $value > test_input.txt
encoded=`cat test_input.txt | $exe encode`
assert "$test_name" "$encoded" "$expectedEncoded"

`$exe encode -f test_input.txt > test_encoded.txt`
decoded=`cat test_encoded.txt | $exe decode`
assert "$test_name" "$decoded" "$value"

rm test_input.txt
rm test_encoded.txt

########################################################################
# Test url encoding
########################################################################
test_name="Test 5"
value="https://Hello?a=c&c=d_arg"
expectedEncoded=aHR0cHM6Ly9IZWxsbz9hPWMmYz1kX2FyZw
echo -n $value > test_input.txt
encoded=`cat test_input.txt | $exe encode --url`
assert "$test_name" "$encoded" "$expectedEncoded"

`$exe encode -f test_input.txt > test_encoded.txt`
decoded=`cat test_encoded.txt | $exe decode --url`
assert "$test_name" "$decoded" "$value"

rm test_input.txt
rm test_encoded.txt

########################################################################
# Test url encoding back and forth with pipe
########################################################################
test_name="Test 6"
value="https://Hello?a=c&c=d_arg"
expected=`$exe encode --url $value | $exe decode --url`

assert "$test_name" "$expected" "$value"

########################################################################
# Test encoding with whitespace
########################################################################
test_name="Test 7"
value="https://Hello?a=c&c=d_arg"
expected=`$exe encode --url  $value  | $exe decode  --url   `

assert "$test_name" "$expected" "$value"


success

