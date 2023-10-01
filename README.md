# Api Fire and Forget

Below are the commonly known scenarios for background processing:
- Sending an email
- Calling a third-party API
- Reading a message queue

Channel + BackgroundService combination is an excellent option for the same. 

This repo contains a code showing how to use channel and background service with .NET Core minimal APIs.

:information_source: Note: Using durable queues such as RabbitMQ and Azure Service Bus is always recommended to retain the data despite failure.
