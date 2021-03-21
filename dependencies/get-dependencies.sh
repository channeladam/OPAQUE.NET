#!/bin/sh

SODIUM_FILE=libsodium-1.0.18-stable.tar.gz
SODIUM_FOLDER=./sodium

mkdir -p $SODIUM_FOLDER
cd $SODIUM_FOLDER
curl -L -o $SODIUM_FILE https://download.libsodium.org/libsodium/releases/$SODIUM_FILE
tar -xvf $SODIUM_FILE libsodium-stable/src/libsodium/include
