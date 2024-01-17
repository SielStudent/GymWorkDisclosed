"use client"
import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

export default function SignalRmessage() {
  const [connection, setConnection] = useState(null);
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    // Create and start the connection
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5206/NewWorkoutMessage", { credentials: 'include' })
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      // Start the connection
      connection
        .start()
        .then(() => {
          console.log("SignalR Connected.");
        })
        .catch((e) => console.log("Connection failed: ", e));

      // Define the event handler for receiving messages
      connection.on("ReceiveMessage", (message) => {
        setMessages((messages) => [...messages, message]);
      });
    }
  }, [connection]);

  return (
    <div>
      <h2>SignalR Messages</h2>
      <ul>
        {messages.map((message, i) => (
          <li key={i}>{message}</li>
        ))}
      </ul>
    </div>
  );
}
