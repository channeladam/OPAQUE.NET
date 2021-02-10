# OPAQUE.NET

## Overview

**DON'T STORE PASSWORDS!**

This library is a .NET 5 / Core implementation of the OPAQUE protocol - a 2021 successor to the Secure Remote Password (SRP) protocol and others.

Targets:

- .NET 5.0
- .NET 2.0

## Development Status

This library is under construction and is not yet ready for use.

## Background

It is 2021 - you don't need to store passwords!

Perhaps you are using the Secure Remote Password (SRP) protocol, and that is better, BUT, SRP has a number of issues.

There is better mechanism available for password authentication - the OPAQUE protocol!


*OPAQUE* is an Asymmetric Password Authenticated Key Exchange (aPAKE) protocol that:

- provides password authentication and mutually authenticated key exchange in a client-server setting;
- does NOT rely on PKI (except during initial registration);
- does NOT disclose passwords to servers or other entities other than the client machine;
- is secure against pre-computation attacks; and
- is capable of using a secret salt.

## Resources

- [Internet Draft for the OPAQUE Protocol](https://cfrg.github.io/draft-irtf-cfrg-opaque/draft-irtf-cfrg-opaque.html)
- [OPAQUE academic publication](https://eprint.iacr.org/2018/163.pdf)
- [Crypto Forum Research Group
 (CFRG) PAKE selection process in 2019](https://github.com/cfrg/pake-selection), which selected OPAQUE as the winner.
- [Wikipedia - Zero-knowledge Protocol](https://en.wikipedia.org/wiki/Zero-knowledge_proof)
