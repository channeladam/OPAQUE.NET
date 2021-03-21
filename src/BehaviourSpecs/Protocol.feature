Feature: Protocol

Scenario: Should correctly generate the protocol Context String
When the Context String for a protocol context is generated
Then the Context String is as expected

# Scenario: Should generate a public/private key pair
# When a public-private key pair is generated
# Then the public and private keys are available

# Scenario: 01 - Should register a client with a server
# Given the client with a username and password
# When the client registers with the server
# Then the client receives a session token from the server